using QRPayApp.Models;
using QRPayApp.Services;
using System.Collections.ObjectModel;

namespace QRPayApp;

public partial class TrangBanHang : ContentPage
{
    DatabaseService db = new DatabaseService();
    QRPayService qr = new QRPayService();
    List<SanPham> kho;
    double tongTien = 0;

    public TrangBanHang() { InitializeComponent(); }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        kho = db.layKho();

        // reset lai so luong moi khi mo tab ban hang
        foreach (var sp in kho) sp.soLuong = 0;

        listSp.ItemsSource = new ObservableCollection<SanPham>(kho);
        tinhTien(null, null);
        imgQR.IsVisible = false;
        txtTim.Text = "";
    }

    void timKiem(object s, TextChangedEventArgs e)
    {
        var tim = e.NewTextValue?.ToLower() ?? "";

        // Thêm (x.ten ?? "") để chống lỗi null
        listSp.ItemsSource = kho.Where(x => (x.ten ?? "").ToLower().Contains(tim)).ToList();
    }

    void tinhTien(object s, TextChangedEventArgs e)
    {
        tongTien = kho.Sum(x => x.gia * x.soLuong);
        lblTong.Text = $"Tong: {tongTien:N0} VND";
    }

    void thanhToan(object s, EventArgs e)
    {
        if (tongTien <= 0)
        {
            DisplayAlert("Loi", "Chua chon san pham nao!", "OK");
            return;
        }

        var caiDat = db.layCaiDat();
        var anh = qr.taoAnhQR(caiDat.bin, caiDat.soTaiKhoan, tongTien);

        imgQR.Source = ImageSource.FromStream(() => new MemoryStream(anh));
        imgQR.IsVisible = true;

        int idHd = db.taoHd(new HoaDon { ngay = DateTime.Now.ToString("dd/MM/yyyy HH:mm"), tong = tongTien, qr = anh });

        foreach (var sp in kho.Where(x => x.soLuong > 0))
        {
            db.luuCt(new ChiTiet { idHoaDon = idHd, ten = sp.ten, sl = sp.soLuong });

            sp.tonKho -= sp.soLuong;
            db.suaSp(sp);
        }

        DisplayAlert("Xong", "Da tao QR va luu hoa don, kho hang da duoc cap nhat!", "OK");
    }
}