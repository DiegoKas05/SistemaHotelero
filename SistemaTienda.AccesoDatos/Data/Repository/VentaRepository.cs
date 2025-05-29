using SistemaHotelero.Data;
using SistemaHotelero.Models;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;

namespace SistemaHotelero.DataAccess.Data.Repository
{
    public class VentaRepository : Repository<Venta>, IVentaRepository
    {
        private readonly ApplicationDbContext _context;

        public VentaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Actualizar(Venta venta)
        {
            var obj = _context.Venta.FirstOrDefault(v => v.IdVenta == venta.IdVenta);
            if (obj != null)
            {
                obj.Total = venta.Total;
                obj.Estado = venta.Estado;
                // Puedes agregar más campos si los necesitas actualizar
            }
        }
    }
}
