using QRPayApp.Models;
using QRPayApp.Services;

namespace QRPayApp;

public class NganHang
{
    public string ten { get; set; }
    public string bin { get; set; }
}

public partial class TrangCaiDat : ContentPage
{
    DatabaseService db = new DatabaseService();
    List<NganHang> dsNganHang;
    CaiDat caiDatHienTai;

    public TrangCaiDat()
    {
        InitializeComponent();

        dsNganHang = new List<NganHang> {
            new NganHang { ten = "Vietcombank", bin = "970436" },
            new NganHang { ten = "MBBank", bin = "970422" },
            new NganHang { ten = "Techcombank", bin = "970407" },
            new NganHang { ten = "VietinBank", bin = "970415" },
            new NganHang { ten = "BIDV", bin = "970418" },
            new NganHang { ten = "ACB", bin = "970416" },
            new NganHang { ten = "VPBank", bin = "970432" },
            new NganHang { ten = "TPBank", bin = "970423" }
        };
        cboNganHang.ItemsSource = dsNganHang;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        caiDatHienTai = db.layCaiDat();

        txtSoTaiKhoan.Text = caiDatHienTai.soTaiKhoan;
        txtBin.Text = caiDatHienTai.bin;

        // load lai ngan hang da chon vao combobox
        var nhDangLuu = dsNganHang.FirstOrDefault(x => x.bin == caiDatHienTai.bin);
        if (nhDangLuu != null)
        {
            cboNganHang.SelectedItem = nhDangLuu;
        }
    }

    void chonNganHang(object sender, EventArgs e)
    {
        var nhChon = cboNganHang.SelectedItem as NganHang;
        if (nhChon != null)
        {
            txtBin.Text = nhChon.bin;
        }
    }

    void luuThongTin(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSoTaiKhoan.Text) || cboNganHang.SelectedItem == null)
        {
            DisplayAlert("Loi", "Vui long nhap day du thong tin", "OK");
            return;
        }

        var nhChon = cboNganHang.SelectedItem as NganHang;
        caiDatHienTai.tenNganHang = nhChon.ten;
        caiDatHienTai.bin = nhChon.bin;
        caiDatHienTai.soTaiKhoan = txtSoTaiKhoan.Text;

        db.luuCaiDat(caiDatHienTai);
        DisplayAlert("Thanh cong", "Da luu thong tin tai khoan!", "OK");
    }
}