using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;

namespace PdfStamper
{
    class Watermark : OverlayElement
    {
        public string WaterMark { get; set; }

        public Watermark(string watermark)
        {
            WaterMark = watermark;

            font = new XFont(FontFace.TimesNewRoman, 48, XFontStyle.Italic);
        }

        public override void OverlayPage(PdfPage thePage)
        {
            // Get an XGraphics object for drawing beneath the existing content
            XGraphics gfx = XGraphics.FromPdfPage(thePage, XGraphicsPdfPageOptions.Prepend);

            // Get the size (in point) of the text
            XSize size = gfx.MeasureString(WaterMark, font);

            // Define a rotation transformation at the center of the page
            gfx.TranslateTransform(thePage.Width / 2, thePage.Height / 2);
            gfx.RotateTransform(-Math.Atan(thePage.Height / thePage.Width) * 180 / Math.PI);
            gfx.TranslateTransform(-thePage.Width / 2, -thePage.Height / 2);

            // Create a string format
            XStringFormat format = new XStringFormat
            {
                Alignment = XStringAlignment.Near,
                LineAlignment = XLineAlignment.Near
            };

            // Create a dimmed red brush
            XBrush brush = new XSolidBrush(XBrushes.DarkTurquoise);

            // Draw the string
            gfx.DrawString(WaterMark, font, brush, new XPoint((thePage.Width - size.Width) / 2, (thePage.Height - size.Height) / 2), format);

            gfx.Dispose();
        }

        public override void OverlayDocuemnt(PdfDocument source, PdfDocument target)
        {
            int count = source.PageCount;

            for (int idx = 0; idx < count; ++idx)
            {
                PdfPage sourcePage = source.Pages[idx];

                PdfPage targetPage = target.AddPage(sourcePage);

                // Get an XGraphics object for drawing beneath the existing content
                XGraphics gfx = XGraphics.FromPdfPage(targetPage, XGraphicsPdfPageOptions.Prepend);

                // Get the size (in point) of the text
                XSize size = gfx.MeasureString(WaterMark, font);

                // Define a rotation transformation at the center of the page
                gfx.TranslateTransform(targetPage.Width / 2, targetPage.Height / 2);
                gfx.RotateTransform(-Math.Atan(targetPage.Height / targetPage.Width) * 180 / Math.PI);
                gfx.TranslateTransform(-targetPage.Width / 2, -targetPage.Height / 2);

                // Create a string format
                XStringFormat format = new XStringFormat
                {
                    Alignment = XStringAlignment.Near,
                    LineAlignment = XLineAlignment.Near
                };

                // Create a dimmed red brush
                XBrush brush = new XSolidBrush(XBrushes.DarkTurquoise);

                // Draw the string
                gfx.DrawString(WaterMark, font, brush, new XPoint((targetPage.Width - size.Width) / 2, (targetPage.Height - size.Height) / 2), format);

                gfx.Dispose();
            }
        }
    }
}
