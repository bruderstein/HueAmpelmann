using System;
using NUnit.Framework;
using TeamCityTrafficLightsConfigurator.Management;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;

namespace LightsUnitTest
{
    [TestFixture]
    public class UnitTest1
    {

        private static string ip = "http://192.168.1.134";
        [Test]
        public void FirstTest_ShouldPass()
        {
            //Arrange
            var lights = new Lights(ip);

            //Act
            var result = lights.DeveloperTest();


            //Assert
        }

        [Test]
        public void CreateNewDeveloper_ShouldPass()
        {
            //Arrange
            var lights = new Lights(ip);

            //Act
            var result = lights.CreateNewDeveloper("testuser", "coolDeveloper");


            //Assert
        }

        [Test]
        public void GetAll_ShouldPass()
        {
            //Arrange
            var lights = new Lights(ip);

            //Act
            var result = lights.GetAll("coolDeveloper");


            //Assert
        }

        [Test]
        public void Get_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.Get("coolDeveloper", 1);


            //Assert
        }

        [Test]
        public void TurnOn_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.TurnOn("coolDeveloper", 3);


            //Assert
        }

        [Test]
        public void TurnOff_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.TurnOff("coolDeveloper", 3);


            //Assert
        }

        [Test]
        public void GetIds_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.GetIds("coolDeveloper");

            //Assert
            Assert.AreEqual(result.Count, 3);
        }
        

        [Test]
        public void ChangeColour_ShouldPass()
        {
            //Arrange

            var lights = new Lights(ip);

            //Act
            var result = lights.ChangeColour("coolDeveloper", 1 , 255, 255, 20);
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

        [Test]
        public void Scheduler_()
        {
            //Arrange
            var scheduler = new Scheduler(1, 1000, ip, "coolDeveloper");
            var colors = new List<int>();
            colors.Add(0);
            colors.Add(12750);
            colors.Add(46920);

            //Act
            scheduler.Start(colors);
            Thread.Sleep(10000);

            scheduler.Stop();
            //Assert
        }

        [Test]
        public void SchedulerManager_()
        {
            //Arrange
            var scheduler = SchedulerManager.Instance;
            scheduler.CreateScheduler(1, 1000, ip, "coolDeveloper");
            scheduler.CreateScheduler(2, 1000, ip, "coolDeveloper");
            scheduler.CreateScheduler(3, 1000, ip, "coolDeveloper");

            var colors1 = new List<int>();
            colors1.Add(0);
            colors1.Add(46920);
            Thread.Sleep(2500);
            scheduler.PushNewResults(1, colors1);

            var colors2 = new List<int>();
            colors2.Add(0);
            colors2.Add(12750);
            scheduler.PushNewResults(2, colors2);

            var colors3 = new List<int>();
            colors3.Add(0);
            colors3.Add(56100);
            scheduler.PushNewResults(3, colors3);
            //1, 1000, ip, "coolDeveloper"

            //Act
            Thread.Sleep(30000);


            scheduler.StopAll();
            //Assert
        }
        
        [Test]
        public void Test1()
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
