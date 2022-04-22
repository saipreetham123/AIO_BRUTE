using CaptchaSharp;
using CaptchaSharp.Services;
using CaptchaSharp.Services.More;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Console = Colorful.Console;

namespace UHQKEK.Start
{
    class Captchas
    {
        public static string CaptchaCredentials { get; set; } = string.Empty;
        public static CaptchaServiceType Service = CaptchaServiceType.TwoCaptcha;
        public static bool CaptchaWhileChecking { get; set; } = false;
        public static decimal Balance { get; set; } = 0;

        public static void setter()
        {
            Logo.logo();
            Console.Write("\n    ( ", Color.GhostWhite);
            Console.Write(">", Color.HotPink);
            Console.Write(" ) ", Color.GhostWhite);
            Console.Write("Captcha Type: " + Service.ToString(), Color.HotPink);
            Console.Write("\n    ( ", Color.GhostWhite);
            Console.Write(">", Color.HotPink);
            Console.Write(" ) ", Color.GhostWhite);
            Console.Write("Captcha Credentials: " + CaptchaCredentials.ToString(), Color.HotPink);
            Console.Write("\n    ( ", Color.GhostWhite);
            Console.Write("C", Color.HotPink);
            Console.Write(" )", Color.GhostWhite);
            Console.Write(" Captcha Enabled: " + CaptchaWhileChecking.ToString(), Color.Yellow);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("1", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("AntiCapKey", Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("2", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("AZCap Key", Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("3", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Captchas IO", Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("4", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("ImageTyperz", Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("5", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("CapMonster", Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("6", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("RuCaptcha", Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("7", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Solve Captcha", Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("8", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Solve Recaptcha", Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("9", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("2Captcha", Color.HotPink);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("10", Color.HotPink);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Death By Captcha", Color.HotPink);
            Console.WriteLine("");
            Console.Write("\n    ( ", Color.GhostWhite);
            Console.Write("B", Color.Lime);
            Console.Write(" ) ", Color.GhostWhite);
            Console.Write("Check Balance: ", Color.Lime);
            Console.Write("\n    [ ", Color.GhostWhite);
            Console.Write("X", Color.Red);
            Console.Write(" ] ", Color.GhostWhite);
            Console.Write("Go Back", Color.Red);
            Console.Write("\n    > Option: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "1":
                    Console.WriteLine(" > Credentials (key) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.AntiCaptcha;
                    break;

                case "2":
                    Console.WriteLine(" > Credentials (key) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.AzCaptcha;
                    break;

                case "3":
                    Console.WriteLine(" > Credentials (key) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.CaptchasIO;
                    break;

                case "4":
                    Console.WriteLine(" > Credentials (key) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.ImageTyperz;
                    break;

                case "5":
                    Console.WriteLine(" > Credentials (key:domain:port) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.CapMonster;
                    break;

                case "6":
                    Console.WriteLine(" > Credentials (key) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.RuCaptcha;
                    break;

                case "7":
                    Console.WriteLine(" > Credentials (key) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.SolveCaptcha;
                    break;

                case "8":
                    Console.WriteLine(" > Credentials (key) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.SolveRecaptcha;
                    break;

                case "9":
                    Console.WriteLine(" > Credentials (key) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.TwoCaptcha;
                    break;

                case "10":
                    Console.WriteLine(" > Credentials (username:password) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.DeathByCaptcha;
                    break;

                /*case "11":
                    Console.WriteLine(" > Credentials (key:domain:port) : ");
                    CaptchaCredentials = Console.ReadLine();
                    Service = CaptchaServiceType.CustomTwoCaptcha;
                    break;*/

                case "C":
                    Captchas.EnableCaptcha();
                    break;

                case "B":
                    Captchas.GetBalance();
                    System.Windows.MessageBox.Show($" Your Captcha Credits Is {Balance} ", " Information ", (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Information);
                    break;

                case "X":
                    Program.startmethod();
                    break;

                default:
                    Captchas.setter();
                    break;
            }
        }
        public enum CaptchaServiceType
        {
            CustomTwoCaptcha,

            TwoCaptcha,

            AntiCaptcha,

            ImageTyperz,

            DeathByCaptcha,

            CapMonster,

            AzCaptcha,

            CaptchasIO,

            RuCaptcha,

            SolveCaptcha,

            SolveRecaptcha,
        }

        public static void EnableCaptcha()
        {
            if (CaptchaWhileChecking == true)
            {
                CaptchaWhileChecking = false;
            }
            else
            {
                CaptchaWhileChecking = true;
            }
        }

        public static void GetBalance()
        {
            try
            {
                Balance = GetService().GetBalanceAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Balance = 0;
                Console.Clear();
                Console.WriteLine(ex);
            }
        }
        public static CaptchaService GetService()
        {
            CaptchaService Solver;

            switch (Service)
            {
                case CaptchaServiceType.AntiCaptcha:
                    Solver = (CaptchaService)new AntiCaptchaService(CaptchaCredentials);
                    break;

                case CaptchaServiceType.AzCaptcha:
                    Solver = (CaptchaService)new AzCaptchaService(CaptchaCredentials);
                    break;

                case CaptchaServiceType.CaptchasIO:
                    Solver = (CaptchaService)new CaptchasIOService(CaptchaCredentials);
                    break;

                case CaptchaServiceType.ImageTyperz:
                    Solver = (CaptchaService)new ImageTyperzService(CaptchaCredentials);
                    break;

                case CaptchaServiceType.CapMonster:

                    var array1 = CaptchaCredentials.Split(new char[] { ':' });
                    Solver = (CaptchaService)new CapMonsterService(array1[0], new Uri($"http://{array1[1]}:{array1[2]}"));

                    break;

                case CaptchaServiceType.DeathByCaptcha:

                    var array2 = CaptchaCredentials.Split(new char[] { ':' });
                    Solver = (CaptchaService)new DeathByCaptchaService(array2[0], array2[1], null);

                    break;

                case CaptchaServiceType.CustomTwoCaptcha:

                    var array3 = CaptchaCredentials.Split(new char[] { ':' });
                    Solver = (CaptchaService)new CustomTwoCaptchaService(array3[0], new Uri($"http://{array3[1]}:{array3[2]}"));

                    break;

                case CaptchaServiceType.RuCaptcha:
                    Solver = (CaptchaService)new RuCaptchaService(CaptchaCredentials);
                    break;

                case CaptchaServiceType.SolveCaptcha:
                    Solver = (CaptchaService)new SolveCaptchaService(CaptchaCredentials);
                    break;

                case CaptchaServiceType.SolveRecaptcha:
                    Solver = (CaptchaService)new SolveRecaptchaService(CaptchaCredentials);
                    break;

                case CaptchaServiceType.TwoCaptcha:
                    Solver = (CaptchaService)new TwoCaptchaService(CaptchaCredentials);
                    break;

                default:
                    throw new NotSupportedException();
            }

            Solver.Timeout = TimeSpan.FromSeconds(120);
            return Solver;
        }
        public static string GetKey(string site, string sitekey)
        {
            try
            {
                string response = GetService().SolveRecaptchaV2Async(sitekey, site, false, null, default).Result.Response;

                if (response.Contains("ERROR_KEY_DOES_NOT_EXIST") || response.Contains("ERROR_ZERO_BALANCE") || response.Contains("ERROR_WRONG_USER_KEY"))
                {
                    return null;
                }
                else
                {
                    ++Program.slovedcaptchas;
                    return response;
                }
            }
            catch (Exception)
            {
                ++Program.captchaerror;
                return null;
            }
        }

        public static string hcapkey(string site, string sitekey)
        {
            try
            {
                string resp = GetService().SolveHCaptchaAsync(sitekey, site).Result.Response;
                if (resp.Contains("ERROR_KEY_DOES_NOT_EXIST") || resp.Contains("ERROR_ZERO_BALANCE") || resp.Contains("ERROR_WRONG_USER_KEY"))
                    return (string)null;
                else
                {
                    ++Program.slovedcaptchas;
                    return resp;
                }
            }
            catch
            {
                ++Program.captchaerror;
                return (string)null;
            }
        }
    }
}
