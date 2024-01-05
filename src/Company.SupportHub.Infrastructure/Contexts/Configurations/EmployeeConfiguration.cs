using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.SupportHub.Infrastructure.Contexts.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Entities.AccountEmployee>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.AccountEmployee> builder)
	{
		builder.HasKey(employee => employee.EmployeeId);
	}
}