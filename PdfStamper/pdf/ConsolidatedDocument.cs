using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfStamper.pdf
{
    class ConsolidatedDocument
    {
        public static PdfDocument makeDocument(byte[] data, string tagInfo)
        {
            MemoryStream stream = new MemoryStream(data);

            PdfDocument document = PdfReader.Open(stream, PdfDocumentOpenMode.Import);
            document.Tag = tagInfo;

            return document;
        }

        public bool WithSpacerPage { get; set; }

        public bool WithBookmarks { get; set; }

        // Stored list of documents to be added
        private readonly List<PdfDocument> inputDocuments = new List<PdfDocument>();

        public int AddedDocuments
        {
            get { return inputDocuments.Count; } 
        }

        public ConsolidatedDocument()
        {
            WithSpacerPage = false;
            WithBookmarks = true;
        }

        public int addDocument(PdfDocument inputDocument)
        {
            inputDocuments.Add(inputDocument);

            return inputDocuments.Count;
        }

        public byte[] consolidate(List<OverlayElement> stamps)
        {
            // Create the output document
            PdfDocument outputDocument = new PdfDocument();

            // Iterate files
            foreach (PdfDocument inputDocument in inputDocuments)
            {
                // Create a page outline
                PdfOutline outline = null;

                // Iterate pages
                for (int idx = 0; idx < inputDocument.PageCount; idx++)
                {
                    // Load source page document
                    PdfPage page = inputDocument.Pages[idx];

                    // Add to target document
                    PdfPage newPage = outputDocument.AddPage(page);

                    // Create the root bookmark. You can set the style and the color.
                    if (WithBookmarks && idx == 0) outline = outputDocument.Outlines.Add(inputDocument.Tag.ToString(), newPage, true, PdfOutlineStyle.Bold, XColors.Black);
                }
            }

            MemoryStream memStream = new MemoryStream();
            outputDocument.Save(memStream);
            return memStream.GetBuffer();
        }
    }
}
