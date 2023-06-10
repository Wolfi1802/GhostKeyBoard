using System;
using System.Diagnostics;
using System.Timers;

namespace GhostKeyBoard.Record
{
    public class TimerService
    {
        private const int TIMER_INTERVALL = 100;
        private static TimerService _instance;

        public static TimerService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimerService();

                return _instance;
            }
        }

        public EventHandler<TimeSpan?> OnTimerTickEvent;

        private Timer Timer { get; set; } = new Timer() { };
        private DateTime? startTime;

        public TimerService()
        {
            this.Timer.Elapsed += OnTimerElapsed;
            this.Timer.Interval = TIMER_INTERVALL;
        }

        public void Start()
        {
            this.Timer.Start();
            this.startTime = DateTime.Now;
        }

        public void Stop()
        {
            this.Timer.Stop();
            this.startTime = null;
        }

        public TimeSpan GetTime()
        {
            return (DateTime.Now - startTime).Value;
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (sender is Timer timer)
            {
                var temp = (e.SignalTime - startTime).Value;
                Debug.WriteLine($"timespan vor event:{temp}");

                if (HookService.Instance.IsPlay)
                    HookService.Instance.HandleMakroLineup(temp);
            }
        }
    }
}
