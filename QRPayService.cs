using System;
using System.Text;
using QRCoder;

namespace QRPayApp.Services;

public class QRPayService
{
    // ham tao chuoi vietqr chuan theo logic ban cung cap
    public string taoPayloadVietQR(string bin, string stk, string tien)
    {
        string payload = "000201010211";

        string sub38_00 = "0006" + bin;
        string sub38_01 = "01" + stk.Length.ToString("D2") + stk;

        string inner38 = "0010A00000072701" + (sub38_00.Length + sub38_01.Length).ToString("D2") + sub38_00 + sub38_01 + "0208QRIBFTTA";
        payload += "38" + inner38.Length.ToString("D2") + inner38;

        payload += "5303704";

        if (!string.IsNullOrEmpty(tien))
        {
            payload += "54" + tien.Length.ToString("D2") + tien;
        }

        payload += "5802VN";
        payload += "6304";

        // tinh toan va ghep ma crc16 vao cuoi
        payload += tinhToanCRC16(payload);

        return payload;
    }

    // thuat toan tinh crc16
    private string tinhToanCRC16(string noiDung)
    {
        ushort crc = 0xFFFF;
        byte[] bytes = Encoding.UTF8.GetBytes(noiDung);
        foreach (byte b in bytes)
        {
            crc ^= (ushort)(b << 8);
            for (int i = 0; i < 8; i++)
            {
                if ((crc & 0x8000) != 0)
                    crc = (ushort)((crc << 1) ^ 0x1021);
                else
                    crc <<= 1;
            }
        }
        return crc.ToString("X4");
    }

    // ham tao anh qr (tra ve byte[] de maui hien thi va luu sqlite duoc)
    public byte[] taoAnhQR(string bin, string stk, double tien)
    {
        try
        {
            // goi ham tao chuoi vietqr o tren
            string payload = taoPayloadVietQR(bin, stk, tien.ToString());

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);

                // su dung pngbyteqrcode thay cho bitmap de tuong thich maui
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20);
                }
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
}