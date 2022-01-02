using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;

namespace PdfStamper
{
    public class PageStamp : OverlayElement
    {
        public XPen BoarderPen { get; set; }

        public XBrush BackgroundBrush { get; set; }

        public PageLocation Location { get; set; }

        public double LeftOffset { get; set; }
        public double RightOffset { get; set; }
        public double TopOffset { get; set; }
        public double BottomOffset { get; set; }

        private readonly string stamp;

        public PageStamp(string stampFormat)
        {
            stamp = stampFormat;

            BoarderPen = XPens.BlanchedAlmond;
            BackgroundBrush = XBrushes.BlanchedAlmond;
        }

        public XPoint GetPageLocation(PdfPage targetPage, XSize elementSize)
        {
            XPoint result = new XPoint(LeftOffset, TopOffset);

            double pageWidth = targetPage.Width.Point;
            _ = targetPage.Height.Point;

            double elementWidth = elementSize.Width;
            _ = elementSize.Height;

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

        private string MakeStamp(string source)
        {
            string target = source;

            foreach (KeyValuePair<string, object> entry in variables)
            {
                target = target.Replace(entry.Key, entry.Value.ToString());
            }

            return target;
        }

        private void AddRectangle(XRect rect, XGraphics gfx)
        {
            gfx.DrawRectangle(BoarderPen, BackgroundBrush, rect);
        }

        private void AddRectangleWithText(XPoint position, string text, XGraphics gfx)
        {
            XSize size = gfx.MeasureString(text, font, XStringFormats.Default);

            XRect rect = new XRect(position.X, position.Y, size.Width, size.Height);

            gfx.DrawRectangle(BoarderPen, BackgroundBrush, rect);

            XStringFormat format = new XStringFormat
            {
                Alignment = XStringAlignment.Center,
                LineAlignment = XLineAlignment.Center
            };

            gfx.DrawString(text, font, FontBrush, rect, format);
        }

        public override void OverlayPage(PdfPage thePage)
        {
            string theStamp = MakeStamp(stamp);

            XGraphics gfx = XGraphics.FromPdfPage(thePage, XGraphicsPdfPageOptions.Append);

            XSize size = gfx.MeasureString(theStamp, font, XStringFormats.Default);
            _ = new XRect(10, 10, size.Width, size.Height);
            XPoint point = new XPoint(10, 10);

            AddRectangleWithText(point, theStamp, gfx);

            gfx.Dispose();
        }

        public override void OverlayDocuemnt(PdfDocument source, PdfDocument target)
        {
            int count = source.PageCount;

            for (int idx = 0; idx < count; ++idx)
            {
                SetPageNumber(idx);
                string theStamp = MakeStamp(stamp);

                PdfPage sourcePage = source.Pages[idx];

                PdfPage targetPage = target.AddPage(sourcePage);

                XGraphics gfx = XGraphics.FromPdfPage(targetPage, XGraphicsPdfPageOptions.Append);

                XSize size = gfx.MeasureString(theStamp, font, XStringFormats.Default);
                _ = new XRect(10, 10, size.Width, size.Height);
                XPoint point = new XPoint(10, 10);

                AddRectangleWithText(point, theStamp, gfx);

                gfx.Dispose();
            }
        }
    }
}
