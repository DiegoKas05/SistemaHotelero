using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaHotelero.Models.ViewModels
{
    public class SalidaVM
    {
        public int IdRecepcion { get; set; }
        public Habitacion Habitacion { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public decimal PrecioInicial { get; set; }
        public decimal Adelanto { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public List<Venta> Ventas { get; set; }
        public decimal TotalVentas { get; set; }
        public Recepcion Recepcion { get; set; }
    }
}