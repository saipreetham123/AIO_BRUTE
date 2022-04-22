using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace UHQKEK.Start
{
    class Starter
    {
        public static void Startert()
        {
            try
            {
                Loader();
                GetTypes();
                GetThreads();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void GetThreads()
        {
            Console.WriteLine();
            Console.Write(" > Threads: ", Color.Gold);
            try
            {
                Program.Threads = int.Parse(Console.ReadLine());
            }
            catch
            {
                GetThreads();
            }
        }
        public static void GetTypes()
        {
            Console.WriteLine(">  Proxy Type (1) Http / (2) Socks4 / (3) Socks5: ", Color.Gold);

            switch (Console.ReadLine())
            {
                case "1":
                    Program.Proxytype = ProxyType.HTTP;
                    break;
                case "2":
                    Program.Proxytype = ProxyType.Socks4;
                    break;
                case "3":
                    Program.Proxytype = ProxyType.Socks5;
                    break;
                default:
                    GetTypes();
                    break;
            }
        }
        public static void Loader()
        {
            Logo.logo();
            Console.Write("[ > ] Load Proxies: ", Color.Gold);
            Program.LoadProxie();
            Console.WriteLine($"Imported {Program.Proxies.Count} Proxies ", Color.Gold);
            Console.WriteLine();

            Console.Write("[ > ] Load Combos: ", Color.Gold);
            Program.LoadCombo();
            Program.Total = Program.Combos.Count();
            Console.WriteLine($"Imported {Program.Combos.Count} Combos ", Color.Gold);
            Console.WriteLine();
        }
    }
}
