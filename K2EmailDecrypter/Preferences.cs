using K2Utilities;
using System;


namespace K2EmailDecrypter
{
    public class Preferences
    {
        public const int MinDelay = 60;
        public const int DefaultDelay = 180;
        public const int MaxDelay = 1800;
        private const string RootKey = "Software";

        public string IMKey
        {
            get { return Utilities.Instance.ReadUserKeyValue(RootKey, nameof(IMKey)); }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(IMKey), value.ToString()); }
        }

        public string Database
        {
            get { return Utilities.Instance.ReadUserKeyValue(RootKey, nameof(Database)); }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(Database), value); }
        }

        public string CryptoProvider
        {
            get { return Utilities.Instance.ReadUserKeyValue(RootKey, nameof(CryptoProvider)); }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(CryptoProvider), value); }
        }

        public int Delay
        {
            get
            {
                Int32.TryParse(Utilities.Instance.ReadUserKeyValue(RootKey, nameof(Delay)), out int val);
                if (val < MinDelay || val > MaxDelay)
                {
                    val = DefaultDelay;
                }
                return val;
            }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(Delay), value.ToString()); }
        }

        public string LastRunISO8601
        {
            get { return Utilities.Instance.ReadUserKeyValue(RootKey, nameof(LastRunISO8601)); }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(LastRunISO8601), value); }
        }

        public string Notifications
        {
            get { return Utilities.Instance.ReadUserKeyValue(RootKey, nameof(Notifications)); }
            set { Utilities.Instance.WriteUserKey(RootKey, nameof(Notifications), value); }
        }
    }
}
