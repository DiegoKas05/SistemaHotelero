using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHotelero.Models.ViewModels
{
    public class ReservaVM
    {
        public Recepcion Recepcion { get; set; }
        public Habitacion Habitacion { get; set; }
        public IEnumerable<SelectListItem> ListaClientes { get; set; }
    }
}
