using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHQKEK.Discordb;
using Console = Colorful.Console;

namespace UHQKEK.Start
{
    class CheckerSettings
    {
        public static bool ProxieViaLink { get; set; } = false;
        public static string ProxyLink { get; set; } = string.Empty;

        public static void checkerset()
        {
            Logo.logo();
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("1", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Load Proxy Via Link - " + ProxieViaLink, Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("2", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Proxy Link - " + ProxyLink, Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("3", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Discord Tag - " + Discordb.Settings.config.DiscordID, Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("4", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Display Hits On Console - " + Discordb.Settings.config.keylo, Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("X", Color.Red);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Go Back", Color.Red);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write(" > Option: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "1":
                    CheckerSettings.EnableProxyLink();
                    break;
                case "2":
                    Console.WriteLine("> Input (Link): ");
                    ProxyLink = Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("> Input (Discord#id): ");
                    Discordb.Settings.settings(true, true);
                    break;
                case "4":
                    Console.WriteLine("> Input (Y | N): ");
                    Discordb.Settings.settings(true, true);
                    break;
                case "X":
                    Program.startmethod();
                    Program.stoter();
                    break;
                default:
                    checkerset();
                    break;
            }
        }
        public static void EnableProxyLink()
        {
            if (ProxieViaLink == true)
            {
                ProxieViaLink = false;
            }
            else
            {
                ProxieViaLink = true;
            }
        }
    }
}
