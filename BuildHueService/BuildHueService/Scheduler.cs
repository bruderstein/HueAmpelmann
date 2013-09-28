using BuildHueService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel.Security;
using System.Threading;
using System.Web;

namespace TeamCityTrafficLightsConfigurator.Management
{
    public class SchedulerManager
    {
        public static SchedulerManager Instance { get { return lazy.Value; } }

        static SchedulerManager()
        {
            ServicePointManager.DefaultConnectionLimit = 10;
        }

        public void CreateScheduler(int lightId, int interval, string ip, string username)
        {
            var scheduler = new Scheduler(lightId, interval, ip, username);
            schedulers.Add(lightId, scheduler);
            var colors = new List<LightColour>();
            colors.Add(new LightColour(25500));
            scheduler.Start(colors);
        }

        public void GetAllSchedulers()
        {

        }

        public void PushNewResults(int lightId, List<LightColour> colors)
        {
             var schedule = schedulers[lightId];
            schedule.NewColors(colors);
        }

 	public void StopAll()
        {
            foreach (var scheduler in schedulers)
            {
                scheduler.Value.Stop();
            }
        }

        private SchedulerManager() { }

        private Dictionary<int, Scheduler> schedulers = new Dictionary<int, Scheduler>();
        private static readonly Lazy<SchedulerManager> lazy = new Lazy<SchedulerManager>(() => new SchedulerManager());
    }

    public class Scheduler
    {
        public Timer timer { get; set;}
        public int lightId;
        public Scheduler(int lightId, int interval, string ip, string username)
        {
            this.lightId = lightId;
            this.interval = interval;
            this.lights = new Lights(ip);
            this.username = username;

        }

        //public void Start(List<int> colors)
        //{
        //    var aTimer = new Timer(1000);
        //    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        //    aTimer.Interval = interval;
        //    aTimer.Enabled = true;

        //    this.colors = colors;

        //}


        public void Start(List<LightColour> colors)
        {
            this.colors = new List<LightColour>(colors);

            timer = new Timer(OnTimedEventThreading, null, 1000, 1000);
            
            
        }

        public void NewColors(List<LightColour> colours)
        {
            lock (padlock)
            {
                this.colors = new List<LightColour>(colours);
                this.currentColorIndex = 0;

            }
        }

        private void OnTimedEventThreading(object target)
        {
            OnTimedEvent(null, null);
        }

        public void Stop()
        {
            if (null != timer)
            {
                timer.Dispose();
            }
        }

        private void OnTimedEvent(object source, /*Elapsed*/EventArgs e)
        {
            LightColour c;
            Debug.WriteLine("locking " + lightId);
            lock (padlock)
            {
                if (currentColorIndex >= colors.Count)
                {
                    currentColorIndex = 0;
                }
                Debug.WriteLine("grabbing colour " + currentColorIndex + " for " + lightId);

            c = colors[currentColorIndex];
            }
            currentColorIndex++;
            Debug.WriteLine("Complete: colour " + currentColorIndex + " for " + lightId);
            Stopwatch starting = new Stopwatch();
            starting.Start();
            lights.ChangeColour(username, lightId, 255, 110, c);
            starting.Stop();
            Debug.WriteLine("Change took " + starting.ElapsedMilliseconds);

        }

        private int interval;
        private int currentColorIndex;
        private Lights lights;
       
        private string username;
        private List<LightColour> colors;
        private readonly object padlock = new object();
        

    }


}
