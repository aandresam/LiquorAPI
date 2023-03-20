
using System.Text.Json.Serialization;

namespace liquorApi.Context.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateTime RegDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<UsersProduct> UsersProducts { get; } = new List<UsersProduct>();
}
