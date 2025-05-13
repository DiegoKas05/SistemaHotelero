using SistemaHotelero.Data;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;

namespace SistemaHotelero.DataAccess.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Categoria categoria)
        {
            var objDesdeDb = _db.Categoria.FirstOrDefault(c => c.IdCategoria == categoria.IdCategoria);
            if (objDesdeDb != null)
            {
                objDesdeDb.Descripcion = categoria.Descripcion;
                objDesdeDb.Estado = categoria.Estado;
            }
        }
    }
}
