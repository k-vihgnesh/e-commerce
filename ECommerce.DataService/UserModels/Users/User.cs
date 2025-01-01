using System;
using System.Collections.Generic;

namespace ECommerce.DataService.UserModels.Users;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? Age { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
