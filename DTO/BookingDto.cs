namespace MassageAPI.DTO
{
    public class BookingDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}