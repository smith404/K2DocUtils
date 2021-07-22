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
    public class RubberStamp : OverlayElement
    {
        public PdfRubberStampAnnotationIcon Icon { get; set; }

        public XPen BoarderPen { get; set; }

        public XBrush BackgroundBrush { get; set; }

        public RubberStamp(PdfRubberStampAnnotationIcon icon)
        {
            Icon = icon;

            BoarderPen = XPens.Green;
            BackgroundBrush = XBrushes.OrangeRed;
            FontBrush = XBrushes.BlueViolet;
        }

        public override void OverlayDocuemnt(PdfDocument source, PdfDocument target)
        {
            throw new NotImplementedException();
        }

        public override void OverlayPage(PdfPage thePage)
        {
            XGraphics gfx = XGraphics.FromPdfPage(thePage, XGraphicsPdfPageOptions.Append);


            // Create a PDF text annotation
            PdfTextAnnotation textAnnot = new PdfTextAnnotation();
            textAnnot.Title = "This is the title";
            textAnnot.Subject = "This is the subject";
            textAnnot.Contents = "This is the contents of the annotation.\rThis is the 2nd line.";
            textAnnot.Icon = PdfTextAnnotationIcon.Note;

            gfx.DrawString("The first text annotation", font, XBrushes.Black, 30, 50, XStringFormats.Default);

            // Convert rectangle from world space to page space. This is necessary because the annotation is
            // placed relative to the bottom left corner of the page with units measured in point.
            XRect rect = gfx.Transformer.WorldToDefaultPage(new XRect(new XPoint(30, 60), new XSize(30, 30)));
            textAnnot.Rectangle = new PdfRectangle(rect);

            // Add the annotation to the page
            thePage.Annotations.Add(textAnnot);

            textAnnot = new PdfTextAnnotation();
            textAnnot.Title = "Annotation 2 (title)";
            textAnnot.Subject = "Annotation 2 (subject)";
            textAnnot.Contents = "This is the contents of the 2nd annotation.";
            textAnnot.Icon = PdfTextAnnotationIcon.Help;
            textAnnot.Color = XColors.LimeGreen;
            textAnnot.Opacity = 0.5;
            textAnnot.Open = true;

            gfx.DrawString("The second text annotation (opened)", font, XBrushes.Black, 30, 140, XStringFormats.Default);

            rect = gfx.Transformer.WorldToDefaultPage(new XRect(new XPoint(30, 150), new XSize(30, 30)));
            textAnnot.Rectangle = new PdfRectangle(rect);

            // Add the 2nd annotation to the page
            thePage.Annotations.Add(textAnnot);

            // Create a so called rubber stamp annotation. I'm not sure if it is useful, but at least
            // it looks impressive...
            PdfRubberStampAnnotation rsAnnot = new PdfRubberStampAnnotation();
            rsAnnot.Icon = PdfRubberStampAnnotationIcon.TopSecret;
            rsAnnot.Flags = PdfAnnotationFlags.ReadOnly;

            rect = gfx.Transformer.WorldToDefaultPage(new XRect(new XPoint(100, 400), new XSize(350, 150)));
            rsAnnot.Rectangle = new PdfRectangle(rect);

            // Add the rubber stamp annotation to the page
            thePage.Annotations.Add(rsAnnot);


            //XRect rect = gfx.Transformer.WorldToDefaultPage(new XRect(new XPoint(20, 20), new XSize(200, 50)));
            //rsAnnot.Rectangle = new PdfRectangle(rect);

            // Add the rubber stamp annotation to the page
            //thePage.Annotations.Add(rsAnnot);
            //addBoarder(rect, gfx);
        }

        private void addBoarder(XRect rect, XGraphics gfx)
        {
            gfx.DrawRoundedRectangle(BoarderPen, BackgroundBrush, rect,new XSize(20,20));
        }

    }

    public class PageStamp : OverlayElement
    {
        public XPen BoarderPen { get; set; }

        public XBrush BackgroundBrush { get; set; }

        public PageLocation Location { get; set; }

        public double LeftOffset { get; set; }
        public double RightOffset { get; set; }
        public double TopOffset { get; set; }
        public double BottomOffset { get; set; }

        private string stamp;

        public PageStamp(string stampFormat)
        {
            this.stamp = stampFormat;

            BoarderPen = XPens.Black;
            BackgroundBrush = XBrushes.BlanchedAlmond;
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
            gfx.DrawRectangle(BoarderPen, BackgroundBrush, rect);
        }

        private void addRectangleWithText(XPoint position, string text, XGraphics gfx)
        {
            XSize size = gfx.MeasureString(text, font, XStringFormats.Default);

            XRect rect = new XRect(position.X, position.Y, size.Width, size.Height);

            gfx.DrawRectangle(BoarderPen, BackgroundBrush, rect);

            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Center;
            format.LineAlignment = XLineAlignment.Center;

            gfx.DrawString(text, font, FontBrush, rect, format);
        }

        public override void OverlayPage(PdfPage thePage)
        {
            string theStamp = makeStamp(stamp);

            XGraphics gfx = XGraphics.FromPdfPage(thePage, XGraphicsPdfPageOptions.Append);

            XSize size = gfx.MeasureString(theStamp, font, XStringFormats.Default);

            XRect rect = new XRect(10, 10, size.Width, size.Height);
            XPoint point = new XPoint(10, 10);

            addRectangleWithText(point, theStamp, gfx);

            gfx.Dispose();
        }

        public override void OverlayDocuemnt(PdfDocument source, PdfDocument target)
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

                gfx.Dispose();
            }
        }
    }
}
