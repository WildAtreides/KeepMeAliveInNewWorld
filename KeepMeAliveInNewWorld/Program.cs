using System;
using System.Text;
using System.Threading;
using KeepMeAliveInNewWorld.ThreadManagerStuff;
using KeepMeAliveInNewWorld.Threads;

namespace KeepMeAliveInNewWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            NewWorldThreadManager threadManager = new NewWorldThreadManager();
            threadManager.Run();
        }
    }
}
