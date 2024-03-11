using reto_citikold_back.Models;
using reto_citikold_back.Models.dto;

namespace reto_citikold_back.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<ClientDTO>> ObtenerTodos();
        Task<ClientDTO> ObtenerPorId(int id);
        Task<int> Crear(ClientDTO cliente);
        Task<int> Actualizar(ClientDTO cliente);
        Task<int> Eliminar(int id);
    }
}
