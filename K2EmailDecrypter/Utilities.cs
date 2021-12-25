using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2EmailDecrypter
{
    class Utilities
    {
        public static string Application { get; set; }
        public static string Version { get; set; }

        public static void WriteUserKey(string key, string val)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software", true);

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

        public static string ReadUserKey(string key)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software", true);

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
