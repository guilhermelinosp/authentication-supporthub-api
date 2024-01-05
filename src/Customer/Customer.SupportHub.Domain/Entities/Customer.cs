using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.SupportHub.Domain.Entities;

[Table("TB_Authentication_Customer")]
public class Customer
{
	[Key] public Guid CustomerId { get; set; } = Guid.NewGuid();
	public required string Cnpj { get; set; }
	public required string Password { get; set; }
	public required string Email { get; set; }
	public bool IsVerified { get; set; }
	public string Phone { get; set; } = string.Empty;
	public bool Is2Fa { get; set; }
	public bool IsDisabled { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
	[ForeignKey("CompanyId")] public Guid CompanyId { get; set; }
}