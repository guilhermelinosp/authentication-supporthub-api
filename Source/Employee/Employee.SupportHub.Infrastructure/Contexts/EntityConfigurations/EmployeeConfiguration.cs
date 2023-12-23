using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.SupportHub.Infrastructure.Contexts.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Entities.Employee>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.Employee> builder)
	{
		builder
			.HasKey(c => c.EmployeeId);
	}
}