/*
 * Copyright (c) 2021. K2-Software
 * All software, both binary and source published by K2-Software (hereafter, Software)
 * is copyrighted by the author (hereafter, K2-Software) and ownership of all right, 
 * title and interest in and to the Software remains with K2-Software. By using or 
 * copying the Software, User agrees to abide by the terms of this Agreement.
 */

using Newtonsoft.Json;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace K2Utilities
{
    public class K2Exception : Exception
    {
        public static int GENERAL_ERROR = 0;
        public static int BAD_USER_INFO = 1000;
        public static int BAD_GET_CALL = 1001;
        public static int BAD_DOWNLOAD_CALL = 1001;
        public static int BAD_UPLOAD_CALL = 1001;

        public int ErrorCode { get; set; }

        public K2Exception() : base() { }
        public K2Exception(string message) : base(message) { }
        public K2Exception(string message, Exception inner) : base(message, inner) { }
        public K2Exception(string message, Exception inner, int code) : base(message, inner) { ErrorCode = code; }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected K2Exception(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class Utilities
    {
        public delegate bool ErrorCallback(Exception ex);
        public static ErrorCallback ErrorHandler { get; set; }

        private static readonly object padlock = new object();
        private static Utilities instance = null;

        public string Application { get; set; }
        public string Version { get; set; }

        public static Utilities Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Utilities();
                        }
                    }
                }

                return instance;
            }
        }

        public string GetNowISO8601(bool universalTime = true)
        {
            return GetTimeISO8601(DateTime.Now, universalTime);
        }

        public string GetTimeISO8601(DateTime theTime, bool universalTime = true)
        {
            if (universalTime)
            {
                return theTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            else
            {
                return theTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            }

        }

        public LookUp MakeLookUp()
        {
            string location = "Software\\" + Application + "\\" + Version;
            LookUp lup = new LookUp();

            lup.Add("application", Application);
            lup.Add("version", Version);

            _ = lup.AddRegistryKeyValues(Registry.CurrentUser, location);

            return lup;
        }

        public void WriteUserKey(string root, string key, string val)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(root, true);

            if (regKey != null)
            {
                regKey.CreateSubKey(Application);
                regKey = regKey.OpenSubKey(Application, true);

                regKey.CreateSubKey(Version);
                regKey = regKey.OpenSubKey(Version, true);

                regKey.SetValue(key, val);
                regKey.Close();
            }
        }

        public string ReadUserKeyValue(string root, string key)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(root, true);

            if (regKey != null)
            {
                regKey.CreateSubKey(Application);
                regKey = regKey.OpenSubKey(Application, true);

                regKey.CreateSubKey(Version);
                regKey = regKey.OpenSubKey(Version, true);

                object val = regKey.GetValue(key);
                if (val != null)
                {
                    return val.ToString();
                }
            }

            return null;
        }

        public object SetObjectProperties(object that, Key key)
        {
            if (that != null && key != null)
            {
                Type objectType = that.GetType();

                foreach (KeyValuePair<string, object> pair in key.Values)
                {
                    PropertyInfo thatProperty = objectType.GetProperty(pair.Key);
                    if (thatProperty != null)
                    {
                        thatProperty.SetValue(that, pair.Value);
                    }
                }
            }
            return that;
        }

        public Key ReadUserKey(RegistryKey hikeKey, string subKey)
        {
            subKey = $"Software\\{Application}\\{Version}\\{subKey}";

            return Key.GetKeyValue(hikeKey, subKey);
        }

        public static string PerformGetCall(string url, string token = "")
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(url);

                if (token.Length > 0)
                {
                    req.PreAuthenticate = true;
                    req.Headers.Add("Authorization", "Bearer " + token);
                }
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
                throw new K2Exception(string.Format("Exception in: MakeGetCall []", url), ex, K2Exception.BAD_GET_CALL);
            }
        }

        public static string PerformPOSTCall(string url, object that, string token = "", bool isPut = false)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(url);

                if (token.Length > 0)
                {
                    req.PreAuthenticate = true;
                    req.Headers.Add("Authorization", "Bearer " + token);
                }
                req.Accept = "application/json";
                req.Method = (isPut) ? "PUT" : "POST";
                req.Timeout = 60000;

                // Turn our object to a json string as a byte array
                string json = JsonConvert.SerializeObject(that);
                byte[] data = Encoding.Default.GetBytes(json);

                req.ContentType = "application/json";
                req.ContentLength = data.Length;

                Stream reqStreeam = req.GetRequestStream();
                reqStreeam.Write(data, 0, data.Length);
                reqStreeam.Close();

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
                throw new K2Exception(string.Format("Exception in: MakeGetCall []", url), ex, K2Exception.BAD_GET_CALL);
            }
        }

        public byte[] PerformDownloadCall(string url, string token = "")
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(url);

                if (token.Length > 0)
                {
                    req.PreAuthenticate = true;
                    req.Headers.Add("Authorization", "Bearer " + token);
                }
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
                throw new K2Exception(string.Format("Exception in: PerformDownloadCall []", url), ex, K2Exception.BAD_DOWNLOAD_CALL);
            }
        }

        public string PerformUploadCall(string url, byte[] data, NameValueCollection parameters, string token = "")
        {
            string boundary = "-----------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(url);

                if (token.Length > 0)
                {
                    req.PreAuthenticate = true;
                    req.Headers.Add("Authorization", "Bearer " + token);
                }
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
                throw new K2Exception(string.Format("Exception in: PerformUploadCall []", url), ex, K2Exception.BAD_UPLOAD_CALL);
            }
        }
    }
}
