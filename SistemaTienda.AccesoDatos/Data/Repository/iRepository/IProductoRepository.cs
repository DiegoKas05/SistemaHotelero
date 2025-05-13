using SistemaHotelero.Models;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface IProductoRepository : iRepository<Producto>
    {
        void Update(Producto producto);
    }
}
