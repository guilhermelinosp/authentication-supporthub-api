using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.SupportHub.Domain.Entities;

[Table("TB_Authentication_Account")]
public class Account
{
	[Key] public Guid AccountId { get; set; } = Guid.NewGuid();
	public required string Cnpj { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
	public required string Phone { get; set; } = string.Empty;
	public bool Is2FaEnabled { get; set; }
	public bool IsVerified { get; set; }
	public bool IsDisabled { get; set; }
	public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}