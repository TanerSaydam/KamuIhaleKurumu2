using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebApi.Entities;

public sealed class UserRole
{
    public UserRole()
    {
        Id = Ulid.NewUlid().ToString();
    }
    public string Id { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }

    [ForeignKey("Role")]
    public string RoleId { get; set; }
    public Role Role { get; set; }
}
