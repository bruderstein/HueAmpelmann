using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace BuildHueService
{
    public class RuleProcessor
    {
        public IEnumerable<LightStatus> GetCurrentStates()
        {
            // This is a dictionary of lights, to state colours, to build colours.  So each light has a state color, and then a list of build colors.
            // Success colours don't get added, so if you end up with an empty dictionary at the end, it's all good.
            var results = new LightResults();

            foreach (var buildState in StateMaintainer.Instance.GetBuildStates())
            {
                ProcessRules(buildState, results);
            }

            return results.GetLightStatuses();
        }

        private void ProcessRules(BuildState buildState, LightResults results)
        {
            BuildConfiguration buildConfiguration;
            if (HueConfiguration.Instance.BuildConfigurations.TryGetValue(buildState.Build, out buildConfiguration))
            {
                if (buildState.BuildResult != BuildResult.Success)
                {
                    foreach (var lightID in buildConfiguration.Lights)
                    {
                        results.AddResult(lightID, HueConfiguration.Instance.ResultStates[buildState.BuildResult], buildConfiguration.Colour);
                    }
                }
            }
        }


        private class LightResults
        {
            private Dictionary<int, Dictionary<LightColour, List<LightColour>>> m_results;

            public LightResults()
            {
                m_results =
                    new Dictionary<int, Dictionary<LightColour, List<LightColour>>>(
                        HueConfiguration.Instance.LightIds.Count);

                foreach (var lightId in HueConfiguration.Instance.LightIds)
                {
                    m_results[lightId] = new Dictionary<LightColour, List<LightColour>>();
                }
            }

            public void AddResult(int lightId, LightColour stateColor, LightColour buildColor)
            {
                Dictionary<LightColour, List<LightColour>> stateDictionary;

                // We should always have the lights inserted, but just in case...
                if (!m_results.TryGetValue(lightId, out stateDictionary))
                {
                    stateDictionary = new Dictionary<LightColour, List<LightColour>>();
                    m_results.Add(lightId, stateDictionary);
                }

                List<LightColour> buildColors;
                if (!stateDictionary.TryGetValue(stateColor, out buildColors))
                {
                    buildColors = new List<LightColour>();
                    stateDictionary.Add(stateColor, buildColors);
                }

                buildColors.Add(buildColor);

            }

            public IEnumerable<LightStatus> GetLightStatuses()
            {
                foreach (var result in m_results)
                {
                    LightStatus lightStatus = new LightStatus(result.Key);
                    if (result.Value.Count == 0)
                    {
                        // Everything is good for this light
                        lightStatus.Colours.Add(HueConfiguration.Instance.ResultStates[BuildResult.Success]);
                    }
                    else
                    {
                        foreach (var stateColour in result.Value)
                        {
                            lightStatus.Colours.Add(stateColour.Key);
                            foreach (var buildColour in stateColour.Value)
                            {
                                lightStatus.Colours.Add(buildColour);
                            }
                        }

                    }
                    yield return lightStatus;
                }
            }
        }
    }
}