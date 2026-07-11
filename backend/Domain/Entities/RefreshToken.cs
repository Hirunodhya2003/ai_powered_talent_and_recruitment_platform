namespace Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expires { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime? Revoked { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}