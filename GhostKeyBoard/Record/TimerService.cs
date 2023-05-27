using System;
using System.Timers;

namespace GhostKeyBoard.Record
{
    public class TimerService
    {
        private const int TIMER_INTERVALL = 40;
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
            this.Timer.Elapsed += OnTimerElapsed;
            this.Timer.Interval = TIMER_INTERVALL;
        }

        private DateTime? startTime;
        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (sender is Timer timer)
            {
                var temp = (e.SignalTime - startTime).Value;
                this.OnTimerTickEvent?.Invoke(sender, temp);
            }
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
    }
}
