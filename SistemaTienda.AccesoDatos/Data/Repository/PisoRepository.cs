using SistemaHotelero.Models;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Data;
using System.Linq;

namespace SistemaHotelero.DataAccess.Data.Repository
{
    public class PisoRepository : Repository<Piso>, IPisoRepository
    {
        private readonly ApplicationDbContext _db;

        public PisoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Piso piso)
        {
            var objDesdeDb = _db.Piso.FirstOrDefault(p => p.IdPiso == piso.IdPiso);
            if (objDesdeDb != null)
            {
                objDesdeDb.Descripcion = piso.Descripcion;
                objDesdeDb.Estado = piso.Estado;
                // No actualizamos FechaCreacion para mantener su valor original.
            }
        }
    }
}
