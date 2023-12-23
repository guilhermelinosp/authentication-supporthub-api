using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Company.SupportHub.Domain.Entities;

namespace Company.SupportHub.Infrastructure.Contexts.EntityConfigurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Domain.Entities.Company>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.Company> builder)
	{
		builder
			.HasKey(c => c.CompanyId);
	}
}