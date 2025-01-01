using System;
using System.Collections.Generic;

namespace ECommerce.DataService.OrderModels.Order;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? OrderStatus { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? ShippingAddressId { get; set; }
}
