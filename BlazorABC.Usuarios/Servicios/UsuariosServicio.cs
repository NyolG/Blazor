using CRUD.Compartir;
using System.Net.Http.Json;


namespace BlazorABC.Client.Servicios 
{ 
    public class UsuariosServicio : IUsuarioServicio
    {
        private readonly HttpClient _http;
        public UsuariosServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Clase1>> lista()
        {
            var resutl = await _http.GetFromJsonAsync<Responses<List<Clase1>>>("api/usuarios/lista");

            if (resutl!.Correcto)
            {
                return resutl.Valores!;
            }
            else
            {
                throw new Exception(resutl.Mensaje);
            }
        }

        public async Task<Clase1> buscar(string username)
        {
            var resutl = await _http.GetFromJsonAsync<Responses<Clase1>>($"api/usuarios/buscar/{username}");

            if (resutl!.Correcto)
            {
                return resutl.Valores!;
            }
            else
            {
                throw new Exception(resutl.Mensaje);
            }
        }

        public async Task<int> guardar(Clase1 usuario)
        {
            var resutl = await _http.PostAsJsonAsync("api/usuarios/guardar", usuario);
            var respondes = await resutl.Content.ReadFromJsonAsync<Responses<int>>();

            if (respondes!.Correcto)
            {
                return respondes.Valores!;
            }
            else
            {
                throw new Exception(respondes.Mensaje);
            }
        }

        public async Task<int> editar(Clase1 usuario)
        {

            var resutl = await _http.PutAsJsonAsync($"api/usuarios/editar/{usuario.Id}", usuario);
            var respondes = await resutl.Content.ReadFromJsonAsync<Responses<int>>();

            if (respondes!.Correcto)
            {
                return respondes.Valores!;
            }
            else
            {
                throw new Exception(respondes.Mensaje);
            }
        }

        public async Task<bool> borrar(int id)
        {
            var resutl = await _http.DeleteAsync($"api/usuarios/borrar/{id}");
            var respondes = await resutl.Content.ReadFromJsonAsync<Responses<int>>();

            if (respondes!.Correcto)
            {
                return respondes.Correcto!;
            }
            else
            {
                throw new Exception(respondes.Mensaje);
            }
        }
    }
}
