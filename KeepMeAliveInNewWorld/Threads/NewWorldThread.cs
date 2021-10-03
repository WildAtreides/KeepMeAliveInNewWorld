using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using KeepMeAliveInNewWorld.ThreadManagerStuff;
using WindowsInput;

namespace KeepMeAliveInNewWorld.Threads
{
    public class NewWorldThread : ThreadManagerThread
    {
        private KeyboardSimulator KeyboardSimulator;

        private const int ShortSleep = 200;
        private const int OneSecond = 1000;
        private int OneMinute => OneSecond * 60;
        private int GetSeconds(int seconds) => seconds * OneSecond;
        private int GetMinutes(int minutes) => minutes * OneMinute;

        private const WindowsInput.Native.VirtualKeyCode W_KEY = WindowsInput.Native.VirtualKeyCode.VK_W;
        private const WindowsInput.Native.VirtualKeyCode A_KEY = WindowsInput.Native.VirtualKeyCode.VK_A;
        private const WindowsInput.Native.VirtualKeyCode S_KEY = WindowsInput.Native.VirtualKeyCode.VK_S;
        private const WindowsInput.Native.VirtualKeyCode D_KEY = WindowsInput.Native.VirtualKeyCode.VK_D;
        private const WindowsInput.Native.VirtualKeyCode C_KEY = WindowsInput.Native.VirtualKeyCode.VK_C;

        private const WindowsInput.Native.VirtualKeyCode J_KEY = WindowsInput.Native.VirtualKeyCode.VK_J;
        private const WindowsInput.Native.VirtualKeyCode K_KEY = WindowsInput.Native.VirtualKeyCode.VK_K;
        private const WindowsInput.Native.VirtualKeyCode TAB_KEY = WindowsInput.Native.VirtualKeyCode.TAB;

        public NewWorldThread(ThreadManager threadManager) : base(threadManager)
        {
            InitializeSimulator();
        }

        public override void GetRunLogic()
        {
            Console.WriteLine("Waiting 30 seconds");
            GetWaitTimer(GetSeconds(30));
            RunOpeningSequence();
            MainWait();
            RunExitingSequence();
            Thread.Sleep(ShortSleep);
        }

