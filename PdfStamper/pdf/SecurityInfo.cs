using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PdfStamper
{
    // #SO https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
    public class KeyGenerator
    {

        internal static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GetUniqueKey(int size)
        {
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }

    public class SecurityInfo
    {
        readonly PdfSecuritySettings pss = null;

        public static byte PermitAccessibilityExtractContent = 0b00000001;
        public static byte PermitAnnotations = 0b00000010;
        public static byte PermitAssembleDocument = 0b00000100;
        public static byte PermitExtractContent = 0b00001000;
        public static byte PermitFormsFill = 0b00010000;
        public static byte PermitFullQualityPrint = 0b00100000;
        public static byte PermitModifyDocument = 0b01000000;
        public static byte PermitPrint = 0b10000000;
        public static byte PermitAllPrint = (byte)(PermitPrint | PermitFullQualityPrint);

        public string UserPassword { get; set; }

        public string AdminPassword { get; set; }

        public SecurityInfo(PdfSecuritySettings pss)
        {
            this.pss = pss;

            ResetPasswords();
        }

        public void ResetPasswords()
        {
            UserPassword = KeyGenerator.GetUniqueKey(16);
            pss.UserPassword = UserPassword;
            AdminPassword = KeyGenerator.GetUniqueKey(24);
            pss.OwnerPassword = AdminPassword;
        }

        public void AllowPrint(bool allow)
        {
            pss.PermitPrint = allow;
            pss.PermitFullQualityPrint = allow;
        }

        public void ProtectDocument(bool allow)
        {
            pss.PermitAccessibilityExtractContent = !allow;
            pss.PermitAnnotations = !allow;
            pss.PermitAssembleDocument = !allow;
            pss.PermitExtractContent = !allow;
            pss.PermitModifyDocument = !allow;
        }

        public void SetPermitOption(int options)
        {
            pss.PermitAccessibilityExtractContent = (options & PermitAccessibilityExtractContent) > 0;
            pss.PermitAnnotations = (options & PermitAnnotations) > 0;
            pss.PermitAssembleDocument = (options & PermitAssembleDocument) > 0;
            pss.PermitExtractContent = (options & PermitExtractContent) > 0;
            pss.PermitFormsFill = (options & PermitFormsFill) > 0;
            pss.PermitFullQualityPrint = (options & PermitFullQualityPrint) > 0;
            pss.PermitModifyDocument = (options & PermitModifyDocument) > 0;
            pss.PermitPrint = (options & PermitPrint) > 0;
        }

    }
}
