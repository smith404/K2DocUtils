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

        public static List<Key> GetSubkeysValue(string path, RegistryHive hive)
        {
            var result = new List<Key>();
            var hiveKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default);

            using (var key = hiveKey.OpenSubKey(path))
            {
                var subkeys = key.GetSubKeyNames();

                foreach (var subkey in subkeys)
                {
                    var values = GetKeyValue(hiveKey, subkey);
                    result.Add(values);
                }
            }

            return result;
        }

        public static Key GetKeyValue(RegistryKey hive, string keyName)
        {
            var result = new Key() { KeyName = keyName };

            using (var key = hive.OpenSubKey(keyName))
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

        public static void SetKeyValue(RegistryKey hive, string keyName, object value)
        {
            using (var key = hive.OpenSubKey(keyName))
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
