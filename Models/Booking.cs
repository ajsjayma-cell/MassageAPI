namespace MassageAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}