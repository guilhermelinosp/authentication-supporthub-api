using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportHub.Auth.Domain.Entities;

[Table("TB_Authentication_Client")]
public class Customer
{
    [Key] public Guid CustomerId { get; set; } = Guid.NewGuid();

    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Phone { get; set; }
    public bool IsVerified { get; set; } = false;
    public bool IsDisabled { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DisabledAt { get; set; } = null;

    [ForeignKey("CompanyId")] public Guid CompanyId { get; set; }
}