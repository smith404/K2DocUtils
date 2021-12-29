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

        public string Database
        {
            get { return Utilities.ReadUserKey(RootKey, nameof(Database)); }
            set { Utilities.WriteUserKey(RootKey, nameof(Database), value.ToString()); }
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

        public string LastRunISO8601
        {
            get { return Utilities.ReadUserKey(RootKey, nameof(LastRunISO8601)); }
            set { Utilities.WriteUserKey(RootKey, nameof(LastRunISO8601), value.ToString()); }
        }

    }
}
