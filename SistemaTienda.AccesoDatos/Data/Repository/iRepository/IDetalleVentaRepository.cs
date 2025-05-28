using SistemaHotelero.Models;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface IDetalleVentaRepository : iRepository<DetalleVenta>
    {
        void Actualizar(DetalleVenta detalleVenta);
    }
}
