using SistemaHotelero.Data;
using SistemaHotelero.Models;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;

namespace SistemaHotelero.DataAccess.Data.Repository
{
    public class DetalleVentaRepository : Repository<DetalleVenta>, IDetalleVentaRepository
    {
        private readonly ApplicationDbContext _context;

        public DetalleVentaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Actualizar(DetalleVenta detalleVenta)
        {
            var obj = _context.DetalleVenta.FirstOrDefault(d => d.IdDetalleVenta == detalleVenta.IdDetalleVenta);
            if (obj != null)
            {
                obj.Cantidad = detalleVenta.Cantidad;
                obj.SubTotal = detalleVenta.SubTotal;
                // Puedes agregar más campos si los necesitas actualizar
            }
        }
    }
}
