using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.SupportHub.Domain.Entities;

[Table("TB_Authentication_Company")]
public class AccountCompany
{
	[Key] public Guid CompanyId { get; set; } = Guid.NewGuid();
	public required string Cnpj { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
	public required string Phone { get; set; }
	public bool IsVerified { get; set; }
	public bool Is2Fa { get; set; }
	public bool IsDisabled { get; set; }
	public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}