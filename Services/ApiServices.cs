using ClienteMauiFrontend.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace ClienteMauiFrontend.Services
{
    //maneja las comunicaciones HTTP con la API
    public class ApiServices
    {
        private readonly HttpClient _httpClient;

        public ApiServices()
        {

            //Configurar la url de la API
            string baseUrl;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                // Emulador de Android -> 10.0.2.2
                baseUrl = "http://10.0.2.2:5291";
            }
            else
            {
                // Windows, iOS (Simulator), etc. -> localhost
                baseUrl = "http://localhost:5291";
            }

            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

            //Configurar la url de la API
            _httpClient.BaseAddress = new Uri("http://localhost:5291/api/");
        }

        //Crear un método para obtener los clientes
        public async Task<ApiResponse<Cliente>> CrearClienteAsync(Cliente cliente)
        {
            var json = JsonConvert.SerializeObject(cliente);//Se debe descargar el framework Newtonsoft
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respusta = await _httpClient.PostAsync("Cliente", contenido);
            var resultado = await respusta.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<Cliente>>(resultado);
        }

        //Listar los clientes
        public async Task<ApiResponse<List<Cliente>>> ListarClientesAsync()
        {
            var respuesta = await _httpClient.GetAsync("Cliente");
            var resultado = await respuesta.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<List<Cliente>>>(resultado);
        }

        //Obtener un cliente por su id
        public async Task<ApiResponse<Cliente>> ObtenerClientePorIdAsync(int id)
        {
            var respuesta = await _httpClient.GetAsync($"Cliente/{id}");
            var resultado = await respuesta.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<Cliente>>(resultado);
        }

        //Actualizar un cliente
        public async Task<ApiResponse<Cliente>> ActualizarClienteAsync(int id, Cliente cliente)
        {
            var json = JsonConvert.SerializeObject(cliente);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PutAsync($"Cliente/{id}", contenido);
            var resultado = await respuesta.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<Cliente>>(resultado);
        }

        //Eliminar un cliente
        public async Task<ApiResponse<string>> BorrarClienteAsync(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"Cliente/{id}");
            var resultado = await respuesta.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<string>>(resultado);
        }

        //Buscar un cliente por su nombre o email
        public async Task<ApiResponse<List<Cliente>>> BuscarClienteAsync(string filtro)
        {
            var respuesta = await _httpClient.GetAsync($"Cliente/buscar?filtro={filtro}");
            var resultado = await respuesta.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<List<Cliente>>>(resultado);
        }
    }
}
