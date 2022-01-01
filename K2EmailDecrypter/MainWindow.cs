using K2IManageObjects;
using K2IMInterface;
using K2Utilities;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace K2EmailDecrypter
{
    public partial class MainWindow : Form
    {
        public static string appName = "K2 Email Decrypter";
        public static string appVersion = "0.0.1";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("MainWindow");

        private readonly Timer appTimer;
        public Timer AppTimer
        {
            get { return appTimer; }
        }

        private readonly Decrypter decrypter = new Decrypter();
        public Decrypter Decrypter
        {
            get { return decrypter; }
        }

        private readonly NotifyIcon AppNotifyIcon;
        private readonly ContextMenu AppContextMenu;
        private readonly MenuItem ExitMenuItem;
        private readonly MenuItem PropertiesMenuItem;

        private bool wasExitAction = false;

        private PropertiesForm properties;

        public MainWindow()
        {
            // Set some global values
            Utilities.Instance.Application = appName;
            Utilities.Instance.Version = appVersion;

            log.Debug("Application started");

            log.Debug("Error Handler Assigned");
            K2IMInterface.IMConnection.Instance.ErrorHandler = MyErrorCallback;

            InitializeComponent();

            components = new System.ComponentModel.Container();

            // Initialize context menu
            ExitMenuItem = new MenuItem
            {
                Index = 0,
                Text = "E&xit"
            };
            ExitMenuItem.Click += new EventHandler(ExitMenuItem_Click);

            // Initialize context menu
            PropertiesMenuItem = new MenuItem
            {
                Index = 0,
                Text = "P&references"
            };
            PropertiesMenuItem.Click += new EventHandler(PropertiesMenuItem_Click);

            AppContextMenu = new ContextMenu();
            AppContextMenu.MenuItems.AddRange(new MenuItem[] { PropertiesMenuItem });
            AppContextMenu.MenuItems.AddRange(new MenuItem[] { ExitMenuItem });

            // Set up how the form should be displayed.
            ClientSize = new System.Drawing.Size(800, 250);
            Text = appName;

            // Set up the notify icon
            AppNotifyIcon = new NotifyIcon(components)
            {
                Icon = new Icon("resources/secure.ico"),
                ContextMenu = AppContextMenu,
                Text = appName,
                Visible = true
            };
            AppNotifyIcon.DoubleClick += new EventHandler(AppNotifyIcon_DoubleClick);

            // Set window properties
            Resize += new EventHandler(MainWindow_Resize);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            FormClosing += MainWindow_Close;

            // Set up the timer object
            appTimer = new Timer();
            appTimer.Tick += new EventHandler(TimerEventProcessor);

            // Delay is stored in seconds so multiply by 1000 for milliseconds
            properties = new PropertiesForm();
            appTimer.Interval = properties.Preferences.Delay * 1000;

            appTimer.Start();

            LookUp lup = Utilities.Instance.MakeLookUp();

            string location = "Software\\" + appName + "\\" + appVersion + "\\Precondition";
            lup.AddRegistryKeyValues(Registry.CurrentUser, location);

            location = "Software\\" + appName + "\\" + appVersion + "\\Postcondition";
            Key key = Key.GetKeyValue(Registry.CurrentUser, location);

            IMInstance inst = new IMInstance
            {
                Name = "WooHoo"
            };

            Utilities.Instance.SetObjectProperties(inst, key);

            //Console.WriteLine(IMConnection.Instance.PerformPOSTCall("", inst));
        }

        public bool MyErrorCallback(Exception ex)
        {
            // Just ignore all the errors
            return true;
        }

        private void MainWindow_Resize(object Sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
            }
        }

        private void MainWindow_Close(object Sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !wasExitAction)
            {
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            else
            {
                // Stop the processing thread
                Decrypter.CancelReceivingThread();

                // Remove the icon to avoid ghosting in the notification area
                AppNotifyIcon.Icon = null;
            }
        }

        private void AppNotifyIcon_DoubleClick(object Sender, EventArgs e)
        {
            // Set the WindowState to normal if the form is minimized.
            if (WindowState == FormWindowState.Minimized)
            {
                // Activate the form.
                WindowState = FormWindowState.Normal;
                Activate();
                ShowInTaskbar = true;
            }
        }

        private void ExitMenuItem_Click(object Sender, EventArgs e)
        {
            // Close the form, as the user has selected exit
            log.Debug("Application exiting");
            wasExitAction = true;
            Close();
        }

        private void PropertiesMenuItem_Click(object Sender, EventArgs e)
        {
            // Close the form, as the user has selected exit
            if (properties == null)
            {
                properties = new PropertiesForm();
            }

            if (properties.Visible)
            {
                properties.Focus();
            }
            else
            {
                properties.Show();
            }
        }

        private void ExecuteBtn_Click(object sender, EventArgs e)
        {
            appTimer.Stop();
            OutputTxt.Text += "Event triggered: " + DateTime.Now + "\r\n";
            Decrypter.Decrypt(new IMDocument() { Id = Utilities.Instance.getNowISO8601() });
            appTimer.Start();

            properties.Preferences.LastRunISO8601 = Utilities.Instance.getNowISO8601();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            appTimer.Stop();
            OutputTxt.Text += "Event triggered: " + DateTime.Now + "\r\n";
            appTimer.Start();

            properties.Preferences.LastRunISO8601 = Utilities.Instance.getNowISO8601();
        }
    }
}
