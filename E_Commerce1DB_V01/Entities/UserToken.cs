
namespace E_Commerce1DB_V01.Entities;

public class UserToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public TokenType Type { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime? LastUsedAt { get; set; }
    public bool IsActive => !IsRevoked && (ExpiresAt == null || ExpiresAt > DateTime.UtcNow);
    
    //navigational
    public int UserId { get; set; }
    public User User { get; set; }
}