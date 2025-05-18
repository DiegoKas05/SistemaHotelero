using SistemaHotelero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface IRecepcionRepository : iRepository<Recepcion>
    {
        void Actualizar(Recepcion recepcion);
    }
}
