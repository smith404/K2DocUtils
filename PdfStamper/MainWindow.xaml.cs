using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PdfStamper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Explorer workspaceExplorer = null;

        public MainWindow()
        {
            InitializeComponent();
            workspaceExplorer = new Explorer(treeView);
        }


        private void executeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Create a new PDF document
            //PdfDocument document = new PdfDocument();

            //SecurityInfo si = new SecurityInfo(document.SecuritySettings);

            //si.ResetPasswords();

            //this.outputTxt.Text = "Admin PWD: " + si.AdminPassword;
            this.outputTxt.Text = "Admin PWD: " + workspaceExplorer.FindSelected();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pdf Files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == true)
            {
                // Open the output document
                PdfDocument outputDocument = new PdfDocument();

                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(openFileDialog.FileName, PdfDocumentOpenMode.Import);

                Watermark wm = new Watermark("Bollocks");

                wm.WatermarkDocument(inputDocument, outputDocument);

                string filename = "ConcatenatedDocument1.pdf";
                outputDocument.Save(filename);
            }
        }

    }
}
