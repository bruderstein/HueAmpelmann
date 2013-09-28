using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BuildHueService
{
    [TestFixture]
    public class TestRuleProcessing
    {
        private LightColour m_green = new LightColour(Color.Green);
        private LightColour m_red = new LightColour(Color.Red);
        private LightColour m_orange = new LightColour(Color.Orange);
        private LightColour m_blue = new LightColour(Color.Blue);

        [SetUp]
        public void SetUp()
        {

            HueConfiguration.ClearConfiguration();
            StateMaintainer.ClearStates();

            HueConfiguration.Instance.ResultStates[BuildResult.Success] = m_green;
            HueConfiguration.Instance.ResultStates[BuildResult.Fail] = m_red;
            HueConfiguration.Instance.ResultStates[BuildResult.FailTests] = m_orange;

            HueConfiguration.Instance.SetBuildConfiguration(new BuildConfiguration()
            {
                Colour = m_blue,
                Lights = new List<int>(new[] {1, 2}),
                Name = "test.build"
            });

            HueConfiguration.Instance.SetLightIds(new[] {1, 2, 3});

        }
        
        [Test]
        public void TestNoRules()
        {
            var ruleProcessor = new RuleProcessor();

            var lightStatuses = new List<LightStatus>(ruleProcessor.GetCurrentStates());
            
            Assert.That(lightStatuses.Count, Is.EqualTo(3));
            Assert.That(lightStatuses[0].LightId, Is.EqualTo(1));
            Assert.That(lightStatuses[0].Colours.Count, Is.EqualTo(1));
            Assert.That(lightStatuses[1].LightId, Is.EqualTo(2));
            Assert.That(lightStatuses[1].Colours.Count, Is.EqualTo(1));
            Assert.That(lightStatuses[2].LightId, Is.EqualTo(3));
            Assert.That(lightStatuses[2].Colours.Count, Is.EqualTo(1));

        }


        [Test]
        public void TestAddFailRule()
        {

            var ruleProcessor = new RuleProcessor();

            StateMaintainer.Instance.ChangeState("test.build", BuildResult.Fail);
            var lightStatuses = new List<LightStatus>(ruleProcessor.GetCurrentStates());
            
            Assert.That(lightStatuses.Count, Is.EqualTo(3));
            Assert.That(lightStatuses[0].LightId, Is.EqualTo(1));
            Assert.That(lightStatuses[0].Colours.Count, Is.EqualTo(2));
            Assert.That(lightStatuses[0].Colours[0], Is.EqualTo(m_red));
            Assert.That(lightStatuses[0].Colours[1], Is.EqualTo(m_blue));

            Assert.That(lightStatuses[1].LightId, Is.EqualTo(2));
            Assert.That(lightStatuses[1].Colours.Count, Is.EqualTo(2));
            Assert.That(lightStatuses[1].Colours[0], Is.EqualTo(m_red));
            Assert.That(lightStatuses[1].Colours[1], Is.EqualTo(m_blue));

            Assert.That(lightStatuses[2].LightId, Is.EqualTo(3));
            Assert.That(lightStatuses[2].Colours.Count, Is.EqualTo(1));
            Assert.That(lightStatuses[2].Colours[0], Is.EqualTo(m_green));

            
        }
    }
}
