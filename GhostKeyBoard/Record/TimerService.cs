using System;
using System.Timers;

namespace GhostKeyBoard.Record
{
    internal class TimerService
    {
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

        public TimerService()
        {
            Timer.Elapsed += OnTimerElapsed; ;
            Timer.Interval = 200;//todo [TS] maybe 1k ms better to prevent ressource killer
        }

        private DateTime? startTime;
        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (sender is Timer timer)
            {
                var temp = (e.SignalTime - startTime).Value;
                OnTimerTickEvent?.Invoke(sender, temp);
            }
        }

        public void Start()
        {
            Timer.Start();
            startTime = DateTime.Now;
        }

        public void Stop()
        {
            Timer.Stop();
            startTime = null;
        }
    }
}
