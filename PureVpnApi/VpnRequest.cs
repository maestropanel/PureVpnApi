namespace PureVpnApi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Linq;

    public class VpnRequest
    {
        private readonly string API_HOST = "https://reseller.purevpn.com/";
        private Dictionary<string, string> args;
        
        public VpnRequest()
        {
            
            args = new Dictionary<string, string>();
        }

        public VpnRequest Authentication(string apiUsername, string apiPassword)
        {            
            AddValue("api_user", apiUsername);
            AddValue("api_password", apiPassword);

            return this;
        }

        public VpnRequest Username(string vpnUsername)
        {
            AddValue("user", vpnUsername);

            return this;
        }

        public VpnRequest Password(string vpnPassword)
        {
            AddValue("vpn_password", vpnPassword);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageType">STANDARD,SSTP,HIGH-BW</param>
        /// <returns></returns>
        public VpnRequest Package(string packageType)
        {
            AddValue("package_type", packageType);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Days">#30,#90,#180,#365</param>
        /// <returns></returns>
        public VpnRequest Period(int Days)
        {
            AddValue("period", String.Format("#{0}",Days));
            return this;
        }


        public VpnResponse Create()
        {
            AddValue("method", "create");

            return Send();
        }

        /// <summary>
        /// Username required
        /// </summary>
        /// <returns></returns>
        public VpnResponse Renew()
        {
            AddValue("method", "renew");
            return Send();
        }

        /// <summary>
        /// Username required
        /// </summary>
        /// <returns></returns>
        public VpnResponse FindStatus()
        {
            AddValue("method", "status");
            return Send();
        }


        /// <summary>
        /// Username required
        /// </summary>
        /// <returns></returns>
        public VpnResponse Delete()
        {
            AddValue("method", "delete");
            return Send();
        }

        /// <summary>
        /// Username required
        /// </summary>
        /// <param name="updateStatus">enable,disable</param>
        /// <returns></returns>
        public VpnResponse Update(string updateStatus)
        {
            AddValue("method", "update_status");
            AddValue("update_status", updateStatus);

            return Send();
        }

        /// <summary>
        /// Username required
        /// </summary>
        /// <param name="newPassword">New Password, Must be Alphanumeric and length should be 8 characters</param>
        /// <returns></returns>
        public VpnResponse ChangePassword(string newPassword)
        {
            AddValue("method", "change_password");
            AddValue("new_pass", newPassword);

            return Send();
        }

        private VpnResponse Send()
        {
            var remoteResponse = SendHttpRequest(API_HOST, "POST", args.ToString());
            var responseObject = DeSerializeObject<VpnResponse>(remoteResponse);

            return responseObject;
        }
        
        private string SendHttpRequest(string Host, string Method, string Params)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate
                                                                            (object sender, X509Certificate certificate, X509Chain chain,
                                                                            SslPolicyErrors sslPolicyErrors)
            { return true; };

            var returnSrting = String.Empty;

            var request = (HttpWebRequest)WebRequest.Create(Host);
            request.Timeout = 30000;
            request.Method = Method;

            var bytes = new ASCIIEncoding().GetBytes(Params);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Params.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }

            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                returnSrting = sr.ReadToEnd();
            }

            return returnSrting;
        }

        private void AddValue(string name, string value)
        {
            if (args.ContainsKey(name))
                args[name] = value;
            else
                args.Add(name, value);
        }   

        private string ToString()
        {
          var pairs = args.Select(x => String.Format("{0}={1}", x.Key, x.Value)).ToArray();
          return string.Join("&", pairs);
        }

        private T DeSerializeObject<T>(string xmlData)
        {
            T deSerializeObject = default(T);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(xmlData);
            XmlReader XR = new XmlTextReader(stringReader);

            if (xmlSerializer.CanDeserialize(XR))
            {
                deSerializeObject = (T)xmlSerializer.Deserialize(XR);
            }

            return deSerializeObject;
        }

    }
}
