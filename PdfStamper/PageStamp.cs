using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
        public static String Arial = "Arial";

        public static String Verdana = "Verdana";

        public static String TimesNewRoman = "Times New Roman";
    }

    public class PageStamp
    {
        public PageLocation Location { get; set; }

        public double LeftOffset { get; set; }
        public double RightOffset { get; set; }
        public double TopOffset { get; set; }
        public double BottomOffset { get; set; }

        private string stampFormat = "CMP/DIV:{0:D4}";
        private XFont font = null;

        public XPoint getPageLocation(PdfPage targetPage, XSize elementSize)
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

        public PageStamp(string stampFormat)
        {
            this.stampFormat = stampFormat;

            font = new XFont("Verdana", 24, XFontStyle.Regular);
        }

        public string generateStamp()
        {
            string stamp = String.Format(stampFormat, 1);

            return stamp;
        }

        public void stampDocument(PdfDocument source, PdfDocument target)
        {
            int count = source.PageCount;

            for (int idx = 0; idx < count; ++idx)
            {
                PdfPage sourcePage = source.Pages[idx];

                PdfPage targetPage = target.AddPage(sourcePage);

                XGraphics gfx = XGraphics.FromPdfPage(targetPage, XGraphicsPdfPageOptions.Append);

                gfx.MeasureString(stampFormat, font, XStringFormats.Default);

                gfx.DrawString(stampFormat, font, XBrushes.Chocolate, 20, 50, XStringFormats.Default);
            }
        }
    }
}
