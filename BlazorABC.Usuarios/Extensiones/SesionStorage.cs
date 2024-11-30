using Blazored.SessionStorage;
using System.Text.Json;
using CRUD.Compartir;

namespace BlazorABC.Client.Extensiones
{
    public static class SesionStorage
    {

        public static async Task GuardarStorage<T>(this ISessionStorageService sessionStorageService,
            string key, T item
            ) where T : class
        {
            var itemjson = JsonSerializer.Serialize(item);
            await sessionStorageService.SetItemAsStringAsync(key, itemjson);
        }

        public static async Task<T?> ObtenerStorage<T>(
          this ISessionStorageService sessionStorageService,
          string key 
          ) where T : class
        {
           var itemjson= await sessionStorageService.GetItemAsStringAsync(key);

            if( itemjson != null )
            {
                var item= JsonSerializer.Deserialize<T>(itemjson);
                return item;
            }
            else
            {
                return null;
            }
                  
        }


    }
}
