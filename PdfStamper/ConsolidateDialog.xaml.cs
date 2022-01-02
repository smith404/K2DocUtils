using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfStamper.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace PdfStamper
{
    /// <summary>
    /// Interaction logic for ConsolidateDialog.xaml
    /// </summary>
    public partial class ConsolidateDialog : Window
    {
        private readonly ObservableCollection<ExplorerItem> documents = null;

        public PdfDocument OutputDocument { get; set; }

        private ConsolidateDialog()
        {
            InitializeComponent();
            OutputDocument = null;
        }

        public ConsolidateDialog(ObservableCollection<ExplorerItem> documents) : this()
        {
            this.documents = documents;
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            ConsolidatedDocument cd = new ConsolidatedDocument();

            if (documents != null)
            {
                foreach (ExplorerItem document in documents)
                {
                    Console.WriteLine(document.Title);

                    byte[] fileData = File.ReadAllBytes(document.Id);

                    cd.AddDocument(ConsolidatedDocument.MakeDocument(fileData, document.Title));
                }

                //Watermark wm = new Watermark("Watermark");

                PageStamp ps = new PageStamp(stampTxt.Text)
                {
                    FontSize = Convert.ToDouble(fontSizeTxt.Text),
                    FontBrush = XBrushes.Red
                };

                ps.ConstructFont();

                //RubberStamp rs = new RubberStamp(PdfRubberStampAnnotationIcon.Approved);

                List<OverlayElement> stamps = new List<OverlayElement>();

                //stamps.Add(rs);
                stamps.Add(ps);
                //stamps.Add(wm);

                byte[] consolidatedDocument = cd.Consolidate(stamps);

                OutputDocument = ConsolidatedDocument.MakeDocument(consolidatedDocument, "Consolidated Document");
                if (OutputDocument.PageCount > 0)
                {
                    DialogResult = true;
                }

                return;
            }

            DialogResult = false;
        }
    }
}
