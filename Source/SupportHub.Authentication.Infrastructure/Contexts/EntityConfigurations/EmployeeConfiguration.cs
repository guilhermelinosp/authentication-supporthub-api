using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportHub.Authentication.Domain.Entities;

namespace SupportHub.Auth.Infrastructure.Contexts.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
	public void Configure(EntityTypeBuilder<Employee> builder)
	{
		builder
			.HasKey(c => c.EmployeeId);
	}
}