using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        protected XFont font = null;

        public string DateFormat { get; set; }

        public string TimeFormat { get; set; }

        public string TimeStampFormat { get; set; }

        public OverlayElement()
        {
            InitSystemVariables();
            font = new XFont("Verdana", 24, XFontStyle.Regular);

        }

        public void InitSystemVariables()
        {
        }

    }

    public class PageStamp : OverlayElement
    {
        public PageLocation Location { get; set; }

        public double LeftOffset { get; set; }
        public double RightOffset { get; set; }
        public double TopOffset { get; set; }
        public double BottomOffset { get; set; }

        public int DN { get; set; }
        public int PN { get; set; }

        private FormattableString stampFormat;

        public PageStamp(string stampFormat)
        {
            this.stampFormat = FormattableStringFactory.Create(stampFormat);

            TimeFormat = "HH:mm:ss";
            DateFormat = "MM/dd/yyyy";
            TimeStampFormat = "MM/dd/yyyy HH:mm";
        }

        public void SetDocumentNumber(int documentNumber)
        {
            DN = documentNumber;
        }

        public void SetPageNumber(int pageNumber)
        {
            PN = pageNumber;
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
                    result.X = (pageWidth - elementWidth) / 2;
                    break;
                default:
                    Console.WriteLine("Default");
                    break;
            }

            result.X = 20;
            result.Y = 50;

            return result;
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

                var ddd = DN;
                var ppp = PN;
                var name = "Mark";

                gfx.MeasureString(stampFormat.ToString(), font, XStringFormats.Default);

                gfx.DrawString(stampFormat.ToString(), font, XBrushes.Chocolate, 20, 50, XStringFormats.Default);
            }
        }
    }
}
