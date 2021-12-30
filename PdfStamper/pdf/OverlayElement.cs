using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Collections.Generic;

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

    public class FontFace
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

        public string FontName { get; set; }
        public double FontSize { get; set; }
        public XFontStyle FontStyle { get; set; }
        public XBrush FontBrush { get; set; }

        public OverlayElement()
        {
            InitSystemVariables();

            FontSize = 24;
            FontName = "Verdana";
            FontStyle = XFontStyle.Regular;
            FontBrush = XBrushes.DeepSkyBlue;

            constructFont();
        }

        public void constructFont()
        {
            font = new XFont(FontName, FontSize, FontStyle);
        }

        public void InitSystemVariables()
        {
            TimeFormat = "HH:mm:ss";
            DateFormat = "MM/dd/yyyy";
            TimeStampFormat = "MM/dd/yyyy HH:mm";

            variables.Add("{DN}", 0);
            variables.Add("{PN}", 0);
        }

        public void SetDocumentNumber(int documentNumber)
        {
            variables["{DN}"] = documentNumber;
        }

        public void SetPageNumber(int pageNumber)
        {
            variables["{PN}"] = pageNumber;
        }

        public abstract void OverlayPage(PdfPage thePage);

        public abstract void OverlayDocuemnt(PdfDocument source, PdfDocument target);
    }

}
