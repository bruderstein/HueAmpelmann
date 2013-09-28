using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamCityTrafficLightsConfigurator.Management;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;

using BuildHueService;

namespace LightsUnitTest
{
    [TestClass]
    public class UnitTest1
    {

        private static string ip = "http://192.168.1.134";
        [TestMethod]
        public void FirstTest_ShouldPass()
        {
            //Arrange
            var lights = new Lights(ip);

            //Act
            var result = lights.DeveloperTest();


            //Assert
        }

        [TestMethod]
        public void CreateNewDeveloper_ShouldPass()
        {
            //Arrange
            var lights = new Lights(ip);

            //Act
            var result = lights.CreateNewDeveloper("testuser", "coolDeveloper");


            //Assert
        }

        [TestMethod]
        public void GetAll_ShouldPass()
        {
            //Arrange
            var lights = new Lights(ip);

            //Act
            var result = lights.GetAll("coolDeveloper");


            //Assert
        }

        [TestMethod]
        public void Get_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.Get("coolDeveloper", 1);


            //Assert
        }

        [TestMethod]
        public void TurnOn_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.TurnOn("coolDeveloper", 3);


            //Assert
        }

        [TestMethod]
        public void TurnOff_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.TurnOff("coolDeveloper", 3);


            //Assert
        }

        [TestMethod]
        public void GetIds_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.GetIds("coolDeveloper");

            //Assert
            Assert.AreEqual(result.Count, 3);
        }


        [TestMethod]
        public void ChangeColour_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.ChangeColour("coolDeveloper", 1, 255, 255, 20);
            Thread.Sleep(1000);
            lights.ChangeColour("coolDeveloper", 1, 255, 255, 46920);
            Thread.Sleep(1000);
            lights.ChangeColour("coolDeveloper", 1, 255, 255, 20);
            Thread.Sleep(1000);
            lights.ChangeColour("coolDeveloper", 1, 255, 255, 46920);
            Thread.Sleep(1000);
            lights.ChangeColour("coolDeveloper", 1, 255, 255, 20);
            Thread.Sleep(1000);
            lights.ChangeColour("coolDeveloper", 1, 255, 255, 46920);

            //Color color = new Color(red, green, blue);
            //color.GetHue();

            //Assert
        }

        [TestMethod]
        public void Scheduler_()
        {
            //Arrange
            var scheduler = new Scheduler(1, 1000, ip, "coolDeveloper");
            var colors = new List<LightColour>();
            colors.Add(new LightColour(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")));
            colors.Add(new LightColour(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")));
            colors.Add(new LightColour(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")));

            //Act
            scheduler.Start(colors);
            Thread.Sleep(10000);


            //Assert
        }

        [TestMethod]
        public void SchedulerManager_()
        {
            //Arrange
            var scheduler = SchedulerManager.Instance;
            scheduler.CreateScheduler(1, 1000, ip, "coolDeveloper");
            scheduler.CreateScheduler(2, 1000, ip, "coolDeveloper");
            scheduler.CreateScheduler(3, 1000, ip, "coolDeveloper");

            var colors1 = new List<LightColour>();
            colors1.Add(new LightColour(0));
            colors1.Add(new LightColour(46920));
            //Thread.Sleep(new LightColour(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")));
            scheduler.PushNewResults(1, colors1);

            var colors2 = new List<LightColour>();
            colors2.Add(new LightColour(0));
            colors2.Add(new LightColour(12750));
            scheduler.PushNewResults(2, colors2);

            var colors3 = new List<LightColour>();
            colors3.Add(new LightColour(0));
            colors3.Add(new LightColour(56100));
            scheduler.PushNewResults(3, colors3);
            //1, 1000, ip, "coolDeveloper"

            //Act
            Thread.Sleep(30000);


            //Assert
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var ip = "";
            var test = new Lights(ip);

            //Act
            test.RestSharpTest();


            //Assert
        }
    }
}
