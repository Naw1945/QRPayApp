namespace QRPayApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Thay MainPage = new MainPage(); mặc định thành dòng dưới đây
        // de ung dung chay vao cau truc Tab cua AppShell
        MainPage = new AppShell();
    }
}