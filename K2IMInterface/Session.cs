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
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace K2IMInterface
{
    [Serializable]
    public class IMException : Exception
    {
        public static int GENERAL_ERROR = 0;
        public static int BAD_USER_INFO = 1000;
        public static int BAD_GET_CALL = 1001;
        public static int BAD_DOWNLOAD_CALL = 1001;
        public static int BAD_UPLOAD_CALL = 1001;

        public int ErrorCode { get; set; }

        public IMException() : base() { }
        public IMException(string message) : base(message) { }
        public IMException(string message, Exception inner) : base(message, inner) { }
        public IMException(string message, Exception inner, int code) : base(message, inner) { ErrorCode = code; }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected IMException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public sealed class IMConnection
    {
        private static readonly object padlock = new object();
        private static IMConnection instance = null;

        public delegate bool ErrorCallback(Exception ex);
        public ErrorCallback ErrorHandler { get; set; }

        public string BaseURI { get; set; }

        public string IMToken { get; set; }

        public string APIVersion { get; set; }


        private IMConnection()
        {
            ErrorHandler = null;
            APIVersion = "api/v1/";
            IMToken = "";
            BaseURI = "https://localhost/";
        }

        public static IMConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new IMConnection();
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
                string json = PerformGetCall(DecorateRESTCall(url));
                if (json.Length > 0)
                {
                    workspaces = JsonConvert.DeserializeObject<IMItemList<IMWorkspace>>(json).Data;
                }
                return new List<IMWorkspace>();
            }
            catch (Exception ex)
            {
                if (ErrorHandler != null)
                {
                    bool handled = ErrorHandler(ex);
                    if (handled)
                    {
                        return null;
                    }
                }

                throw new IMException("Exception in: Workspaces, unable to read user workspaces", ex, IMException.GENERAL_ERROR);
            }
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
            StringBuilder uri = new StringBuilder(query);

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

            if (total)
            {
                uri.Append("&total=true");
            }

            return uri.ToString();
        }

        public string ConstructSearchTerm(string query, List<string> terms)
        {
            StringBuilder uri = new StringBuilder(query);

            bool firstTerm = true;
            foreach (string term in terms)
            {
                if (firstTerm)
                {
                    firstTerm = false;
                    uri.Append("?");
                }
                else
                {
                    uri.Append("&");
                }

                uri.Append(term);
            }

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

                WebResponse resp = req.GetResponse();

                if (resp == null)
                {
                    return null;
                }

                StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

                string json = sr.ReadToEnd().Trim();

                return JsonConvert.DeserializeObject<IMItem<IMUser>>(json).Data;
            }
            catch (Exception ex)
            {
                if (ErrorHandler != null)
                {
                    bool handled = ErrorHandler(ex);
                    if (handled)
                    {
                        return null;
                    }
                }
                throw new IMException("Exception in: WhoAmI, unable to read user details", ex, IMException.BAD_USER_INFO);
            }
        }

        public string PerformGetCall(string uri)
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

                WebResponse resp = req.GetResponse();

                if (resp == null)
                {
                    return null;
                }

                StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

                return sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                if (ErrorHandler != null)
                {
                    bool handled = ErrorHandler(ex);
                    if (handled)
                    {
                        return null;
                    }
                }
                throw new IMException(string.Format("Exception in: MakeGetCall []", uri), ex, IMException.BAD_GET_CALL);
            }
        }

        public byte[] PerformDownloadCall(string uri)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(uri);

                req.PreAuthenticate = true;
                req.Headers.Add("Authorization", "Bearer " + IMToken);
                req.Accept = "application/json";
                req.Method = "GET";
                req.KeepAlive = true;

                WebResponse resp = req.GetResponse();
                if (resp == null)
                {
                    return null;
                }

                Stream respStream = resp.GetResponseStream();
                using (MemoryStream ms = new MemoryStream())
                {
                    respStream.CopyTo(ms);

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ErrorHandler != null)
                {
                    bool handled = ErrorHandler(ex);
                    if (handled)
                    {
                        return null;
                    }
                }
                throw new IMException(string.Format("Exception in: PerformDownloadCall []", uri), ex, IMException.BAD_DOWNLOAD_CALL);
            }
        }

        public string PerformVersionUpload(string uri, byte[] data, IMDocument doc)
        {
            string boundary = "-----------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(uri);

                req.PreAuthenticate = true;
                req.Headers.Add("Authorization", "Bearer " + IMToken);
                req.Accept = "application/json";
                req.Method = "POST";
                req.KeepAlive = true;
                req.ContentType = "multipart/form-data; boundary=" + boundary;

                // Get the request stream
                Stream rs = req.GetRequestStream();

                // Create a profile object
                IMProfile profile = new IMProfile();
                IMDocProfile docProfile = new IMDocProfile
                {
                    Author = doc.Author,
                    Clazz = doc.Clazz,
                    Extension = doc.Extension,
                    Type = doc.Type
                };
                profile.DocProfile = docProfile;

                string fileName = doc.Name + "." + doc.Extension;

                // Write the profile object
                rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                string postTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\nContent-Type: application/json\r\n\r\n{1}";
                string formData = string.Format(postTemplate, "profile", JsonConvert.SerializeObject(profile));
                byte[] formBytes = System.Text.Encoding.UTF8.GetBytes(formData);
                rs.Write(formBytes, 0, formBytes.Length);

                // Write the file object
                rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                string fileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                string fileData = string.Format(fileTemplate, "file", fileName, "application/octet-stream");
                byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(fileData);
                rs.Write(fileBytes, 0, fileData.Length);
                rs.Write(data, 0, data.Length);

                // Write the trailer
                byte[] trailerBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailerBytes, 0, trailerBytes.Length);
                rs.Close();

                WebResponse resp = req.GetResponse();
                if (resp == null)
                {
                    return null;
                }

                StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

                return sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                if (ErrorHandler != null)
                {
                    bool handled = ErrorHandler(ex);
                    if (handled)
                    {
                        return null;
                    }
                }
                throw new IMException(string.Format("Exception in: PerformUploadCall []", uri), ex, IMException.BAD_UPLOAD_CALL);
            }
        }

        public string PerformUploadCall(string uri, byte[] data, NameValueCollection parameters)
        {
            string boundary = "-----------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(uri);

                req.PreAuthenticate = true;
                req.Headers.Add("Authorization", "Bearer " + IMToken);
                req.Accept = "application/json";
                req.Method = "POST";
                req.KeepAlive = true;
                req.ContentType = "multipart/form-data; boundary=" + boundary;

                // Get the request stream
                Stream rs = req.GetRequestStream();

                // Write the paramter objects
                foreach (string key in parameters.Keys)
                {
                    // Reserved keys star with a "#"
                    if (!key.StartsWith("#"))
                    {
                        string parameterDetails = parameters[key];
                        string[] details = parameterDetails.Split('|');
                        if (details.Length == 3)
                        {
                            // We expect a something like:
                            // object|application/json|{"field": "value"}
                            // otherwise there is not much we can do
                            rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                            string postTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\nContent-Type: {1}\r\n\r\n{2}";
                            string formData = string.Format(postTemplate, details[0], details[1]);
                            byte[] formBytes = Encoding.UTF8.GetBytes(formData);
                            rs.Write(formBytes, 0, formBytes.Length);
                        }
                    }
                }

                // Write the file object
                string fileDetails = parameters["#file"];
                if (fileDetails != null)
                {
                    string[] details = fileDetails.Split('|');
                    if (details.Length == 3)
                    {
                        // We have all three pieced of inforamtion
                        rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                        string fileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                        string fileData = string.Format(fileTemplate, details[0], details[1], details[2]);
                        byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(fileData);
                        rs.Write(fileBytes, 0, fileData.Length);
                        rs.Write(data, 0, data.Length);
                    }
                    else if (details.Length == 2)
                    {
                        // Assume the content type is octet-stream
                        rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                        string fileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                        string fileData = string.Format(fileTemplate, details[0], details[1], "application/octet-stream");
                        byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(fileData);
                        rs.Write(fileBytes, 0, fileData.Length);
                        rs.Write(data, 0, data.Length);
                    }
                    else if (details.Length == 1)
                    {
                        // Assume the content type is octet-stream and the field is form field is named "file"
                        rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                        string fileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                        string fileData = string.Format(fileTemplate, "file", details[1], "application/octet-stream");
                        byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(fileData);
                        rs.Write(fileBytes, 0, fileData.Length);
                        rs.Write(data, 0, data.Length);
                    }
                }

                // Write the trailer
                byte[] trailerBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailerBytes, 0, trailerBytes.Length);
                rs.Close();

                WebResponse resp = req.GetResponse();
                if (resp == null)
                {
                    return null;
                }

                StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

                return sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                if (ErrorHandler != null)
                {
                    bool handled = ErrorHandler(ex);
                    if (handled)
                    {
                        return null;
                    }
                }
                throw new IMException(string.Format("Exception in: PerformUploadCall []", uri), ex, IMException.BAD_UPLOAD_CALL);
            }
        }
    }
}
