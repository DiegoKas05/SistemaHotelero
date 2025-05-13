using SistemaHotelero.Models;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface ICategoriaRepository : iRepository<Categoria>
    {
        void Update(Categoria categoria);
    }
}
