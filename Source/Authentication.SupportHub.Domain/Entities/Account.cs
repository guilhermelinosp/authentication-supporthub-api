using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.SupportHub.Domain.Entities;

[Table("TB_Account")]
public class Account
{
	[Key] public Guid AccountId { get; set; } = Guid.NewGuid();
	public required string Identity { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
	public string Phone { get; set; } = string.Empty;
	public bool Is2Fa { get; set; }
	public bool IsVerified { get; set; }
	public bool IsDisabled { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? DisabledAt { get; set; } = DateTime.UtcNow;
}