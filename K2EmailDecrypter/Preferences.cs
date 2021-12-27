using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2EmailDecrypter
{
    class Preferences
    {
        private static string RootKey = "Software";

        public string IMKey
        {
            get { return Utilities.ReadUserKey(RootKey, nameof(IMKey)); }
            set { Utilities.WriteUserKey(RootKey, nameof(IMKey), value.ToString()); }
        }

        public string CryptoProvider
        {
            get { return Utilities.ReadUserKey(RootKey, nameof(CryptoProvider)); }
            set { Utilities.WriteUserKey(RootKey, nameof(CryptoProvider), value.ToString()); }
        }

        public int Delay
        {
            get
            {
                Int32.TryParse(Utilities.ReadUserKey(RootKey, nameof(Delay)), out int val);
                return val;
            }
            set { Utilities.WriteUserKey(RootKey, nameof(Delay), value.ToString()); }
        }
    }
}
