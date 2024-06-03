using loginDemo.Models;

public class Payment
{
    public int? Id { get; set; }
    public int? ReservationId { get; set; }
    public string? UserId { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? Status { get; set; }

    public Reservation? Reservation { get; set; }
}
