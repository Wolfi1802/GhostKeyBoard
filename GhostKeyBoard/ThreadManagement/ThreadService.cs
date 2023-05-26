

using System;
using System.Collections.Generic;
using System.Threading;

namespace GhostKeyBoard.ThreadManagement
{
    public class ThreadService
    {
        private List<ThreadClass> Threads = new List<ThreadClass>();

        private static ThreadService singelton;
        public static ThreadService Instance
        {
            get
            {
                if (singelton == null)
                    singelton = new ThreadService();
                return singelton;
            }
        }

        private ThreadService()
        {

        }

        /// <summary>
        /// Erzeugt einen neuen Thread, fügt die <paramref name="method"/> hinein und startet diese.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="name"></param>
        /// <param name="IsStaThread"></param>
        public void StartThread(Action method, string name, bool IsStaThread = false)
        {
            Thread thread = new Thread(() =>
            {
                method();
            });

            thread.Name = name;

            if (IsStaThread)
                thread.SetApartmentState(ApartmentState.STA);

            this.AddToPool(thread);
        }

        /// <summary>
        /// Fügt einen Thread zur Liste hinzu und startet diesen.
        /// </summary>
        /// <param name="thread"></param>
        private void AddToPool(Thread thread)
        {
            if (thread == null)
                System.Diagnostics.Debug.WriteLine($"{nameof(ThreadService)},{nameof(AddToPool)},[{nameof(thread)}] == null");
            else if (thread.ThreadState == ThreadState.Running)
                System.Diagnostics.Debug.WriteLine($"{nameof(ThreadService)},{nameof(AddToPool)},[{nameof(thread)}] wurde gestartet!");

            ThreadClass model = new ThreadClass(thread, thread.Name);
            model.Start();

            this.Threads.Add(model);
        }
    }
}
