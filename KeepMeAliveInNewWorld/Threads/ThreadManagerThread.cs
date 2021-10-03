using System;
using System.Collections.Generic;
using System.Text;
using KeepMeAliveInNewWorld.ThreadManagerStuff;

namespace KeepMeAliveInNewWorld.Threads
{
    public abstract class ThreadManagerThread
    {
        protected ThreadManager _threadManager { get; set; }
        private bool _runIsCancelled;
        public bool RunIsCancelled { get { return _runIsCancelled; } set { _runIsCancelled = value; } }

        private bool _killProgramOnExit;

        public ThreadManagerThread(ThreadManager threadManager, bool killProgramOnExit = false)
        {
            _threadManager = threadManager;
            _runIsCancelled = false;
            _killProgramOnExit = killProgramOnExit;
        }
        
        public void Run()
        {
            while (!_runIsCancelled)
            {
                GetRunLogic();
            }

            if (_killProgramOnExit)
            {
                InitiateCancellation();
            }            
        }        

        public abstract void GetRunLogic();

        public void Stop()
        {
            _runIsCancelled = true;
        }

        private void InitiateCancellation()
        {
            _threadManager.IsCancelled = true;
        }

        protected ConsoleKeyInfo ConsoleReadKey()
        {
            return _threadManager.ConsoleReadKey();
        }
    }
}
