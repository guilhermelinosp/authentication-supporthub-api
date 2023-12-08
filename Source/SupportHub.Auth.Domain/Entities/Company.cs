using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportHub.Auth.Domain.Entities;

[Table("TB_Authentication_Company")]
public class Company
{
    [Key] public Guid CompanyId { get; set; } = Guid.NewGuid();
    public required string Cnpj { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string Phone { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public bool Is2Fa { get; set; } = false;
    public string Code { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}