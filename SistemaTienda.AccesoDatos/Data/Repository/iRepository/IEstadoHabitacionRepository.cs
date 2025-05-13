using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;


namespace SistemaHotelero.AccesoDatos.Data.Repository.iRepository
{
    public interface IEstadoHabitacionRepository : iRepository<EstadoHabitacion>
    {
        void Update(EstadoHabitacion estadoHabitacion);
    }
}
