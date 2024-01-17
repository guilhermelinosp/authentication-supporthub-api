using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.SupportHub.Domain.Entities;

[Table("TB_Company")]
public class Company
{
	public Guid CompanyId { get; set; } = Guid.NewGuid();
	public required string Name { get; set; }
	public required string Cnpj { get; set; }
	public int Licences { get; set; } = 3;
	public bool IsDisabled { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
	[ForeignKey("AccountId")] public Guid AccountId { get; set; }
}