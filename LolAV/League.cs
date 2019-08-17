using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LolAV
{
    class League
    {
        static readonly HttpClient client = new HttpClient();
        static string authRegexPattern = @"""--remoting-auth-token=(?'token'.*?)"" | ""--app-port=(?'port'|.*?)""";
        static RegexOptions authRegexOptions = RegexOptions.Multiline;

        public static void SetIcon(string id, IconPick v)
        {
            v.statusMessage.Invoke((MethodInvoker)delegate
            {
                v.statusMessage.Text = "Connecting to League Client...";
            });
            try
            {
                var auth = League.GetAuth();
                v.statusMessage.Invoke((MethodInvoker)delegate
                {
                    v.statusMessage.Text = "Sending request...";
                });
                League.SendRequest(auth.Item1, auth.Item2, id);
                v.statusMessage.Invoke((MethodInvoker)delegate
                {
                    v.statusMessage.Text = "Icon was set successfully.";
                });
            } catch (Exception)
            {
                v.statusMessage.Invoke((MethodInvoker)delegate
                {
                    v.statusMessage.Text = "Can't connect to League Client. Is it on?";
                });
            }
        }
        private static (String, String) GetAuth()
        {
            String token = "";
            String port = "";
            var mngmt = new ManagementClass("Win32_Process");
            foreach (ManagementObject o in mngmt.GetInstances())
            {
                if (o["Name"].Equals("LeagueClientUx.exe"))
                {
                    //Console.WriteLine(o["CommandLine"]);
                    

                    foreach(Match m in Regex.Matches(o["CommandLine"].ToString(), League.authRegexPattern, League.authRegexOptions))
                    {
                        if(!String.IsNullOrEmpty(m.Groups["port"].ToString()))
                        {
                            port = m.Groups["port"].ToString();
                        } else if (!String.IsNullOrEmpty(m.Groups["token"].ToString()))
                        {
                            token = m.Groups["token"].ToString();
                        }
                    }
                    //return o["CommandLine"].GetType().ToString();
                }
            }
            if (String.IsNullOrEmpty(token) || String.IsNullOrEmpty(port))
            {
                throw new Exception("No League client found");
            }

            return (token, port);
        }
        private static void SendRequest(string token, string port, string id)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new RestClient("https://127.0.0.1:" + port);
            client.Authenticator = new HttpBasicAuthenticator("riot", token);
            var req = new RestRequest("/lol-summoner/v1/current-summoner/icon", Method.PUT);
            req.AddJsonBody(new { profileIconId = id});
            var res = client.Execute(req);
        }
    }
}
