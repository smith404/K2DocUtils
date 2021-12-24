using System.Windows.Forms;

namespace K2EmailDecrypter
{
    public partial class MainWindow : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow()
        {
            log.Debug("Application started");
            InitializeComponent();
        }
    }
}
