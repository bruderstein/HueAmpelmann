using BuildHueService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace TeamCityTrafficLightsConfigurator.Management
{
    public class SchedulerManager
    {
        public static SchedulerManager Instance { get { return lazy.Value; } }
        

        public void CreateScheduler(int lightId, int interval, string ip, string username)
        {
            var scheduler = new Scheduler(lightId, interval, ip, username);
            schedulers.Add(scheduler);
            var colors = new List<LightColour>();
            colors.Add(new LightColour(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")));
            scheduler.Start(colors);
        }

        public void GetAllSchedulers()
        {

        }

        public void PushNewResults(int lightId, List<LightColour> colors)
        {


            var schedule = schedulers.Where(c => c.lightId == lightId).FirstOrDefault();
            schedule.NewColors(colors);
        }

        public void PushNewResults(LightStatus status)
        {

            var schedule = schedulers.Where(c => c.lightId == status.LightId).FirstOrDefault();
            schedule.NewColors(status.Colours);
        }

        private SchedulerManager() { }

        private List<Scheduler> schedulers = new List<Scheduler>();
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

            var aTimer = new Timer(OnTimedEventThreading, null, 1000, 1000);
            
            
        }

        public void NewColors(List<LightColour> colours)
        {
            lock (padlock)
            {
                this.colors = new List<LightColour>(colors);
                this.currentColorIndex = 0;

            }
        }
        public void NewColors(IEnumerable<LightColour> colors)
        {
            lock (padlock)
            {
                this.colors = new List<LightColour>(colors);
                this.currentColorIndex = 0;

            }
        }

        private void OnTimedEventThreading(object target)
        {
            OnTimedEvent(null, null);
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
            Debug.WriteLine("Complete: colour " + currentColorIndex + " for " + lightId);
            lights.ChangeColour(username, lightId, 255, 110, c);
            currentColorIndex++;
            
        }

        private int interval;
        private int currentColorIndex;
        private Lights lights;
       
        private string username;
        private List<LightColour> colors;
        private readonly object padlock = new object();
        

    }


}