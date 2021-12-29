using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2EmailDecrypter
{
    class Utilities
    {
        public static string Application { get; set; }
        public static string Version { get; set; }

        public static string getNowISO8601(bool universalTime = true)
        {
            return getTimeISO8601(DateTime.Now, universalTime);
        }

        public static string getTimeISO8601(DateTime theTime, bool universalTime = true)
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

        public static void WriteUserKey(string root, string key, string val)
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

        public static string ReadUserKey(string root, string key)
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

    class Key
    {
        public string KeyName { get; set; }

        public List<KeyValuePair<string, object>> Values { get; set; }

        public Key(string keyName)
        {
            KeyName = keyName;
            Values = new List<KeyValuePair<string, object>>();
        }

        public static List<Key> GetSubkeysList(RegistryKey hiveKey, string path)
        {
            var result = new List<Key>();

            using (var key = hiveKey.OpenSubKey(path))
            {
                if (key != null)
                {
                    var subkeys = key.GetSubKeyNames();

                    foreach (var subkey in subkeys)
                    {
                        var values = GetKeyValue(hiveKey, subkey);
                        result.Add(values);
                    }
                }
            }

            return result;
        }

        public static Key GetKeyValue(RegistryKey hiveKey, string keyName)
        {
            var result = new Key(keyName);

            using (var key = hiveKey.OpenSubKey(keyName))
            {
                if (key != null)
                {
                    foreach (var valueName in key.GetValueNames())
                    {
                        var val = key.GetValue(valueName);
                        var pair = new KeyValuePair<string, object>(valueName, val);
                        result.Values.Add(pair);
                    }
                }
                else
                {
                    result.Values = null;
                }
            }

            return result;
        }

        public static void SetKeyValue(RegistryKey hiveKey, string keyName, object value)
        {
            using (var key = hiveKey.OpenSubKey(keyName))
            {
                foreach (var valueName in key.GetValueNames())
                {
                    key.SetValue(valueName, value);
                }
            }
        }

        public override string ToString()
        {
            return KeyName;
        }

        public override bool Equals(Object that)
        {
            if ((that == null))
            {
                return false;
            }
            else
            {
                return KeyName.Equals(that.ToString());
            }
        }

        public override int GetHashCode()
        {
            return KeyName.GetHashCode();
        }

    }
}
