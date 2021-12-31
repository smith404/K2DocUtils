/*
 * Copyright (c) 2021. K2-Software
 * All software, both binary and source published by K2-Software (hereafter, Software)
 * is copyrighted by the author (hereafter, K2-Software) and ownership of all right, 
 * title and interest in and to the Software remains with K2-Software. By using or 
 * copying the Software, User agrees to abide by the terms of this Agreement.
 */

using Microsoft.Win32;
using System.Collections.Generic;
using System.Text;

namespace K2Utilities
{
    public class LookUp
    {
        // Characters to identify a variable place holder
        public string Prepend = "${";
        public string Append = "}";

        // Create a new dictionary of strings, with string keys for the lookup
        public readonly Dictionary<string, string> Entries = new Dictionary<string, string>();

        public int AddRegistryKeyValues(RegistryKey hive, string location)
        {
            Key key = Key.GetKeyValue(hive, location);

            if (key != null)
            {
                foreach (KeyValuePair<string, object> p in key.Values)
                {
                    Entries[p.Key.ToLower()] = p.Value.ToString();
                }

                return key.Values.Count;
            }
            return 0;
        }

        public int AddObject(object that)
        {
            if (that != null)
            {
                int c = 0;

                foreach (var prop in that.GetType().GetProperties())
                {
                    object value = prop.GetValue(that, null);
                    if (value != null)
                    {
                        Entries[prop.Name.ToLower()] = value.ToString();
                        ++c;
                    }
                }

                return c;
            }
            return 0;
        }

        public void Add(string key, string value)
        {
            Entries[key.ToLower()] = value;
        }

        public string SearchAndReplace(string inStr)
        {
            string outStr = inStr;

            foreach (KeyValuePair<string, string> kvp in Entries)
            {
                outStr = outStr.Replace(Prepend + kvp.Key + Append, kvp.Value);
            }

            return outStr;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> kvp in Entries)
            {
                sb.AppendLine(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
            }

            return sb.ToString();
        }
    }
}
