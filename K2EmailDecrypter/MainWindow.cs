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

        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;
        private MenuItem exitMenuItem;
        private MenuItem propertiesMenuItem;

        private bool wasExitAction = false;

        public MainWindow()
        {
            Utilities.Application = appName;
            Utilities.Version = appVersion;

            log.Debug("Application started");

            log.Debug("Error Handler Assigned");
            K2IMInterface.IMSession.Instance.ErrorHandler = this.MyErrorCallback;

            InitializeComponent();

            this.components = new System.ComponentModel.Container();

            // Initialize context menu
            this.exitMenuItem = new MenuItem();
            this.exitMenuItem.Index = 0;
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new EventHandler(this.exitMenuItem_Click);

            // Initialize context menu
            this.propertiesMenuItem = new MenuItem();
            this.propertiesMenuItem.Index = 0;
            this.propertiesMenuItem.Text = "P&references";
            this.propertiesMenuItem.Click += new EventHandler(this.propertiesMenuItem_Click);

            this.contextMenu = new ContextMenu();
            this.contextMenu.MenuItems.AddRange(new MenuItem[] { this.propertiesMenuItem });
            this.contextMenu.MenuItems.AddRange(new MenuItem[] { this.exitMenuItem });


            // Set up how the form should be displayed.
            this.ClientSize = new System.Drawing.Size(800, 250);
            this.Text = appName;

            // Set up the notify icon
            this.notifyIcon = new NotifyIcon(this.components);
            notifyIcon.Icon = new Icon("resources/secure.ico");
            notifyIcon.ContextMenu = this.contextMenu;
            notifyIcon.Text = appName;
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += new EventHandler(this.notifyIcon_DoubleClick);

            // Set window properties
            this.Resize += new EventHandler(this.mainWindow_Resize);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.FormClosing += mainWindow_Close;

            keyTxt.Text = Utilities.ReadUserKey("IMToken");
        }

        private void mainWindow_Resize(object Sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
        }

        private void mainWindow_Close(object Sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !wasExitAction)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            else
            {
                notifyIcon.Icon = null;
                Utilities.WriteUserKey("IMToken", keyTxt.Text);
            }
        }

        private void notifyIcon_DoubleClick(object Sender, EventArgs e)
        {
            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
            {
                // Activate the form.
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.ShowInTaskbar = true;
            }
        }

        private void exitMenuItem_Click(object Sender, EventArgs e)
        {
            // Close the form, as the user has selected exit
            log.Debug("Application exiting");
            wasExitAction = true;
            this.Close();
        }

        private void propertiesMenuItem_Click(object Sender, EventArgs e)
        {
            // Close the form, as the user has selected exit
            PropertiesForm propsFrm = new PropertiesForm();

            propsFrm.Show();
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
