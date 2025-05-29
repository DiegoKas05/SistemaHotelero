using SistemaHotelero.Models;
using System.Linq.Expressions;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface IProductoRepository : iRepository<Producto>
    {
        void Update(Producto producto);
        Producto GetFirstOrDefault(Expression<Func<Producto, bool>> filter);
    }
}
