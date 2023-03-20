
using System.Text.Json.Serialization;

namespace liquorApi.Context.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public DateTime RegDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<UsersProduct> UsersProducts { get; } = new List<UsersProduct>();
}
