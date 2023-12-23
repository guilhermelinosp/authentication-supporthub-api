using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.SupportHub.Domain.Entities;

[Table("TB_Authentication_Customer_Employee")]
public sealed class Employee
{
	[Key] public Guid EmployeeId { get; set; } = Guid.NewGuid();
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public bool IsDisabled { get; set; } = false;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
	[ForeignKey("CustomerId")] public Guid CustomerId { get; set; }
}