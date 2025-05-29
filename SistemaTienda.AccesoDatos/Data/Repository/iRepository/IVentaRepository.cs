using SistemaHotelero.Models;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface IVentaRepository : iRepository<Venta>
    {
        void Actualizar(Venta venta);
    }
}
