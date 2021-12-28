﻿using System;
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
        public Preferences Preferences
        {
            get { return preferences; }
        }

        public PropertiesForm()
        {
            // Set up window with no task bar presence
            InitializeComponent();
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

            List<Key> keys = Key.GetSubkeysValue(ProviderRoot, Microsoft.Win32.RegistryHive.LocalMachine);
            foreach(Key key in keys)
            {
                CryptoProviderCbb.Items.Add(key);
            }

            Key provider = new Key
            {
                KeyName = preferences.CryptoProvider
            };

            CryptoProviderCbb.SelectedItem = provider;

            TokenTxt.Text = preferences.IMKey;

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