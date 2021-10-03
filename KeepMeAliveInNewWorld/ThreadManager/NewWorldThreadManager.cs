using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using KeepMeAliveInNewWorld.Threads;

namespace KeepMeAliveInNewWorld.ThreadManagerStuff
{
    /*NOTES:
     * An implementation of ThreadManager is a "unique blend" of different Threads available in this project.
     * All this class does is specify that the QuitListenerThread and NewWorldThread will be running if this manager is selected.
     */
    public class NewWorldThreadManager : ThreadManager
    {
        public NewWorldThreadManager() { }

        public override List<ThreadManagerThread> GetThreads() => new List<ThreadManagerThread>()
        {
            new QuitListenerThread(this),
            new NewWorldThread(this)
        };

        public override void RunStartup()
        {
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("Hello! Are you trying to secure your space on a New World [or some other MMO] server?");
            Thread.Sleep(300);
            messageBuilder.AppendLine("If so, please press one of the following keys.");
            Thread.Sleep(300);
            messageBuilder.AppendLine("If you choose 'yes', then you will have 30 seconds to switch focus into the game (click in the game)");
            Thread.Sleep(300);
            messageBuilder.AppendLine("'y' = yes");
            messageBuilder.AppendLine("'n' = no");
            Console.WriteLine(messageBuilder.ToString());

            ConsoleKeyInfo keyPress = ConsoleReadKey();
            bool yesOrNo = false;

            while (!yesOrNo)
            {
                Console.WriteLine();
                Console.WriteLine();

                if (keyPress.KeyChar.Equals('y') || keyPress.KeyChar.Equals('n'))
                {
                    yesOrNo = true;
                }
                else
                {
                    Console.WriteLine($"Did you not read what I said before? Please use either the key 'y' or 'n' to continue.");
                }
            }

            Console.WriteLine("Starting threads.");
            Console.WriteLine("You now have 30 seconds to switch focus to New World.");
        }

        public override void RunExit()
        {
            Console.WriteLine("You have awoken my restless slumber.");
            Console.WriteLine("You are now in control.");
        }
    }
}
