using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildHueService
{
    public class StateMaintainer
    {
        private Dictionary<string, BuildResult> m_states;
        private static StateMaintainer s_instance;

        public static StateMaintainer Instance
        {
            get
            {
                if (null == s_instance)
                {
                    s_instance = new StateMaintainer();
                }
                return s_instance;
            } 
        }

        private StateMaintainer()
        {
            m_states = new Dictionary<string, BuildResult>();
        }

        public void ChangeState(string build, BuildResult buildResult)
        {
            m_states[build] = buildResult;
        }

        public IEnumerable<BuildState> GetBuildStates()
        {
            foreach (var buildState in m_states)
            {
                yield return new BuildState()
                {
                    Build = buildState.Key,
                    BuildResult = buildState.Value
                };
            }
        }
    }
}
