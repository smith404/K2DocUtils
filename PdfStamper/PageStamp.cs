using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfStamper
{
    public enum PageLocation
    {
        North,
        South,
        East,
        West,
        NorthEast,
        NorthWeat,
        SouthEast,
        SouthWest
    }

    public class FontName
    {
        public static string Arial = "Arial";

        public static string Verdana = "Verdana";

        public static string TimesNewRoman = "Times New Roman";
    }

    public abstract class OverlayElement
    {
        protected Dictionary<string, string> variables = new Dictionary<string, string>();

        protected XFont font = null;

        public string DateFormat { get; set; }

        public string TimeFormat { get; set; }

        public string TimeStampFormat { get; set; }

        public OverlayElement()
        {
            font = new XFont("Verdana", 24, XFontStyle.Regular);

        }

        public void AddVariable(string name, string value)
        {
            variables.Add(name, value);
        }

        public string ApplyDictionary(string inStr)
        {
            foreach (KeyValuePair<string, string> entry in variables)
            {
                inStr = inStr.Replace(entry.Key, entry.Value);
            }

            return inStr;
        }

        public void InitSystemVariables(string name, string value)
        {
            variables.Add("[T]", DateTime.Now.ToString(TimeFormat));
            variables.Add("[D]", DateTime.Now.ToString(DateFormat));
            variables.Add("[TS]", DateTime.Now.ToString(TimeStampFormat));
        }

    }

    public class PageStamp : OverlayElement
    {
        public PageLocation Location { get; set; }

        public double LeftOffset { get; set; }
        public double RightOffset { get; set; }
        public double TopOffset { get; set; }
        public double BottomOffset { get; set; }

        private string stampFormat = "CMP/DIV:{0:D4}";

        public PageStamp(string stampFormat)
        {
            this.stampFormat = stampFormat;

            TimeFormat = "HH:mm:ss";
            DateFormat = "MM/dd/yyyy";
            TimeStampFormat = "MM/dd/yyyy HH:mm";


        }

        public void SetDocumentNumber(int documentNumber)
        {
            variables.Add("[DN]", Convert.ToString(documentNumber));
        }

        public void SetPageNumber(int pageNumber)
        {
            variables.Add("[PN]", Convert.ToString(pageNumber));
        }

        public XPoint GetPageLocation(PdfPage targetPage, XSize elementSize)
        {
            XPoint result = new XPoint(LeftOffset, TopOffset);


            double pageWidth = targetPage.Width.Point;
            double pageHeight = targetPage.Height.Point;

            double elementWidth = elementSize.Width;
            double elementHeight = elementSize.Height;

            switch (Location)
            {
                case PageLocation.North:
                    result.X = (pageWidth-elementWidth) / 2;
                    break;
                default:
                    Console.WriteLine("Default");
                    break;
            }

            result.X = 20;
            result.Y = 50;

            return result;
        }

        public string GenerateStamp()
        {
            string stamp = String.Format(stampFormat, 1);

            return stamp;
        }

        public void StampDocument(PdfDocument source, PdfDocument target)
        {
            int count = source.PageCount;

            for (int idx = 0; idx < count; ++idx)
            {
                SetPageNumber(idx);

                PdfPage sourcePage = source.Pages[idx];

                PdfPage targetPage = target.AddPage(sourcePage);

                XGraphics gfx = XGraphics.FromPdfPage(targetPage, XGraphicsPdfPageOptions.Append);

                gfx.MeasureString(stampFormat, font, XStringFormats.Default);

                gfx.DrawString(stampFormat, font, XBrushes.Chocolate, 20, 50, XStringFormats.Default);
            }
        }
    }
}
