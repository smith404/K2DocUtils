using System;

namespace K2EmailDecrypter
{
    public class Preferences
    {
        private static readonly string RootKey = "Software";

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
