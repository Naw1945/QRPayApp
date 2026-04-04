using QRPayApp.Models;
using QRPayApp.Services;

namespace QRPayApp;

public partial class TrangKhoHang : ContentPage
{
    DatabaseService db = new DatabaseService();
    SanPham dangChon;

    public TrangKhoHang() { InitializeComponent(); }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        load();
    }

    void load()
    {
        listKho.ItemsSource = db.layKho();
        txtTen.Text = "";
        txtGia.Text = "";
        txtTonKho.Text = "";
        dangChon = null;
        listKho.SelectedItem = null;
    }

    void chon(object s, SelectionChangedEventArgs e)
    {
        dangChon = e.CurrentSelection.FirstOrDefault() as SanPham;
        if (dangChon != null)
        {
            txtTen.Text = dangChon.ten;
            txtGia.Text = dangChon.gia.ToString();
            txtTonKho.Text = dangChon.tonKho.ToString();
        }
    }

    void them(object s, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtTen.Text) || string.IsNullOrEmpty(txtGia.Text) || string.IsNullOrEmpty(txtTonKho.Text)) return;

        db.themSp(new SanPham
        {
            ten = txtTen.Text,
            gia = double.Parse(txtGia.Text),
            tonKho = int.Parse(txtTonKho.Text)
        });
        load();
    }

    void sua(object s, EventArgs e)
    {
        if (dangChon == null) return;

        dangChon.ten = txtTen.Text;
        dangChon.gia = double.Parse(txtGia.Text);
        dangChon.tonKho = int.Parse(txtTonKho.Text);

        db.suaSp(dangChon);
        load();
    }

    void xoa(object s, EventArgs e)
    {
        if (dangChon != null)
        {
            db.xoaSp(dangChon);
            load();
        }
    }
}