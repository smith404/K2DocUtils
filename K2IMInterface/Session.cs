using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace K2IMInterface
{
    public sealed class IMSession
    {
        private static IMSession instance = null;

        private static readonly object padlock = new object();

        private IMSession()
        {
            APIVersion = "api/v1/";
            IMToken = "";
            BaseURI = "https://localhost/";
        }

        public static IMSession Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new IMSession();
                        }
                    }
                }

                return instance;
            }
        }

        public string BaseURI { get; set; }

        public string IMToken { get; set; }

        public string APIVersion { get; set; }

        public string ConstructDocDownload(string id)
        {
            return BaseURI + APIVersion + "documents/" + id + "/download";
        }

        public string ConstructDocSearch(string term, int offset, int limit, bool total)
        {
            var uri = new StringBuilder();

            uri.Append(BaseURI);
            uri.Append(APIVersion);
            uri.Append("documents/search?anywhere=");
            uri.Append(term);

            if (offset > 0)
            {
                uri.Append("&offset=");
                uri.Append(offset);
            }

            if (offset != 25)
            {
                uri.Append("&limit=");
                uri.Append(limit);
            }

            if (total) uri.Append("&total=true");

            return uri.ToString();
        }

        public string MakeGetCall(string uri)
        {
            uri = BaseURI + APIVersion + uri;
            HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(uri);

            req.PreAuthenticate = true;
            req.Headers.Add("Athorization", "Bearer " + IMToken);
            req.Accept = "application/json";
            req.Method = "GET";
            req.Timeout = 60000;

            System.Net.WebResponse resp = req.GetResponse();

            if (resp == null) return null;

            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

            return sr.ReadToEnd().Trim();
        }
    }
}
