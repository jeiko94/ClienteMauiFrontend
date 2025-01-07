using ClienteMauiFrontend.Models;
using ClienteMauiFrontend.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ClienteMauiFrontend.ViewModels
{
    public class CrearClienteViewModel : INotifyPropertyChanged
    {

        private readonly ApiServices _apiServices;


        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public ICommand CrearClienteCommand { get; }

        public CrearClienteViewModel()
        {
            _apiServices = new ApiServices();
            CrearClienteCommand = new Command(async () => await CrearCliente());
        }

        public async Task CrearCliente()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Debe ingresar el nombre y el email", "Aceptar");
                return;
            }

            var cliente = new Cliente
            {
                Nombre = this.Nombre,
                Email = this.Email
            };

            var respuesta = await _apiServices.CrearClienteAsync(cliente);

            if (respuesta.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Cliente creado correctamente", "Aceptar");

                //Limpiar campos
                Nombre = string.Empty;
                Email = string.Empty;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", respuesta.Message, "Aceptar");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
