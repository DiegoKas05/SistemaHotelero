namespace SistemaHotelero.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
