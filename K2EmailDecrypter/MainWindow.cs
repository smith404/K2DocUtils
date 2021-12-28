using System;
using System.Drawing;
using System.Windows.Forms;

namespace K2EmailDecrypter
{
    public partial class MainWindow : Form
    {
        public static string appName = "K2 Email Decrypter";
        public static string appVersion = "0.0.1";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly NotifyIcon notifyIcon;
        private readonly ContextMenu contextMenu;
        private readonly MenuItem exitMenuItem;
        private readonly MenuItem propertiesMenuItem;

        private bool wasExitAction = false;

        private PropertiesForm properties;

        public MainWindow()
        {
            // Set some global values
            Utilities.Application = appName;
            Utilities.Version = appVersion;

            log.Debug("Application started");

            log.Debug("Error Handler Assigned");
            K2IMInterface.IMSession.Instance.ErrorHandler = MyErrorCallback;

            InitializeComponent();

            components = new System.ComponentModel.Container();

            // Initialize context menu
            exitMenuItem = new MenuItem
            {
                Index = 0,
                Text = "E&xit"
            };
            exitMenuItem.Click += new EventHandler(exitMenuItem_Click);

            // Initialize context menu
            propertiesMenuItem = new MenuItem
            {
                Index = 0,
                Text = "P&references"
            };
            propertiesMenuItem.Click += new EventHandler(propertiesMenuItem_Click);

            contextMenu = new ContextMenu();
            contextMenu.MenuItems.AddRange(new MenuItem[] { propertiesMenuItem });
            contextMenu.MenuItems.AddRange(new MenuItem[] { exitMenuItem });


            // Set up how the form should be displayed.
            ClientSize = new System.Drawing.Size(800, 250);
            Text = appName;

            // Set up the notify icon
            notifyIcon = new NotifyIcon(components)
            {
                Icon = new Icon("resources/secure.ico"),
                ContextMenu = contextMenu,
                Text = appName,
                Visible = true
            };
            notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);

            // Set window properties
            Resize += new EventHandler(mainWindow_Resize);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            FormClosing += mainWindow_Close;
        }

        private void mainWindow_Resize(object Sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
            }
        }

        private void mainWindow_Close(object Sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !wasExitAction)
            {
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            else
            {
                notifyIcon.Icon = null;
            }
        }

        private void notifyIcon_DoubleClick(object Sender, EventArgs e)
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

        private void exitMenuItem_Click(object Sender, EventArgs e)
        {
            // Close the form, as the user has selected exit
            log.Debug("Application exiting");
            wasExitAction = true;
            Close();
        }

        private void propertiesMenuItem_Click(object Sender, EventArgs e)
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

        private void executeBtn_Click(object sender, EventArgs e)
        {

        }

        public bool MyErrorCallback(Exception ex)
        {
            // Just ignore all the errors
            return true;
        }
    }
}
