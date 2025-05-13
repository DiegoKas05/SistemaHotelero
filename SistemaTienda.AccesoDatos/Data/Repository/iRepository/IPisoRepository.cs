using SistemaHotelero.Models;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface IPisoRepository : iRepository<Piso>
    {
        void Update(Piso piso);
    }
}
