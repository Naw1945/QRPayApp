using SQLite;

namespace QRPayApp.Models;

public class SanPham
{
    [PrimaryKey, AutoIncrement] public int id { get; set; }
    public string ten { get; set; }
    public double gia { get; set; }
    public int tonKho { get; set; }
    [Ignore] public int soLuong { get; set; }
}

public class HoaDon
{
    [PrimaryKey, AutoIncrement] public int id { get; set; }
    public string ngay { get; set; }
    public double tong { get; set; }
    public byte[] qr { get; set; }
}

public class ChiTiet
{
    [PrimaryKey, AutoIncrement] public int id { get; set; }
    public int idHoaDon { get; set; }
    public string ten { get; set; }
    public int sl { get; set; }
}

public class CaiDat
{
    [PrimaryKey] public int id { get; set; }
    public string tenNganHang { get; set; }
    public string bin { get; set; }
    public string soTaiKhoan { get; set; }
}