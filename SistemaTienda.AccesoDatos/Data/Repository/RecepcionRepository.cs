using SistemaHotelero.Data;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHotelero.DataAccess.Data.Repository
{
    public class RecepcionRepository : Repository<Recepcion>, IRecepcionRepository
    {
        private readonly ApplicationDbContext _db;

        public RecepcionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Recepcion recepcion)
        {
            var objDesdeDb = _db.Recepcion.FirstOrDefault(r => r.IdRecepcion == recepcion.IdRecepcion);
            if (objDesdeDb != null)
            {
                objDesdeDb.IdApplicationUser = recepcion.IdApplicationUser;
                objDesdeDb.IdHabitacion = recepcion.IdHabitacion;
                objDesdeDb.FechaSalida = recepcion.FechaSalida;
                objDesdeDb.Observacion = recepcion.Observacion;
                objDesdeDb.Adelanto = recepcion.Adelanto;
                objDesdeDb.PrecioInicial = recepcion.PrecioInicial;
                objDesdeDb.TotalPagado = recepcion.TotalPagado;
                objDesdeDb.CostoPenalidad = recepcion.CostoPenalidad;
                objDesdeDb.Estado = recepcion.Estado;
            }
        }
    }
}
