using QRCoder;
using System;

namespace AgriTrace.API.Services
{
    public class QRCodeService
    {
        public string GenerateQRCode(string productCode)
        {
            // Đây là link Netlify của bạn
            string trackingUrl = $"https://farm-trace.netlify.app?code={productCode}";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(trackingUrl, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
                return $"data:image/png;base64,{Convert.ToBase64String(qrCodeAsPngByteArr)}";
            }
        }
    }
}
