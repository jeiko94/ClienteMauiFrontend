using CommunityToolkit.Maui;

namespace ClienteMauiFrontend
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Registrar CommunityToolkit.Maui
            var builder = MauiApp.CreateBuilder()
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .Build();

            MainPage = new AppShell();
        }
    }
}
