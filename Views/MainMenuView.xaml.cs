using ClienteMauiFrontend.ViewModels;

namespace ClienteMauiFrontend.Views;

public partial class MainMenuView : ContentPage
{
	public MainMenuView()
	{
		InitializeComponent();
		BindingContext = new MainMenuViewModel();
    }
}