using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.SupportHub.Domain.Entities;

[Table("TB_Authentication_Employee")]
public class Employee
{
	[Key] public Guid EmployeeId { get; set; } = Guid.NewGuid();
	public required string Email { get; set; }
	public required string Password { get; set; }
	public bool IsDisabled { get; set; } = false;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
	[ForeignKey("CompanyId")] public Guid CompanyId { get; set; }
}