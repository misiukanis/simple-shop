using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Shop.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        [Description("New")]
        New = 1,

        [Description("Paid")]
        Paid = 2,

        [Description("Shipped")]
        Shipped = 3,

        [Description("Cancelled")]
        Cancelled = 4
    }
}
