using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;

namespace PdfStamper.office
{
    class WordDocument
    {
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

                if (styleName == "Title")
                {
                    title.Add(text.ToString());
                }
                else if (styleName == "Subtitle")
                {
                    st.Add(text.ToString());
                }
                else if (styleName == "Heading 1")
                {
                    headings.Add(text.ToString() + "\n");
                }
            }

            sourceDocument.Close();
            wordApp.Quit();
        }

        public void openProtected(string path, string pwd)
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
