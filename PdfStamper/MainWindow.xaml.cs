using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.Sequence;
using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
            pdfWebViewer.Navigate(new Uri("about:blank"));
        }

        public string Redacte(string source, List<ModelResult> results)
        {
            var sb = new StringBuilder();

            int idx = 0;
            results.ForEach(delegate (ModelResult result)
            {
                int abschnitt = result.Start - idx;
                sb.Append(source.Substring(idx, abschnitt));
                sb.Append("[REDACTED]");
                idx = result.End + 1;
            });
            sb.Append(source.Substring(idx));

            return sb.ToString();
        }

        private void executeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Create a new PDF document
            //PdfDocument document = new PdfDocument();

            //SecurityInfo si = new SecurityInfo(document.SecuritySettings);

            //si.ResetPasswords();

            //this.outputTxt.Text = "Admin PWD: " + si.AdminPassword;
            List<ExplorerItem> items = workspaceExplorer.FindSelected();
            ObservableCollection<SelectedDocument> tasks = SelectedDocument.CreateTasks(items);
            this.listView.ItemsSource = tasks;


            var culture = Culture.English;
            //var results = SequenceRecognizer.RecognizeIpAddress(inputTxt.Text, culture);
            //var results = SequenceRecognizer.RecognizePhoneNumber(inputTxt.Text, culture);
            var results = DateTimeRecognizer.RecognizeDateTime(inputTxt.Text, culture);

            var sb = new StringBuilder();
            results.ForEach(delegate (ModelResult result)
            {
                sb.Append(result.Text);
                sb.Append(":[");
                sb.Append(result.Start);
                sb.Append("..");
                sb.Append(result.End);
                sb.AppendLine("]");
            });

            sb.AppendLine(Redacte(inputTxt.Text, results));

            outputTxt.Text = sb.ToString();
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

                //Watermark wm = new Watermark("Bollocks");
                PageStamp ps = new PageStamp("Bates {DN} / {PN}");

                //wm.WatermarkDocument(inputDocument, outputDocument);
                ps.StampDocument(inputDocument, outputDocument);

                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                Console.WriteLine(fileName);
                outputDocument.Save(fileName);

                pdfWebViewer.Navigate(fileName);
            }
        }

        ListViewDragDropManager<SelectedDocument> dragMgr;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // This is all that you need to do, in order to use the ListViewDragManager.
            this.dragMgr = new ListViewDragDropManager<SelectedDocument>(this.listView);
        }

        // Performs custom drop logic for the top ListView.
        void dragMgr_ProcessDrop(object sender, ProcessDropEventArgs<SelectedDocument> e)
        {
            // This shows how to customize the behavior of a drop.
            // Here we perform a swap, instead of just moving the dropped item.

            int higherIdx = Math.Max(e.OldIndex, e.NewIndex);
            int lowerIdx = Math.Min(e.OldIndex, e.NewIndex);

            if (lowerIdx < 0)
            {
                // The item came from the lower ListView
                // so just insert it.
                e.ItemsSource.Insert(higherIdx, e.DataItem);
            }
            else
            {
                // null values will cause an error when calling Move.
                // It looks like a bug in ObservableCollection to me.
                if (e.ItemsSource[lowerIdx] == null ||
                    e.ItemsSource[higherIdx] == null)
                    return;

                // The item came from the ListView into which
                // it was dropped, so swap it with the item
                // at the target index.
                e.ItemsSource.Move(lowerIdx, higherIdx);
                e.ItemsSource.Move(higherIdx - 1, lowerIdx);
            }

            // Set this to 'Move' so that the OnListViewDrop knows to 
            // remove the item from the other ListView.
            e.Effects = DragDropEffects.Move;
        }

        // Handles the DragEnter event for both ListViews.
        void OnListViewDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

    }
}
