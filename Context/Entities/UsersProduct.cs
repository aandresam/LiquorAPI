using System;
using System.Collections.Generic;

namespace liquorApi.Context.Entities;

public partial class UsersProduct
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime RegDate { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
