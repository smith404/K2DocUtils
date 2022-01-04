using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;

namespace PdfStamper.office
{
    class WordDocument
    {
        private const string Subtitle = "Subtitle";
        private const string Title = "Title";
        private const string Heading1 = "Heading 1";

        // Add missing obhect to simplify calling
        private object missing = System.Type.Missing;

        public WordDocument(string path)
        {
            Application wordApp = new Application();
            Document sourceDocument = new Document();

            object fileName = path;

            sourceDocument = wordApp.Documents.Open(ref fileName, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing);

            String read = string.Empty;
            List<string> data = new List<string>();
            for (int i = 0; i < sourceDocument.Paragraphs.Count; i++)
            {
                string temp = sourceDocument.Paragraphs[i + 1].Range.Text.Trim();
                if (temp != string.Empty)
                {
                    data.Add(temp);
                }
            }

            List<String> title = new List<String>();
            List<String> st = new List<String>();
            List<String> headings = new List<String>();

            foreach (Paragraph paragraph in sourceDocument.Paragraphs)
            {
                Microsoft.Office.Interop.Word.Style style = paragraph.get_Style() as Style;
                string styleName = style.NameLocal;
                string text = paragraph.Range.Text;

                switch (styleName)
                {
                    case Title:
                        title.Add(text.ToString());
                        break;
                    case Subtitle:
                        st.Add(text.ToString());
                        break;
                    case Heading1:
                        headings.Add(text.ToString() + Environment.NewLine);
                        break;
                    default:
                        break;
                }
            }

            sourceDocument.Close();
            wordApp.Quit();
        }

        public void OpenProtected(string path, string pwd)
        {
            Application wordApp = null;
            Document sourceDocument = null;

            object fileName = path;
            object isVisible = false;
            object readOnly = true;
            object password = pwd;

            try
            {
                wordApp = new Application();

                sourceDocument = wordApp.Documents.Open(ref fileName, ref missing, ref readOnly,
                ref missing, ref password, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref isVisible, ref missing,
                ref missing, ref missing, ref missing);

            }
            finally
            {
                if (sourceDocument != null)
                {
                    sourceDocument.Close(ref missing, ref missing, ref missing);
                    sourceDocument = null;
                }

                if (wordApp != null)
                {
                    wordApp.Quit(ref missing, ref missing, ref missing);
                    wordApp = null;
                }
            }
        }
    }
}
