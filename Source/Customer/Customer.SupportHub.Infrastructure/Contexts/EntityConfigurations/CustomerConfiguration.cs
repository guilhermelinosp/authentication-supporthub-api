using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Customer.SupportHub.Infrastructure.Contexts.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Entities.Customer>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.Customer> builder)
	{
		builder
			.HasKey(c => c.CustomerId);
	}
}