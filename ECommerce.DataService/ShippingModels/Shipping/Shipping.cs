using System;
using System.Collections.Generic;

namespace ECommerce.DataService.ShippingModels.Shipping;

public partial class Shipping
{
    public int ShippingId { get; set; }

    public int? OrderId { get; set; }

    public DateTime? ShippingDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? ShippingStatus { get; set; }

    public string? TrackingNumber { get; set; }

    public int? ShippingAddressId { get; set; }
}
