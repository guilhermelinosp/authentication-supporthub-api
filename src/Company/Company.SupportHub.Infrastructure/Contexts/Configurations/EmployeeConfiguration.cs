using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.SupportHub.Infrastructure.Contexts.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Entities.Employee>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.Employee> builder)
	{
		builder.HasKey(employee => employee.EmployeeId);
	}
}