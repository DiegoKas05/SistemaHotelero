using SistemaHotelero.AccesoDatos.Data.Repository.iRepository;
using SistemaHotelero.Data;
using SistemaHotelero.DataAccess.Data.Repository;
using SistemaHotelero.Models;

namespace SistemaHotelero.AccesoDatos.Repositorio
{
    public class EstadoHabitacionRepository : Repository<EstadoHabitacion>, IEstadoHabitacionRepository
    {
        private readonly ApplicationDbContext _db;

        public EstadoHabitacionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(EstadoHabitacion estado)
        {
            var objDesdeDb = _db.EstadoHabitacion.FirstOrDefault(e => e.IdEstadoHabitacion == estado.IdEstadoHabitacion);
            if (objDesdeDb != null)
            {
                objDesdeDb.Descripcion = estado.Descripcion;
                objDesdeDb.Estado = estado.Estado;
            }
        }
    }
}
