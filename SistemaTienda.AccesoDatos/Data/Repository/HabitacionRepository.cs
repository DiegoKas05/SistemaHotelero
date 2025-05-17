using SistemaHotelero.Models;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Data;
using System.Linq;
using SistemaHotelero.AccesoDatos.Repositorio.IRepositorio;

namespace SistemaHotelero.DataAccess.Data.Repository
{
    public class HabitacionRepository : Repository<Habitacion>, IHabitacionRepository
    {
        private readonly ApplicationDbContext _db;

        public HabitacionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Habitacion habitacion)
        {
            var objDesdeDb = _db.Habitacion.FirstOrDefault(h => h.IdHabitacion == habitacion.IdHabitacion);
            if (objDesdeDb != null)
            {
                objDesdeDb.Numero = habitacion.Numero;
                objDesdeDb.Detalle = habitacion.Detalle;
                objDesdeDb.Precio = habitacion.Precio;
                objDesdeDb.IdEstadoHabitacion = habitacion.IdEstadoHabitacion;
                objDesdeDb.IdPiso = habitacion.IdPiso;
                objDesdeDb.IdCategoria = habitacion.IdCategoria;
                objDesdeDb.Estado = habitacion.Estado;
            }
        }
    }
}
