using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Annotations;
using System;


namespace PdfStamper
{
    public class Annotation : OverlayElement
    {
        public XPen BoarderPen { get; set; }

        public XBrush BackgroundBrush { get; set; }

        public Annotation()
        {
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

            textAnnot = new PdfTextAnnotation();
            textAnnot.Title = "Annotation 2 (title)";
            textAnnot.Subject = "Annotation 2 (subject)";
            textAnnot.Contents = "This is the contents of the 2nd annotation.";
            textAnnot.Icon = PdfTextAnnotationIcon.Help;
            textAnnot.Color = XColors.LimeGreen;
            textAnnot.Opacity = 0.5;
            textAnnot.Open = true;

            gfx.DrawString("The second text annotation (opened)", font, XBrushes.Black, 30, 140, XStringFormats.Default);

            XRect rect = gfx.Transformer.WorldToDefaultPage(new XRect(new XPoint(30, 150), new XSize(30, 30)));
            textAnnot.Rectangle = new PdfRectangle(rect);

            // Add the annotation to the page
            thePage.Annotations.Add(textAnnot);
        }
    }
}
