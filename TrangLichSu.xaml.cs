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


        var dsMonDo = db.layChiTietHd(hd.id);

        var listMonDo = new ListView
        {
            ItemsSource = dsMonDo,
            HeightRequest = 200 
        };
        listMonDo.ItemTemplate = new DataTemplate(() => {
            var cell = new TextCell();
            cell.SetBinding(TextCell.TextProperty, "ten");
            cell.SetBinding(TextCell.DetailProperty, new Binding("sl", stringFormat: "Số lượng: {0}"));
            return cell;
        });


        Image anhQR = new Image { WidthRequest = 250, HeightRequest = 250 };
        if (hd.qr != null && hd.qr.Length > 0)
        {
            anhQR.Source = ImageSource.FromStream(() => new MemoryStream(hd.qr));
        }


        var layout = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = {
                new Label { Text = $"Thời gian: {hd.ngay}", FontAttributes = FontAttributes.Bold, FontSize = 18 },
                new Label { Text = $"Tổng tiền: {hd.tong:N0} VND", TextColor = Colors.Red, FontAttributes = FontAttributes.Bold, FontSize = 18 },
                new Label { Text = "Các món đã mua :", FontAttributes = FontAttributes.Bold },
                listMonDo,
                new Label { Text = "Mã QR thanh toán:", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                anhQR
            }
        };


        await Navigation.PushAsync(new ContentPage
        {
            Title = "Chi Tiết",
            Content = new ScrollView { Content = layout }
        });

        listHd.SelectedItem = null;
    }
}