using CRUD.Compartir;

namespace BlazorABC.Client.Servicios
{
    public interface IUsuarioServicio
    {
        Task<List<Clase1>> lista();

        Task<Clase1> buscar(string username);

        Task<int> guardar(Clase1 usuario);
        Task<int> editar(Clase1 usuario);

        Task<bool> borrar(int id);
    }
}
