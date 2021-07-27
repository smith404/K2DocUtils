using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Annotations;
using System;

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

            XRect rect = gfx.Transformer.WorldToDefaultPage(new XRect(new XPoint(20, 20), new XSize(200, 50)));

            PdfRubberStampAnnotation rsAnnot = new PdfRubberStampAnnotation();
            rsAnnot.Icon = Icon;
            rsAnnot.Flags = PdfAnnotationFlags.ReadOnly;
            rsAnnot.Rectangle = new PdfRectangle(rect);

            // Add the rubber stamp annotation to the page
            thePage.Annotations.Add(rsAnnot);
        }

        private void addBoarder(XRect rect, XGraphics gfx)
        {
            gfx.DrawRoundedRectangle(BoarderPen, BackgroundBrush, rect, new XSize(20, 20));
        }

    }

}
