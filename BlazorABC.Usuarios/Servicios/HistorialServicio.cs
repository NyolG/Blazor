using BlazorABC.Client.Pages;
using CRUD.Compartir;
using System.Net.Http.Json;

namespace BlazorABC.Client.Servicios
{
    public class HistorialServicio : IHistorialServicio
    {
        private readonly HttpClient _http;
        public HistorialServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<HistorialClass>> lista(int idus)
        {
            var resutl = await _http.GetFromJsonAsync<Responses<List<HistorialClass>>>($"api/Historial/lista/{idus}");

            if (resutl!.Correcto)
            {
                return resutl.Valores!;
            }
            else
            {
                throw new Exception(resutl.Mensaje);
            }
        }


        public async Task<HistorialClass> buscar(int id)
        {

            var resutl = await _http.GetFromJsonAsync<Responses<HistorialClass>>($"api/Historial/buscar/{id}");

            if (resutl!.Correcto)
            {
                return resutl.Valores!;
            }
            else
            {
                throw new Exception(resutl.Mensaje);
            }
        }

        public async Task<int> guardar(HistorialClass his)
        {
            var resutl = await _http.PostAsJsonAsync("api/Historial/guardar", his);
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

        public async Task<int> editar(HistorialClass his)
        {
            var resutl = await _http.PutAsJsonAsync($"api/Historial/editar/{his.Id}", his);
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
            var resutl = await _http.DeleteAsync($"api/Historial/borrar/{id}");
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
