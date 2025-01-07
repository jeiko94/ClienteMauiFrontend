using ClienteMauiFrontend.Models;
using ClienteMauiFrontend.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ClienteMauiFrontend.ViewModels
{
    public class ClienteViewModel : INotifyPropertyChanged // Permite enviar y recibir información en las variable 
    {
        private readonly ApiServices _apiServices;

        public ClienteViewModel()
        {
            _apiServices = new ApiServices();

            CrearClienteCommand = new Command(async () => await CrearCliente());
            ListarClientesCommand = new Command(async () => await ListarClientes());
            BuscarClienteCommand = new Command(async () => await BuscarClientes());

            Clientes = new ObservableCollection<Cliente>();
        }



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

        //properties para listar y buscar
        public ObservableCollection<Cliente> Clientes { get; set; }

        private string _criterioBusqueda;
        public string CriterioBusqueda
        {
            get => _criterioBusqueda;
            set { _criterioBusqueda = value; OnPropertyChanged(); }
        }

        //Commands
        public ICommand CrearClienteCommand { get; }
        public ICommand ListarClientesCommand { get; }
        public ICommand BuscarClienteCommand { get; }

        //Métodos
        private async Task CrearCliente()
        {
            var cliente = new Cliente
            {
                Nombre = this.Nombre,
                Email = this.Email
            };

            var respuesta = await _apiServices.CrearClienteAsync(cliente);

            if (respuesta.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Exito", respuesta.Message, "Ok");

                //Limpiar los campos
                Nombre = string.Empty;
                Email = string.Empty;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", respuesta.Message, "OK");
            }
        }

        public async Task ListarClientes()
        {
            var respuesta = await _apiServices.ListarClientesAsync();

            if (respuesta.IsSuccess && respuesta.Result != null)
            {
                Clientes.Clear();
                foreach (var cliente in respuesta.Result)
                {
                    Clientes.Add(cliente);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", respuesta.Message, "OK");
            }
        }

        public async Task BuscarClientes()
        {
            if (string.IsNullOrWhiteSpace(CriterioBusqueda))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar un criterio de búsqueda", "OK");
                return;
            }

            var respuesta = await _apiServices.BuscarClienteAsync(CriterioBusqueda);

            if (respuesta.IsSuccess && respuesta.Result != null)
            {
                Clientes.Clear();

                foreach (var cliente in respuesta.Result)
                {
                    Clientes.Add(cliente);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", respuesta.Message, "OK");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nombrePropiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }
    }
}
