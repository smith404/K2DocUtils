using K2IManageObjects;
using K2Utilities;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace K2EmailDecrypter
{
    public partial class MainWindow : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string appName = "K2 Email Decrypter";
        public static string appVersion = "0.0.1";

        private readonly PropertiesForm properties;

        private readonly NotifyIcon AppNotifyIcon;
        private readonly ContextMenu AppContextMenu;
        private readonly MenuItem ExitMenuItem;
        private readonly MenuItem PropertiesMenuItem;

        private bool wasExitAction = false;

        private Socket httpServer = null;
        private int serverPort = 80;
        private Thread thread;

        private readonly Decrypter decrypter = new Decrypter();
        public Decrypter Decrypter
        {
            get { return decrypter; }
        }

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

            // Create the singleton properties form for the application
            properties = new PropertiesForm();

            // Disable and Enable Buttons
            StartBtn.Enabled = true;
            HaltBtn.Enabled = false;
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
                // Stop the server if it was running
                StopServer();

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
            if (properties.Visible)
            {
                properties.Focus();
            }
            else
            {
                properties.Show();
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            OutputTxt.Text = "";

            try
            {
                httpServer = new Socket(SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    serverPort = properties.Preferences.Port;

                    if (serverPort > 65535 || serverPort <= 0)
                    {
                        serverPort = 80;
                    }

                    thread = new Thread(new ThreadStart(this.ConnectionThreadMethod));
                    thread.Start();

                    // Disable and Enable Buttons
                    StartBtn.Enabled = false;
                    HaltBtn.Enabled = true;

                    OutputTxt.AppendText("Server Started");
                }
                catch (Exception ex)
                {
                    log.Warn(ex);
                    httpServer = null;
                    serverPort = 80;
                    OutputTxt.AppendText("Server Failed to Start on Specified Port \n" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                httpServer = null;
                OutputTxt.AppendText("Server Starting Failed \n" + ex.Message);
            }
        }

        private void ConnectionThreadMethod()
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, serverPort);
                httpServer.Bind(endpoint);
                httpServer.Listen(1);
                StartListeningForConnection();
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }
        }

        private void StartListeningForConnection()
        {
            while (true)
            {
                DateTime time = DateTime.Now;

                String data = "";
                byte[] bytes = new byte[2048];

                Socket client = httpServer.Accept(); // Blocking Statement

                // Wait for a connection
                while (true)
                {
                    int numBytes = client.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, numBytes);

                    if (data.IndexOf("\r\n") > -1)
                        break;
                }

                OutputTxt.Invoke((MethodInvoker)delegate {
                    // Runs inside the UI Thread
                    string stuff = "\r\n\r\n";
                    stuff += data;
                    stuff += "\r\n\r\n------ End of Request -------\r\n\r\n";
                    OutputTxt.AppendText(stuff);
                });

                client.SendTo(Decrypter.Decrypt(data), client.RemoteEndPoint);

                client.Close();
            }
        }

        private void HaltBtn_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void StopServer()
        {
            try
            {
                if (httpServer != null)
                {
                    // Close the Socket
                    httpServer.Close();
                    httpServer = null;

                    // Kill the Thread
                    thread.Abort();

                    // Disable and Enable Buttons
                    StartBtn.Enabled = true;
                    HaltBtn.Enabled = false;

                    OutputTxt.AppendText("Server Halted");
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                OutputTxt.AppendText("Server Halt Failed \n" + ex.Message);
            }
        }
    }
}
