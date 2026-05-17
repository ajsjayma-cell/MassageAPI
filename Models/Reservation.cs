namespace MassageAPI.Models;

public class Reservation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ServiceId { get; set; }

    public int TherapistId { get; set; }

    public DateTime ReservationDate { get; set; }

    public string ReservationTime { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}