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
        protected Dictionary<string, object> variables = new Dictionary<string, object>();

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
            variables.Add("{DN}", 0);
            variables.Add("{PN}", 0);
        }

    }

    public class PageStamp : OverlayElement
    {
        public PageLocation Location { get; set; }

        public double LeftOffset { get; set; }
        public double RightOffset { get; set; }
        public double TopOffset { get; set; }
        public double BottomOffset { get; set; }

        private string stamp;

        public PageStamp(string stampFormat)
        {
            this.stamp = stampFormat;

            TimeFormat = "HH:mm:ss";
            DateFormat = "MM/dd/yyyy";
            TimeStampFormat = "MM/dd/yyyy HH:mm";
        }

        public void SetDocumentNumber(int documentNumber)
        {
            variables["{DN}"] = documentNumber;
        }

        public void SetPageNumber(int pageNumber)
        {
            variables["{PN}"] = pageNumber;
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

        private string makeStamp(string source)
        {
            string target = source;

            foreach (KeyValuePair<string, object> entry in variables)
            {
                target = target.Replace(entry.Key, entry.Value.ToString());
            }

            return target;
        }

        private void addRectangle(XRect rect, XGraphics gfx)
        {
            XBrush brush = XBrushes.WhiteSmoke;
            XPen pen = XPens.Purple;

            //XStringFormat format = new XStringFormat();

            gfx.DrawRectangle(pen, brush, rect);

            //gfx.DrawLine(XPens.YellowGreen, rect.Width / 2, 0, rect.Width / 2, rect.Height);
            //gfx.DrawLine(XPens.YellowGreen, 0, rect.Height / 2, rect.Width, rect.Height / 2);

            //gfx.DrawString("TopLeft", font, brush, rect, format);
            //XRect background = new XRect(20, 50- size.Height, size.Width, size.Height);

            //format.Alignment = XStringAlignment.Center;
            //gfx.DrawString("Center", font, brush, rect, format);


        }

        private void addRectangleWithText(XPoint position, string text, XGraphics gfx)
        {
            XBrush brush = XBrushes.WhiteSmoke;
            XPen pen = XPens.Purple;

            XSize size = gfx.MeasureString(text, font, XStringFormats.Default);

            XRect rect = new XRect(position.X, position.Y, size.Width, size.Height);

            gfx.DrawRectangle(pen, brush, rect);

            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Center;
            format.LineAlignment = XLineAlignment.Center;

            brush = XBrushes.Yellow;
            pen = XPens.Yellow;
            gfx.DrawString(text, font, brush, rect, format);


        }

        public void StampDocument(PdfDocument source, PdfDocument target)
        {
            int count = source.PageCount;

            for (int idx = 0; idx < count; ++idx)
            {
                SetPageNumber(idx);
                string theStamp = makeStamp(stamp);

                PdfPage sourcePage = source.Pages[idx];

                PdfPage targetPage = target.AddPage(sourcePage);

                XGraphics gfx = XGraphics.FromPdfPage(targetPage, XGraphicsPdfPageOptions.Append);
 
                XSize size = gfx.MeasureString(theStamp, font, XStringFormats.Default);

                XRect rect = new XRect(10, 10, size.Width, size.Height);
                XPoint point = new XPoint(10, 10);

                addRectangleWithText(point, theStamp, gfx);

                //addRectangle(rect, gfx);

                //gfx.DrawString(theStamp, font, XBrushes.Chocolate, 20, 50, XStringFormats.Default);
            }
        }
    }
}
