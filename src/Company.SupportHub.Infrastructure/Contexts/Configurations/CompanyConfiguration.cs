using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.SupportHub.Infrastructure.Contexts.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Domain.Entities.AccountCompany>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.AccountCompany> builder)
	{
		builder.HasKey(company => company.CompanyId);
	}
}