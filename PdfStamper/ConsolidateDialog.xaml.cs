using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Annotations;
using PdfStamper.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

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

                    cd.addDocument(ConsolidatedDocument.makeDocument(fileData, document.Title));
                }

                //Watermark wm = new Watermark("Watermark");

                PageStamp ps = new PageStamp(stampTxt.Text)
                {
                    FontSize = Convert.ToDouble(fontSizeTxt.Text),
                    FontBrush = XBrushes.Red
                };

                ps.constructFont();

                //RubberStamp rs = new RubberStamp(PdfRubberStampAnnotationIcon.Approved);

                var stamps = new List<OverlayElement>();

                //stamps.Add(rs);
                stamps.Add(ps);
                //stamps.Add(wm);

                byte[] consolidatedDocument = cd.consolidate(stamps);

                OutputDocument = ConsolidatedDocument.makeDocument(consolidatedDocument, "Consolidated Document");
                if (OutputDocument.PageCount > 0) DialogResult = true;

                return;
            }

            DialogResult = false;
        }
    }
}
