using ClienteMauiFrontend.Models;
using ClienteMauiFrontend.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ClienteMauiFrontend.ViewModels
{
    public class ListarClientesViewModel : INotifyPropertyChanged
    {
        private readonly ApiServices _apiService;

        public ObservableCollection<Cliente> Clientes { get; set; }

        public ICommand CargarClientesCommand { get; }
        public ICommand BorrarClienteCommand { get; }
        public ICommand ClienteSeleccionadoCommand { get; }

        public ListarClientesViewModel()
        {
            _apiService = new ApiServices();
            Clientes = new ObservableCollection<Cliente>();
            CargarClientesCommand = new Command(async () => await CargarClientes());
            BorrarClienteCommand = new Command<int>(async (id) => await BorrarClienteAsync(id));
            ClienteSeleccionadoCommand = new Command<Cliente>(async (cliente) => await ClienteSeleccionado(cliente));

            // Cargar clientes al iniciar
            Task.Run(async () => await CargarClientes());
        }

        private async Task CargarClientes()
        {
            var response = await _apiService.ListarClientesAsync();

            if (response.IsSuccess && response.Result != null)
            {
                Clientes.Clear();
                foreach (var cliente in response.Result)
                {
                    Clientes.Add(cliente);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
            }
        }

        public async Task BorrarClienteAsync(int id)
        {
            var response = await _apiService.BorrarClienteAsync(id);

            if (response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", response.Message, "OK");
                await CargarClientes();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
            }
        }

        private async Task ClienteSeleccionado(Cliente cliente)
        {
            if (cliente == null)
                return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmación", $"¿Deseas eliminar a {cliente.Nombre}?", "Sí", "No");
            if (confirm)
            {
                await BorrarClienteAsync(cliente.Id);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nombrePropiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }
    }
}
