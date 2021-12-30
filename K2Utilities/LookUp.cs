using Microsoft.Win32;
using System.Collections.Generic;
using System.Text;

namespace K2Utilities
{
    public class LookUp
    {
        // Character to identify a variable place holder
        public char Identifier = '$';

        // Create a new dictionary of strings, with string keys for the lookup
        public readonly Dictionary<string, string> Entries = new Dictionary<string, string>();

        public int AddRegistryKeyValues(RegistryKey hive, string location)
        {
            Key key = Key.GetKeyValue(hive, location);

            if (key != null)
            {
                foreach (KeyValuePair<string, object> p in key.Values)
                {
                    Entries.Add(p.Key, p.Value.ToString());
                }

                return key.Values.Count;
            }

            return 0;
        }

        public void Add(string key, string value)
        {
            Entries.Add(key, value);
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
