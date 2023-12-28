using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.SupportHub.Infrastructure.Contexts.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Entities.Company>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.Company> builder)
	{
		builder.HasKey(company => company.CompanyId);
	}
}