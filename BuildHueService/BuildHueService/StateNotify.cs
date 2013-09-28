using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BuildHueService
{
    [ServiceBehavior(
    ConcurrencyMode = ConcurrencyMode.Single,
    InstanceContextMode = InstanceContextMode.Single
  )]
    public class StateNotify : IStateNotify
    {
        private static Logger s_log = LogManager.GetCurrentClassLogger();

        private RuleProcessor m_ruleProcessor;

        public StateNotify(RuleProcessor ruleProcessor)
        {
            m_ruleProcessor = ruleProcessor;
        }

        public void SetState(string build, string result)
        {
            BuildResult resultEnum;
            if (Enum.TryParse(result, true, out resultEnum))
            {
                StateMaintainer.Instance.ChangeState(build, resultEnum);
                StartRuleProcess();
            }
        }

        private void StartRuleProcess()
        {
            Task.Factory.StartNew(() => RunRuleProcess());
        }

        private void RunRuleProcess()
        {
            IEnumerable<LightStatus> lightStatuses = m_ruleProcessor.GetCurrentStates();
            s_log.Info("Sending new colour states");
            foreach (var lightStatus in lightStatuses)
            {

                s_log.Debug("LightID: " + lightStatus.LightId);
                foreach (var lightColour in lightStatus.Colours)
                {
                    s_log.Debug(String.Concat("Colour X ", lightColour.X, " Y ", lightColour.Y));
                }

            }

        }

    }
}
