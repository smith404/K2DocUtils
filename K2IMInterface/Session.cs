/*
 * Copyright (c) 2021. K2-Software
 * All software, both binary and source published by K2-Software (hereafter, Software)
 * is copyrighted by the author (hereafter, K2-Software) and ownership of all right, 
 * title and interest in and to the Software remains with K2-Software. By using or 
 * copying the Software, User agrees to abide by the terms of this Agreement.
 */

using K2IManageObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace K2IMInterface
{
    public sealed class IMSession
    {
        private static readonly object padlock = new object();
        private static IMSession instance = null;

        public delegate bool ErrorCallback(Exception ex);
        public ErrorCallback ErrorHandler { get; set; }

        public string BaseURI { get; set; }

        public string IMToken { get; set; }

        public string APIVersion { get; set; }


        private IMSession()
        {
            ErrorHandler = null;
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

        public string DecorateRESTCall(string url)
        {
            return BaseURI + APIVersion + url;
        }

        public List<IMWorkspace> Workspaces()
        {
            string url = "workspaces/search";
            List<IMWorkspace> workspaces = null;

            try
            {
                string json = MakeGetCall(DecorateRESTCall(url));
                if (json.Length > 0)
                {
                    workspaces = JsonConvert.DeserializeObject<IMItemList<IMWorkspace>>(json).Data;
                }
            }
            catch(Exception)
            {
                // Not much to be done
            }

            return workspaces;
        }

        public string ConstructDocumentDownload(IMDBObject doc)
        {
            string url = "";

            if (doc != null)
            {
                url = "documents/" + doc.Id + "/download";
            }

            return DecorateRESTCall(url);
        }

        public string ConstructNewVersion(IMDBObject doc)
        {
            string url = "";

            if (doc != null)
            {
                url = "documents/" + doc.Id + "/versions";
            }

            return DecorateRESTCall(url);
        }

        public string ConstructDocumentGet(IMDBObject doc)
        {
            string url = "";

            if (doc != null)
            {
                url = "documents/" + doc.Id;
            }

            return DecorateRESTCall(url);
        }

        public string ConstructEmailGet(IMDBObject doc)
        {
            string url = "";

            if (doc != null)
            {
                url = "email/" + doc.Id;
            }

            return DecorateRESTCall(url);
        }

        public string ConstructSearchTerm(string query, string term)
        {
            return ConstructSearchTerm(query, term, 0, 25, false);
        }

            public string ConstructSearchTerm(string query, string term, int offset, int limit, bool total)
        {
            var uri = new StringBuilder(query);

            uri.Append("?anywhere=");
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

        public IMUser WhoAmI()
        {
            string uri = DecorateRESTCall("users/me");
            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(uri);

                req.PreAuthenticate = true;
                req.Headers.Add("Authorization", "Bearer " + IMToken);
                req.Accept = "application/json";
                req.Method = "GET";
                req.Timeout = 60000;

                System.Net.WebResponse resp = req.GetResponse();

                if (resp == null) return null;

                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

                string json = sr.ReadToEnd().Trim();

                return JsonConvert.DeserializeObject<IMItem<IMUser>>(json).Data;
            }
            catch(Exception ex)
            {
                if (ErrorHandler != null)
                {
                    bool handled = ErrorHandler(ex);
                    if (!handled)
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }

            return null;
        }

        public string MakeGetCall(string uri)
        {
            uri = DecorateRESTCall(uri);

            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(uri);

                req.PreAuthenticate = true;
                req.Headers.Add("Authorization", "Bearer " + IMToken);
                req.Accept = "application/json";
                req.Method = "GET";
                req.Timeout = 60000;

                System.Net.WebResponse resp = req.GetResponse();

                if (resp == null) return null;

                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

                return sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                if (ErrorHandler != null)
                {
                    bool handled = ErrorHandler(ex);
                    if (!handled)
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }

            return "";
        }
    }
}
