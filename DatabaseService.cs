using SQLite;
using QRPayApp.Models;
using System.IO;

namespace QRPayApp.Services;

public class DatabaseService
{
    SQLiteConnection db;

    public DatabaseService()
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, "mayPOS1.db");
        db = new SQLiteConnection(path);

        db.CreateTable<SanPham>();
        db.CreateTable<HoaDon>();
        db.CreateTable<ChiTiet>();
        db.CreateTable<CaiDat>();

        khoiTaoDuLieuMau();

        if (db.Table<CaiDat>().Count() == 0)
        {
            db.Insert(new CaiDat { id = 1, tenNganHang = "MBBank", bin = "970422", soTaiKhoan = "0123456789" });
        }
    }

    private void khoiTaoDuLieuMau()
    {
        if (db.Table<SanPham>().Count() == 0)
        {
            var danhSachMau = new List<SanPham> {
                new SanPham { ten = "Mì Hảo Hảo Tôm Chua Cay", gia = 4500, tonKho = 100 },
                new SanPham { ten = "Mì Omachi Xốt Vang", gia = 8000, tonKho = 50 },
                new SanPham { ten = "Mì Kokomi Đại", gia = 3500, tonKho = 120 },
                new SanPham { ten = "Phở Bò Vifon", gia = 7000, tonKho = 60 },
                new SanPham { ten = "Bún Bò Huế Đệ Nhất", gia = 8500, tonKho = 40 },
                new SanPham { ten = "Sữa Tươi Vinamilk Có Đường 180ml", gia = 8500, tonKho = 200 },
                new SanPham { ten = "Sữa Chua Vinamilk Nha Đam", gia = 6500, tonKho = 150 },
                new SanPham { ten = "Sữa Đặc Ông Thọ Đỏ", gia = 22000, tonKho = 30 },
                new SanPham { ten = "Sữa TH True Milk Ít Đường", gia = 9000, tonKho = 180 },
                new SanPham { ten = "Nước Lọc Lavie 500ml", gia = 5000, tonKho = 300 },
                new SanPham { ten = "Nước Khoáng Aquafina 500ml", gia = 5000, tonKho = 300 },
                new SanPham { ten = "Coca Cola Lon 320ml", gia = 10000, tonKho = 240 },
                new SanPham { ten = "Pepsi Lon 320ml", gia = 10000, tonKho = 240 },
                new SanPham { ten = "Sting Dâu Chai Nhựa", gia = 11000, tonKho = 120 },
                new SanPham { ten = "Redbull Lon (Bò Húc)", gia = 15000, tonKho = 100 },
                new SanPham { ten = "Nước Mắm Nam Ngư 750ml", gia = 45000, tonKho = 20 },
                new SanPham { ten = "Nước Tương Chinsu", gia = 18000, tonKho = 30 },
                new SanPham { ten = "Tương Ớt Chinsu 250g", gia = 15000, tonKho = 50 },
                new SanPham { ten = "Dầu Ăn Simply 1L", gia = 65000, tonKho = 15 },
                new SanPham { ten = "Dầu Ăn Tường An 1L", gia = 55000, tonKho = 25 },
                new SanPham { ten = "Đường Cát Trắng Biên Hòa 1kg", gia = 28000, tonKho = 40 },
                new SanPham { ten = "Muối Tinh I-ốt Hải Châu 500g", gia = 6000, tonKho = 80 },
                new SanPham { ten = "Bột Ngọt Ajinomoto 400g", gia = 32000, tonKho = 25 },
                new SanPham { ten = "Hạt Nêm Knorr Thịt Thăn 400g", gia = 38000, tonKho = 30 },
                new SanPham { ten = "Gạo ST25 Ông Cua 5kg", gia = 180000, tonKho = 10 },
                new SanPham { ten = "Trứng Gà Ba Huân (Hộp 10 Quả)", gia = 35000, tonKho = 20 },
                new SanPham { ten = "Trứng Vịt (Quả)", gia = 4000, tonKho = 100 },
                new SanPham { ten = "Xúc Xích Vissan Heo (Gói 4 Cây)", gia = 22000, tonKho = 50 },
                new SanPham { ten = "Xúc Xích Ponnie", gia = 15000, tonKho = 60 },
                new SanPham { ten = "Bánh Mì Sandwich Kinh Đô", gia = 25000, tonKho = 15 },
                new SanPham { ten = "Bánh Mì Đặc Ruột", gia = 5000, tonKho = 50 },
                new SanPham { ten = "Bánh Gạo One One", gia = 35000, tonKho = 20 },
                new SanPham { ten = "Bánh Chocopie (Hộp 12 Cái)", gia = 55000, tonKho = 30 },
                new SanPham { ten = "Snack Oishi Tôm Cay", gia = 6000, tonKho = 100 },
                new SanPham { ten = "Bim Bim Khoai Tây Lay's", gia = 12000, tonKho = 80 },
                new SanPham { ten = "Kẹo Ngậm Alpenliebe", gia = 15000, tonKho = 40 },
                new SanPham { ten = "Kẹo Cao Su Doublemint", gia = 35000, tonKho = 20 },
                new SanPham { ten = "Kem Đánh Răng P/S Bảo Vệ 123", gia = 35000, tonKho = 40 },
                new SanPham { ten = "Kem Đánh Răng Colgate", gia = 38000, tonKho = 35 },
                new SanPham { ten = "Bàn Chải Đánh Răng Sensodyne", gia = 45000, tonKho = 25 },
                new SanPham { ten = "Dầu Gội Clear Men 630g", gia = 165000, tonKho = 12 },
                new SanPham { ten = "Dầu Gội Sunsilk Chó Óng Mượt", gia = 145000, tonKho = 15 },
                new SanPham { ten = "Sữa Tắm Lifebuoy 800g", gia = 155000, tonKho = 10 },
                new SanPham { ten = "Xà Phòng Cục Lifebuoy", gia = 16000, tonKho = 50 },
                new SanPham { ten = "Bột Giặt OMO 800g", gia = 45000, tonKho = 30 },
                new SanPham { ten = "Nước Giặt Ariel Cửa Trên 2.4kg", gia = 185000, tonKho = 15 },
                new SanPham { ten = "Nước Xả Vải Downy Đam Mê 1.5L", gia = 120000, tonKho = 18 },
                new SanPham { ten = "Nước Rửa Chén Sunlight Chanh 750g", gia = 32000, tonKho = 40 },
                new SanPham { ten = "Giấy Vệ Sinh Watersilk (Lốc 10 Cuộn)", gia = 65000, tonKho = 25 },
                new SanPham { ten = "Khăn Giấy Ướt Baby", gia = 25000, tonKho = 50 }
            };

            foreach (var sp in danhSachMau)
            {
                db.Insert(sp);
            }
        }
    }

    public List<SanPham> layKho() => db.Table<SanPham>().ToList();
    public void themSp(SanPham sp) => db.Insert(sp);
    public void suaSp(SanPham sp) => db.Update(sp);
    public void xoaSp(SanPham sp) => db.Delete(sp);

    public int taoHd(HoaDon hd) { db.Insert(hd); return hd.id; }
    public void luuCt(ChiTiet ct) => db.Insert(ct);
    public List<HoaDon> layHd() => db.Table<HoaDon>().OrderByDescending(x => x.id).ToList();
    public List<ChiTiet> layChiTietHd(int idHd) => db.Table<ChiTiet>().Where(x => x.idHoaDon == idHd).ToList();

    public CaiDat layCaiDat() => db.Table<CaiDat>().FirstOrDefault();
    public void luuCaiDat(CaiDat cd) => db.Update(cd);
}