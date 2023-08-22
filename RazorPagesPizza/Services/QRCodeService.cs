using QRCoder;

namespace RazorPagesPizza.Services
{
    public class QRCodeService
    {
        private readonly QRCodeGenerator _generator;

        public QRCodeService(QRCodeGenerator generator)
        {
            _generator = generator;
        }

        public string GetQRCodeAsBase64(string textToEncode)
        {
            // QRCodeGenerator.ECCLevel.Q => error correction level for QR Codes = High
            QRCodeData qrCodeData = _generator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);

            return Convert.ToBase64String(qrCode.GetGraphic(4));
        }
    }
}