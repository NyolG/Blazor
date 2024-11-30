using CRUD.Compartir;

namespace BlazorABC.Client.Servicios
{
    public interface IHistorialServicio
    {
        Task<List<HistorialClass>> lista(int idus);

        Task<HistorialClass> buscar(int id);

        Task<int> guardar(HistorialClass his);
        Task<int> editar(HistorialClass his);

        Task<bool> borrar(int id);
    }
}
