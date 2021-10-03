using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using KeepMeAliveInNewWorld.Threads;

namespace KeepMeAliveInNewWorld.ThreadManagerStuff
{
    /*NOTES:
     * ThreadManager's job is to run threads until it receives a cancellation token. Rather, once something updates a _isCancelled's value to true.
     */
    public abstract class ThreadManager
    {
        public abstract List<ThreadManagerThread> GetThreads();
        public abstract void RunStartup();
        public abstract void RunExit();

        private List<Thread> _threads;
        private List<ThreadManagerThread> _threadManagerThreads;
        private bool _isCancelled;
        public bool IsCancelled { get { return _isCancelled; } set { _isCancelled = value; } }

        public ThreadManager()
        {
            _threads = new List<Thread>();
            _isCancelled = false;
            _threadManagerThreads = GetThreads();

            foreach (ThreadManagerThread threadManagerThread in _threadManagerThreads)
            {
                _threads.Add(new Thread(new ThreadStart(threadManagerThread.Run)));
            }
        }

        public void Run()
        {
            RunStartup();

            if (_threads.Count < 1)
            {
                Console.WriteLine("No threads to start have been detected.");
                ExitPrompt();
                return;
            }

            StartThreads();
            RunMain();
            CleanupThreads();
            RealRunExit();
        }

        private void StartThreads()
        {
            foreach (Thread thread in _threads)
            {
                thread.Start();
            }
        }

        protected virtual void RunMain()
        {
            while (!_isCancelled)
            {
                Thread.Sleep(300);
            }
        }

        private void CleanupThreads()
        {
            foreach (ThreadManagerThread threadManagerThread in _threadManagerThreads)
            {
                threadManagerThread.Stop();
            }

            foreach (Thread thread in _threads)
            {
                //kill thread?
                if (thread.ThreadState == ThreadState.Running)
                {
                    thread.IsBackground = true;
                }
            }
        }

        private void RealRunExit()
        {
            RunExit();
            ExitPrompt();
        }

        private void ExitPrompt()
        {
            Console.WriteLine("Press any key to exit.");
            ConsoleReadKey();
        }

        public ConsoleKeyInfo ConsoleReadKey()
        {
            ConsoleKeyInfo keyPress = Console.ReadKey();
            Console.WriteLine();

            return keyPress;
        }
    }
}
