using SistemaHotelero.Models;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Data;

namespace SistemaHotelero.DataAccess.Data.Repository
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Producto producto)
        {
            var objDesdeDb = _db.Producto.FirstOrDefault(p => p.IdProducto == producto.IdProducto);
            if (objDesdeDb != null)
            {
                objDesdeDb.Nombre = producto.Nombre;
                objDesdeDb.Detalle = producto.Detalle;
                objDesdeDb.Precio = producto.Precio;
                objDesdeDb.Cantidad = producto.Cantidad;
                objDesdeDb.Estado = producto.Estado;
            }
        }
    }
}
