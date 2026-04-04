using QRPayApp.Models;
using QRPayApp.Services;

namespace QRPayApp;

public partial class TrangLichSu : ContentPage
{
    DatabaseService db = new DatabaseService();

    public TrangLichSu() { InitializeComponent(); }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        listHd.ItemsSource = db.layHd();
    }

    async void xemLaiQR(object s, SelectedItemChangedEventArgs e)
    {
        var hd = e.SelectedItem as HoaDon;
        if (hd == null) return;

        // 1. Lay danh sach cac mon do cua hoa don nay
        var dsMonDo = db.layChiTietHd(hd.id);

        // 2. Tao danh sach hien thi mon do (bang code C#)
        var listMonDo = new ListView
        {
            ItemsSource = dsMonDo,
            HeightRequest = 200 // Gioi han chieu cao
        };
        listMonDo.ItemTemplate = new DataTemplate(() => {
            var cell = new TextCell();
            cell.SetBinding(TextCell.TextProperty, "ten");
            cell.SetBinding(TextCell.DetailProperty, new Binding("sl", stringFormat: "So luong: {0}"));
            return cell;
        });

        // 3. Tao cho hien thi QR
        Image anhQR = new Image { WidthRequest = 250, HeightRequest = 250 };
        if (hd.qr != null && hd.qr.Length > 0)
        {
            anhQR.Source = ImageSource.FromStream(() => new MemoryStream(hd.qr));
        }

        // 4. Ghep tat ca vao mot man hinh
        var layout = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = {
                new Label { Text = $"Thoi gian: {hd.ngay}", FontAttributes = FontAttributes.Bold, FontSize = 18 },
                new Label { Text = $"Tong tien: {hd.tong:N0} VND", TextColor = Colors.Red, FontAttributes = FontAttributes.Bold, FontSize = 18 },
                new Label { Text = "Cac mon da mua:", FontAttributes = FontAttributes.Bold },
                listMonDo,
                new Label { Text = "Ma QR thanh toan:", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                anhQR
            }
        };

        // 5. Chuyen sang trang chi tiet
        await Navigation.PushAsync(new ContentPage
        {
            Title = "Chi Tiet Hoa Don",
            Content = new ScrollView { Content = layout }
        });

        listHd.SelectedItem = null; // bo chon
    }
}