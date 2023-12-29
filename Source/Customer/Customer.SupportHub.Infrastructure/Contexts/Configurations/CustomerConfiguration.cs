using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.SupportHub.Infrastructure.Contexts.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Entities.Customer>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.Customer> builder)
	{
		builder.HasKey(customer => customer.CustomerId);
	}
}