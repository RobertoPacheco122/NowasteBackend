namespace Nowaste.Domain.Entities;

public class OperatingDayEntity : BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }

    public Guid EstablishmentId { get; set; }
    public virtual EstablishmentEntity Establishment { get; set; } = null!;
}
