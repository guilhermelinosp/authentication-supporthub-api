using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportHub.Authentication.Domain.Entities;

namespace SupportHub.Auth.Infrastructure.Contexts.EntityConfigurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
	public void Configure(EntityTypeBuilder<Company> builder)
	{
		builder
			.HasKey(c => c.CompanyId);
	}
}