        private void GetWaitTimer(int time)
        {
            TimeSpan blah = TimeSpan.FromMilliseconds(time);
            Console.WriteLine();
            //DateTime now = DateTime.Now;
            //string dateString = now.ToString("mm:ss");
            Console.Write(blah.ToString(@"mm\:ss"));

            bool isCancelled = false;
            int timeCopy = time;

            while (!isCancelled && !RunIsCancelled)
            {
                //now = DateTime.Now;
                //dateString = now.ToString("mm:ss");
                ClearCurrentConsoleLine();
                blah = TimeSpan.FromMilliseconds(timeCopy);
                Console.Write(blah.ToString(@"mm\:ss"));

                if (timeCopy >= 100)
                {
                    Thread.Sleep(100);
                    timeCopy = timeCopy - 100;
                }
                else
                {
                    isCancelled = true;
                }
            }

            Console.WriteLine();
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private void MainWait()
        {
            int minutes = 1;
            //int minutes = GetMinutes(GetRandomValueInRange(4, 13));
            int seconds = GetSeconds(GetRandomValueInRange(2, 59));

            Console.WriteLine($"{GetCurrentTime()} - Waiting for {minutes}min and {seconds}sec");
            //Thread.Sleep(minutes + seconds);


            GetWaitTimer(minutes + seconds);
        }

        private string GetCurrentTime()
        {
            DateTime now = DateTime.Now;
            return now.ToString("HH:mm:ss.ffff");
        }

        private void RunOpeningSequence()
        {
            int numberOfActions = GetRandomValueInRange(1, 4);
            Console.WriteLine($"Generating {numberOfActions} random sequences");

            for (int i = 0; i < numberOfActions; i++)
            {
                if (RunIsCancelled)
                {
                    return;
                }
                RunRandomAction();
                if (RunIsCancelled)
                {
                    return;
                }
            }
        }

        private void RunExitingSequence()
        {
            int numberOfActions = GetRandomValueInRange(1, 4);
            Console.WriteLine($"Generating {numberOfActions} random sequences");

            for (int i = 0; i < numberOfActions; i++)
            {
                if (RunIsCancelled)
                {
                    return;
                }
                RunRandomAction();
                if (RunIsCancelled)
                {
                    return;
                }
            }
        }

        public void RunRandomAction()
        {
            Random random = new Random();
            int blah = random.Next(1, 100);

            if (blah > 50)
            {
                Console.WriteLine("Running Menu Action");
                MenuAction();
                Console.WriteLine("End Menu Action");
            }
            else
            {
                Console.WriteLine("Running Move Action");
                MoveAction();
                Console.WriteLine("End Move Action");
            }
        }

        private void MenuAction()
        {
            WindowsInput.Native.VirtualKeyCode key = GetRandomMenuKey();
            DateTime now = DateTime.Now;
            Console.WriteLine($"{now.ToString("HH:mm:ss.ffff")} - Opening {GetKeyString(key)} Menu");
            OpenMenu(key);
            Console.WriteLine($"{now.ToString("HH:mm:ss.ffff")} - Closing {GetKeyString(key)} Menu");
        }

        private void MoveAction()
        {
            int numberOfMovements = GetRandomValueInRange(2, 9);

            for (int i = 0; i < numberOfMovements; i++)
            {
                if (RunIsCancelled)
                {
                    return;
                }
                Move();
                if (RunIsCancelled)
                {
                    return;
                }
                Thread.Sleep(ShortSleep);
            }
        }

        private void Move()
        {
            WindowsInput.Native.VirtualKeyCode keyCode = GetRandomDirectionKey();

            DateTime now = DateTime.Now;
            Console.WriteLine($"{now.ToString("HH:mm:ss.ffff")} - Moving {GetKeyString(keyCode)}");
            KeyDownUp(keyCode, GetSeconds(GetRandomMovementTime()));
        }

        private int GetRandomMovementTime()
        {
            return GetRandomValueInRange(1, 9);
        }

        private WindowsInput.Native.VirtualKeyCode GetRandomDirectionKey()
        {
            Random random = new Random();
            int direction = random.Next(1, 4);

            switch (direction)
            {
                case 1:
                    return W_KEY;
                case 2:
                    return A_KEY;
                case 3:
                    return S_KEY;
                case 4:
                    return D_KEY;
                default:
                    return W_KEY;
            }
        }

        private WindowsInput.Native.VirtualKeyCode GetRandomMenuKey()
        {
            Random random = new Random();
            int direction = random.Next(1, 3);

            switch (direction)
            {
                case 1:
                    return TAB_KEY;
                case 2:
                    return J_KEY;
                case 3:
                    return K_KEY;
                default:
                    return TAB_KEY;
            }
        }

        private string GetKeyString(WindowsInput.Native.VirtualKeyCode keyCode)
        {
            string keyString = string.Empty;

            switch (keyCode)
            {
                case WindowsInput.Native.VirtualKeyCode.VK_W:
                    return "W";
                case WindowsInput.Native.VirtualKeyCode.VK_A:
                    return "A";
                case WindowsInput.Native.VirtualKeyCode.VK_S:
                    return "S";
                case WindowsInput.Native.VirtualKeyCode.VK_D:
                    return "D";
                case WindowsInput.Native.VirtualKeyCode.VK_C:
                    return "C";
                case WindowsInput.Native.VirtualKeyCode.VK_J:
                    return "J";
                case WindowsInput.Native.VirtualKeyCode.VK_K:
                    return "K";
                case WindowsInput.Native.VirtualKeyCode.TAB:
                    return "TAB";
            }

            return keyString;
        }

        private void OpenMenu(WindowsInput.Native.VirtualKeyCode menuKey)
        {
            KeyPress(menuKey);
            Thread.Sleep(GetSeconds(GetRandomMenuOpenInterval()));
            KeyPress(menuKey);
        }

        private int GetRandomMenuOpenInterval()
        {
            return GetRandomValueInRange(1, 5);
        }

        private int GetRandomValueInRange(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private void KeyPress(WindowsInput.Native.VirtualKeyCode keyCode)
        {
            KeyboardSimulator.KeyPress(keyCode);
        }

        private void KeyDownUp(WindowsInput.Native.VirtualKeyCode keyCode, int sleepTime)
        {
            KeyboardSimulator.KeyDown(keyCode);
            Thread.Sleep(sleepTime);
            KeyboardSimulator.KeyUp(keyCode);
        }

        private void InitializeSimulator()
        {
            InputSimulator inputSimulator = new InputSimulator();
            KeyboardSimulator = new KeyboardSimulator(inputSimulator);
        }
    }
}
