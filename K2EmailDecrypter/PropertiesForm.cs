using K2Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace K2EmailDecrypter
{
    public partial class PropertiesForm : Form
    {
        private const string ProviderRoot = "SOFTWARE\\Microsoft\\Cryptography\\Defaults\\Provider";

        private readonly Preferences preferences;
        public Preferences Preferences
        {
            get { return preferences; }
        }

        public PropertiesForm()
        {
            // Set up window with no task bar presence
            InitializeComponent();

            RefreshTrk.Minimum = Preferences.MinDelay;
            RefreshTrk.Maximum = Preferences.MaxDelay;

            ShowInTaskbar = false;
            TokenTxt.UseSystemPasswordChar = true;

            // Create application preferences instance
            preferences = new Preferences();

            VersionNumberLbl.Text = MainWindow.appVersion;

            // Read the delay preference and make sure it's valid
            int delay = preferences.Delay;
            if (delay < RefreshTrk.Minimum || delay > RefreshTrk.Maximum)
            {
                delay = RefreshTrk.Minimum;
            }
            RefreshTrk.Value = delay;
            ValLbl.Text = delay.ToString();

            List<Key> keys = Key.GetSubkeysList(Registry.LocalMachine, ProviderRoot);
            foreach (Key key in keys)
            {
                CryptoProviderCbb.Items.Add(key);
            }

            Key provider = new Key(preferences.CryptoProvider);

            CryptoProviderCbb.SelectedItem = provider;

            TokenTxt.Text = preferences.IMKey;

            bool.TryParse(preferences.Notifications, out bool notify);
            NotificationsCbx.Checked = notify;
        }

        private void PropertiesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                preferences.IMKey = TokenTxt.Text;
                preferences.Delay = RefreshTrk.Value;
                if (CryptoProviderCbb.SelectedItem != null)
                {
                    preferences.CryptoProvider = CryptoProviderCbb.SelectedItem.ToString();
                }
                preferences.Notifications = NotificationsCbx.Checked.ToString();

                e.Cancel = true;
                Hide();
            }

        }

        private void RefeshTrk_ValueChanged(object sender, EventArgs e)
        {
            ValLbl.Text = RefreshTrk.Value.ToString();
        }

        private void ViewBtn_Click(object sender, EventArgs e)
        {
            TokenTxt.UseSystemPasswordChar = !TokenTxt.UseSystemPasswordChar;
        }
    }
}
