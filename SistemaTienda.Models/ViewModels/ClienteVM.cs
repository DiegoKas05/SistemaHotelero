using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHotelero.Models;
using System.Collections.Generic;

namespace SistemaHotelero.Models.ViewModels
{
    public class ClienteVM
    {
        public IEnumerable<SelectListItem> ListaTiposDocumento { get; set; }
    }
}