using System.Drawing;
using BarcodeStandard;

namespace Encomendas.Shared.Utils
{
    public static class BarcodeService
    {
        public static string GenerateBarcodeBase64(string text)
        {
            var barcode = new Barcode
            {
                IncludeLabel = true,
                Alignment = AlignmentPositions.CENTER,
                LabelPosition = LabelPositions.BOTTOMCENTER
            };

            Image image = barcode.Encode(TYPE.CODE128, text, Color.Black, Color.White, 300, 100);
            using var ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
