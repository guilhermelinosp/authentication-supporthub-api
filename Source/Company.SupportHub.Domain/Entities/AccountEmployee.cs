using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.SupportHub.Domain.Entities;

[Table("TB_Authentication_Company_Employee")]
public sealed class AccountEmployee
{
	[Key] public Guid EmployeeId { get; set; } = Guid.NewGuid();
	public string? Cpf { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public int? Level { get; set; }
	public bool IsDisabled { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
	[ForeignKey("CompanyId")] public Guid CompanyId { get; set; }
}