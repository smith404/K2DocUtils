using Microsoft.Recognizers.Text;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Drawing.BarCodes;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace PdfStamper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Explorer workspaceExplorer = null;

        public MainWindow()
        {
            InitializeComponent();
            workspaceExplorer = new Explorer(TreeView);
            PdfWebViewer.Navigate(new Uri("about:blank"));
        }

        public string Redacte(string source, List<ModelResult> results)
        {
            StringBuilder sb = new StringBuilder();

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

        private readonly ObservableCollection<ExplorerItem> documents = null;
        private void ExecuteBtn_Click(object sender, RoutedEventArgs e)
        {
            // Create a new PDF document
            PdfDocument s_document = new PdfDocument();
            s_document.Info.Title = "PDFsharp XGraphic Sample";
            s_document.Info.Author = "Stefan Lange";
            s_document.Info.Subject = "Created with code snippets that show the use of graphical functions";
            s_document.Info.Keywords = "PDFsharp, XGraphics";

            RenderPage(s_document.AddPage());
            s_document.Save("test.pdf");



            //SecurityInfo si = new SecurityInfo(document.SecuritySettings);

            //si.ResetPasswords();

            //this.outputTxt.Text = "Admin PWD: " + si.AdminPassword;
            //List<ExplorerItem> items = workspaceExplorer.FindSelected();
            //documents = ExplorerItem.CreateItems(items);
            //this.listView.ItemsSource = documents;


            //var culture = Culture.English;
            //var results = SequenceRecognizer.RecognizeIpAddress(inputTxt.Text, culture);
            //var results = SequenceRecognizer.RecognizePhoneNumber(inputTxt.Text, culture);
            //var results = DateTimeRecognizer.RecognizeDateTime(inputTxt.Text, culture);
            /*
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
            */
        }

        public void RenderPage(PdfPage page)
        {
            XGraphics gfx = XGraphics.FromPdfPage(page);

            Code2of5Interleaved bc25 = new Code2of5Interleaved
            {
                Text = "2501068039721",
                Size = new XSize(90, 30),
                Direction = CodeDirection.LeftToRight,
                TextLocation = TextLocation.Above
            };

            gfx.DrawBarCode(bc25, XBrushes.Black, new XPoint(10, 10));

            /*
                        Code3of9Standard bc39 = new Code3of9Standard("ISABEL123", new XSize(90, 40));
                        bc39.TextLocation = TextLocation.AboveEmbedded;
                        gfx.DrawBarCode(bc39, XBrushes.DarkBlue, new XPoint(100, 500));

                        bc39.Direction = CodeDirection.RightToLeft;
                        gfx.DrawBarCode(bc39, XBrushes.DarkBlue, new XPoint(300, 500));

                        bc39.Text = "TITUS";
                        bc39.Direction = CodeDirection.TopToBottom;
                        gfx.DrawBarCode(bc39, XBrushes.DarkBlue, new XPoint(100, 700));

                        bc39.Direction = CodeDirection.BottomToTop;
                        gfx.DrawBarCode(bc39, XBrushes.Red, new XPoint(300, 700));
                        */
        }

        private void OpenFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pdf Files (*.pdf)|*.pdf"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Open the output document
                PdfDocument outputDocument = new PdfDocument();

                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(openFileDialog.FileName, PdfDocumentOpenMode.Import);

                //Watermark wm = new Watermark("Bollocks");
                PageStamp ps = new PageStamp("Bates {DN} / {PN}");

                //wm.WatermarkDocument(inputDocument, outputDocument);
                ps.OverlayDocuemnt(inputDocument, outputDocument);

                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                Console.WriteLine(fileName);
                outputDocument.Save(fileName);

                PdfWebViewer.Navigate(fileName);
            }
        }

        ListViewDragDropManager<ExplorerItem> dragMgr;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // This is all that you need to do, in order to use the ListViewDragManager.
            dragMgr = new ListViewDragDropManager<ExplorerItem>(ListView);
        }

        // Performs custom drop logic for the top ListView.
        void DragMgr_ProcessDrop(object sender, ProcessDropEventArgs<ExplorerItem> e)
        {
            // Perform a swap, instead of just moving the dropped item.
            int higherIdx = Math.Max(e.OldIndex, e.NewIndex);
            int lowerIdx = Math.Min(e.OldIndex, e.NewIndex);

            if (lowerIdx < 0)
            {
                // The item came from the lower ListView, so just insert it.
                e.ItemsSource.Insert(higherIdx, e.DataItem);
            }
            else
            {
                // Null values will cause an error when calling Move.
                if (e.ItemsSource[lowerIdx] == null ||
                    e.ItemsSource[higherIdx] == null)
                {
                    return;
                }

                // The item came from the ListView into which it was dropped, so swap it with the item at the target index.
                e.ItemsSource.Move(lowerIdx, higherIdx);
                e.ItemsSource.Move(higherIdx - 1, lowerIdx);
            }

            // Set this to 'Move' so that the OnListViewDrop knows to remove the item from the other ListView.
            e.Effects = DragDropEffects.Move;
        }

        // Handles the DragEnter event for both ListViews.
        void OnListViewDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        private void MakeBtn_Click(object sender, RoutedEventArgs e)
        {
            ConsolidateDialog dialog = new ConsolidateDialog(documents);

            // Display the dialog box and read the response
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                // User accepted the dialog box
                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";

                if (dialog.passwordCbb.IsChecked ?? true)
                {
                    SecurityInfo si = new SecurityInfo(dialog.OutputDocument.SecuritySettings);

                    si.ResetPasswords();
                    AdminPasswordTxt.Text = si.AdminPassword;
                    UserPassowrdTxt.Text = si.UserPassword;

                }

                Console.WriteLine(fileName);

                dialog.OutputDocument.Save(fileName);

                PdfWebViewer.Navigate(fileName);
            }
            else
            {
                // User cancelled the dialog box
                MessageBox.Show("Sorry it didn't work out, we'll try again later.");
            }
            /*
                        ConsolidatedDocument cd = new ConsolidatedDocument();

                        if (documents != null)
                        {
                            foreach (ExplorerItem document in documents)
                            {
                                Console.WriteLine(document.Title);

                                byte[] fileData = File.ReadAllBytes(document.Id);

                                cd.addDocument(ConsolidatedDocument.makeDocument(fileData, document.Title));
                            }

                            Watermark wm = new Watermark("Bollocks");
                            PageStamp ps = new PageStamp("Bates {DN} / {PN}");
                            var stamps = new List<OverlayElement>();

                            stamps.Add(ps);
                            stamps.Add(wm);

                            byte[] consolidatedDocument = cd.consolidate(stamps);

                            PdfDocument outputDocument = ConsolidatedDocument.makeDocument(consolidatedDocument, "Consolidated Document");
                            Console.WriteLine("Pages: " + outputDocument.PageCount);

                            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                            Console.WriteLine(fileName);
                            outputDocument.Save(fileName);

                            pdfWebViewer.Navigate(fileName);
                        }
            */
        }
    }
}
