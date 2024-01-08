using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.SupportHub.Domain.Entities;

[Table("TB_Authentication_Employee")]
public sealed class Employee
{
	[Key] public Guid EmployeeId { get; set; } = Guid.NewGuid();
	public string? Cpf { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public bool IsDisabled { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
	[ForeignKey("AccountId")] public Guid ResponsibleId { get; set; }
}