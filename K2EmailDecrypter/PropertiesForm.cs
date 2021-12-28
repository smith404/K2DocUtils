using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K2EmailDecrypter
{
    public partial class PropertiesForm : Form
    {
        private static readonly string ProviderRoot = "SOFTWARE\\Microsoft\\Cryptography\\Defaults\\Provider";
        private readonly Preferences preferences;

        public PropertiesForm()
        {
            // Set up window with no task bar presence
            InitializeComponent();
            ShowInTaskbar = false;

            // Create application preferences instance
            preferences = new Preferences();

            versionNumberLbl.Text = MainWindow.appVersion;

            // Read the delay preference and make sure it's valid
            int delay = preferences.Delay;
            if (delay < refreshTrk.Minimum || delay > refreshTrk.Maximum)
            {
                delay = refreshTrk.Minimum;
            }
            refreshTrk.Value = delay;
            valLbl.Text = delay.ToString();

            List<Key> keys = Key.GetSubkeysValue(ProviderRoot, Microsoft.Win32.RegistryHive.LocalMachine);
            foreach(Key key in keys)
            {
                cryptoProviderCbb.Items.Add(key);
            }

            Key provider = new Key
            {
                KeyName = preferences.CryptoProvider
            };

            cryptoProviderCbb.SelectedItem = provider;
        }

        private void PropertiesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                preferences.Delay = refreshTrk.Value;
                if (cryptoProviderCbb.SelectedItem != null)
                {
                    preferences.CryptoProvider = cryptoProviderCbb.SelectedItem.ToString();
                }

                e.Cancel = true;
                Hide();
            }

        }

        private void refeshTrk_ValueChanged(object sender, EventArgs e)
        {
            valLbl.Text = refreshTrk.Value.ToString();
        }

        private void PropertiesForm_Load(object sender, EventArgs e)
        {

        }
    }
}
