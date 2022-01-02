using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace PdfStamper.pdf
{
    class ConsolidatedDocument
    {
        static void PasswordProvider(PdfPasswordProviderArgs args)
        {
            // Show a dialog here in a real application
            args.Password = "owner";
        }

        public static PdfDocument OpenDocument(string path)
        {
            PdfDocument document = null;
            try
            {
                // Try and open the document
                document = PdfReader.Open(path);
            }
            catch (Exception ex)
            {
                // Try to open with a password
                document = PdfReader.Open(path, PdfDocumentOpenMode.Modify, PasswordProvider);

                Console.WriteLine(ex.Message);
            }

            return document;
        }

        public static PdfDocument MakeDocument(byte[] data, string tagInfo)
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

        public int AddDocument(PdfDocument inputDocument)
        {
            inputDocuments.Add(inputDocument);

            return inputDocuments.Count;
        }

        public byte[] Consolidate(List<OverlayElement> stamps)
        {
            // Create the output document
            PdfDocument outputDocument = new PdfDocument();

            // Iterate files
            int docIdx = 0;
            foreach (PdfDocument inputDocument in inputDocuments)
            {
                // Create a page outline
                PdfOutline outline = null;

                // Iterate pages
                for (int pageIdx = 0; pageIdx < inputDocument.PageCount; pageIdx++)
                {
                    // Load source page document
                    PdfPage page = inputDocument.Pages[pageIdx];

                    // Add to target document
                    PdfPage newPage = outputDocument.AddPage(page);

                    if (stamps != null)
                    {
                        foreach (OverlayElement stamp in stamps)
                        {
                            stamp.SetPageNumber(pageIdx + 1);
                            stamp.SetDocumentNumber(docIdx + 1);
                            stamp.OverlayPage(newPage);
                        }
                    }

                    // Create the root bookmark. You can set the style and the color.
                    if (WithBookmarks && pageIdx == 0)
                    {
                        outline = outputDocument.Outlines.Add(inputDocument.Tag.ToString(), newPage, true, PdfOutlineStyle.Bold, XColors.Black);
                    }
                }

                ++docIdx;
            }

            MemoryStream memStream = new MemoryStream();
            outputDocument.Save(memStream);
            return memStream.GetBuffer();
        }
    }
}
