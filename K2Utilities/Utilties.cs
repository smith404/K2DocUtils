using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2Utilities
{
    public class Utilities
    {
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

        public string getNowISO8601(bool universalTime = true)
        {
            return getTimeISO8601(DateTime.Now, universalTime);
        }

        public string getTimeISO8601(DateTime theTime, bool universalTime = true)
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
            LookUp lup = new LookUp();

            lup.Add("application", Application);
            lup.Add("version", Version);

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

        public string ReadUserKey(string root, string key)
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
    }
}
