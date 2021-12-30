using System;
using K2Utilities;


namespace K2EmailDecrypter
{
    public class Preferences
    {
        private static readonly string RootKey = "Software";

        public string IMKey
        {
            get { return Utilities.Instance.ReadUserKey(RootKey, nameof(IMKey)); }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(IMKey), value.ToString()); }
        }

        public string Database
        {
            get { return Utilities.Instance.ReadUserKey(RootKey, nameof(Database)); }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(Database), value.ToString()); }
        }

        public string CryptoProvider
        {
            get { return Utilities.Instance.ReadUserKey(RootKey, nameof(CryptoProvider)); }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(CryptoProvider), value.ToString()); }
        }

        public int Delay
        {
            get
            {
                Int32.TryParse(Utilities.Instance.ReadUserKey(RootKey, nameof(Delay)), out int val);
                return val;
            }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(Delay), value.ToString()); }
        }

        public string LastRunISO8601
        {
            get { return Utilities.Instance.ReadUserKey(RootKey, nameof(LastRunISO8601)); }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(LastRunISO8601), value.ToString()); }
        }

    }
}
