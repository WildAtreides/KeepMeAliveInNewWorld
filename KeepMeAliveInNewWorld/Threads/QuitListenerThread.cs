using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using KeepMeAliveInNewWorld.ThreadManagerStuff;

namespace KeepMeAliveInNewWorld.Threads
{
    public class QuitListenerThread : ThreadManagerThread
    {
        public QuitListenerThread(ThreadManager threadManager) : base(threadManager, true) { }

        public override void GetRunLogic()
        {
            ConsoleKeyInfo keyPress = ConsoleReadKey();

            if (keyPress.KeyChar.Equals('q'))
            {
                Stop();
                Console.WriteLine("Quit detected. Shutting down all threads.");
            }
            else
            {
                Console.WriteLine("Key Press detected, but was not 'q'. Press 'q' to quit.");
            }
        }
    }
}
