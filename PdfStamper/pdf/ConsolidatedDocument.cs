using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
        // Stored list of documents to be added
        private readonly List<PdfDocument> inputDocuments = new List<PdfDocument>();

        public int AddedDocuments
        {
            get { return inputDocuments.Count; } 
        }

        public ConsolidatedDocument()
        {
        }

        public int addDocument(PdfDocument inputDocument)
        {
            inputDocuments.Add(inputDocument);

            return inputDocuments.Count;
        }

        public byte[] consolidate()
        {
            MemoryStream memStream = new MemoryStream();

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

                    // Create the root bookmark. You can set the style and the color.
                    if (idx==0) outline = outputDocument.Outlines.Add(inputDocument.FullPath, page, true, PdfOutlineStyle.Bold, XColors.Black);

                    // Add to target document
                    outputDocument.AddPage(page);
                }
            }

            outputDocument.Save(memStream);

            return memStream.GetBuffer();
        }

        public void consolidate(string fileName)
        {
            File.WriteAllBytes(fileName, consolidate());
        }
    }
}
