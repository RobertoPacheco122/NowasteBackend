namespace Nowaste.Communication.Responses.Establishment;

public class ResponseGetOperatingDayByIdJson
{
    public Guid Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
    public Guid EstablishmentId { get; set; }
}
