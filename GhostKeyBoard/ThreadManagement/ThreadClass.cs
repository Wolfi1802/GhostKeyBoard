using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GhostKeyBoard.ThreadManagement
{
    public class ThreadClass
    {
        public ThreadClass(Thread thread, string name, bool isBackground = true)
        {
            if (thread != null)
            {
                this.Thread = thread;
                this.ThreadGuid = new Guid();
                this.ThreadName = name;
                this.ThreadStartTime = DateTime.Now;
                this.Thread.IsBackground = isBackground;
            }
            else
                Debug.WriteLine($"{nameof(ThreadClass)},{nameof(ThreadClass)},{name} ist hier null. Bitte Prüfen!\n{Environment.StackTrace}\n");
        }

        public Thread Thread { private set; get; }

        public Guid ThreadGuid { private set; get; }

        public string ThreadName { private set; get; }

        public DateTime ThreadStartTime { private set; get; }

        public void Start()
        {
            this.Thread.Start();
        }
    }
}
