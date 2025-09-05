namespace Nowaste.Domain.Enums;

public enum EOrderStatus {
    Pending = 1,
    Confirmed = 2,
    Preparing = 3,
    OutForDelivery = 4,
    Delivered = 5,
    WaitingWithdrawal = 6,
    Canceled = 7,
}