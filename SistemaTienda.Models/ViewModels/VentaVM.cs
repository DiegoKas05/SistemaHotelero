using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaHotelero.Models.ViewModels
{
    public class VentaVM
    {
        public Habitacion Habitacion { get; set; }
        public ApplicationUser Cliente { get; set; }
        public Venta Venta { get; set; }
        public List<DetalleVenta> DetalleVenta { get; set; }
        public IEnumerable<SelectListItem> ListaProductos { get; set; }
        public int IdRecepcion { get; set; }
    }
}
