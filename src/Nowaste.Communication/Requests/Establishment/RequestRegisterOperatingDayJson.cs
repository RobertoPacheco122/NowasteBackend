namespace Nowaste.Communication.Requests.Establishment;

public class RequestRegisterOperatingDayJson {
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
    public Guid EstablishmentId { get; set; }
}
