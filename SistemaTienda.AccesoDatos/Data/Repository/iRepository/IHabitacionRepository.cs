using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;

namespace SistemaHotelero.AccesoDatos.Repositorio.IRepositorio
{
    public interface IHabitacionRepository : iRepository<Habitacion>
    {
        void Update(Habitacion habitacion);
    }
}