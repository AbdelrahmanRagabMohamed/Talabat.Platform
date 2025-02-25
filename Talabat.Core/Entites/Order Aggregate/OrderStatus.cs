using System.Runtime.Serialization;

namespace Talabat.Core.Entites.Order_Aggregate;
public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending,

    [EnumMember(Value = "PaymentRecieved")]
    PaymentReceived,

    [EnumMember(Value = "PaymentFailed")]
    PaymentFailed

}
