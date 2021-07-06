using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfStamper
{
    class Watermark : OverlayElement
    {
        public string WaterMark { get; set; }

        public Watermark(string watermark)
        {
            WaterMark = watermark;

            font = new XFont(FontName.TimesNewRoman, 48, XFontStyle.Italic);
        }

        // http://www.pdfsharp.com/PDFsharp/index.php?option=com_content&task=view&id=40&Itemid=51
        public void WatermarkDocument(PdfDocument source, PdfDocument target)
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
                XStringFormat format = new XStringFormat();
                format.Alignment = XStringAlignment.Near;
                format.LineAlignment = XLineAlignment.Near;

                // Create a dimmed red brush
                XBrush brush = new XSolidBrush(XBrushes.DarkTurquoise);

                // Draw the string
                gfx.DrawString(WaterMark, font, brush, new XPoint((targetPage.Width - size.Width) / 2, (targetPage.Height - size.Height) / 2), format);
            }
        }
    }
}
