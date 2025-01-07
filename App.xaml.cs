using ClienteMauiFrontend.Views;

namespace ClienteMauiFrontend
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CrearClienteView());
        }
    }
}
