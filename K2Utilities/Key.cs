/*
 * Copyright (c) 2021. K2-Software
 * All software, both binary and source published by K2-Software (hereafter, Software)
 * is copyrighted by the author (hereafter, K2-Software) and ownership of all right, 
 * title and interest in and to the Software remains with K2-Software. By using or 
 * copying the Software, User agrees to abide by the terms of this Agreement.
 */

using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace K2Utilities
{
    public class Key
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
            List<Key> result = new List<Key>();

            using (RegistryKey key = hiveKey.OpenSubKey(path))
            {
                if (key != null)
                {
                    string[] subkeys = key.GetSubKeyNames();

                    foreach (string subkey in subkeys)
                    {
                        Key values = GetKeyValue(hiveKey, subkey);
                        result.Add(values);
                    }
                }
            }

            return result;
        }

        public static Key GetKeyValue(RegistryKey hiveKey, string keyName)
        {
            Key result = new Key(keyName);

            using (RegistryKey key = hiveKey.OpenSubKey(keyName))
            {
                if (key != null)
                {
                    foreach (string valueName in key.GetValueNames())
                    {
                        object val = key.GetValue(valueName);
                        KeyValuePair<string, object> pair = new KeyValuePair<string, object>(valueName, val);
                        result.Values.Add(pair);
                    }
                }
            }

            return result;
        }

        public static void SetKeyValue(RegistryKey hiveKey, string keyName, object value)
        {
            using (RegistryKey key = hiveKey.OpenSubKey(keyName))
            {
                foreach (string valueName in key.GetValueNames())
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
