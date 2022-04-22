using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace UHQKEK.Start
{
    class Threading
    {
        public static List<Func<string, bool>> Worker = new List<Func<string, bool>>();
        public static void DoneThread()
        {
            Console.Title = string.Format("DIVINE AIO - Done {0} / {1} Valids ", Program.Good, Program.Good);
            Thread.Sleep(-1);
        }
        public static string SetProxies()
        {
            return Program.Proxies.ElementAt(Program.Rnd.Next(0, Program.Proxies.Count)); ;
        }
        public static void RunnerView()
        {

        }
        public static void Starter()
        {
            Threading.Thread2(new ThreadStart(Process));
            Threading.Thread1();
            Threading.DoneThread();
        }
        public static void Process()
        {
            while (!Program.Combos.IsEmpty)
            {
                string result;
                if (Program.Combos.TryDequeue(out result))
                {
                    foreach (Func<string, bool> method in Worker)
                    {
                        while (!method(result))
                        {
                            Thread.Sleep(1);
                            Thread.Sleep(1);
                        }
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        public static void Thread1()
        {
            Thread Cpm = new Thread(Program.Elapsed);
            Cpm.Start();
            Program.lines_in_use = true;
            AutoSave.save();
            Logo.logo();
            Program.Folder();
            Program.Title();


            for (int index = 0; index < Program.ThreadBase.Count; ++index)
            {
                Program.ThreadBase[index].Start();
            }
            for (int index = 0; index < Program.ThreadBase.Count; ++index)
            {
                Program.ThreadBase[index].Join();
            }
        }

        public static void Thread2(ThreadStart Method)
        {
            for (int index = 0; index < Program.Threads; ++index)
            {
                Program.ThreadBase.Add(new Thread(Method));
            }
        }
    }
}
