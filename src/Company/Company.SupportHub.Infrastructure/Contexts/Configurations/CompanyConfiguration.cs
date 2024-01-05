using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.SupportHub.Infrastructure.Contexts.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Domain.Entities.Company>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.Company> builder)
	{
		builder.HasKey(company => company.CompanyId);
	}
}