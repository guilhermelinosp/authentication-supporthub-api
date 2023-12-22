using Microsoft.EntityFrameworkCore;
using SupportHub.Domain.Entities;
using SupportHub.Domain.Repositories;
using SupportHub.Infrastructure.Contexts;

namespace SupportHub.Infrastructure.Repositories;

public class CustomerRepository(ApplicationDbContext context) : ICustomerRepository
{
	public async Task<Customer?> FindCustomerByIdAsync(Guid customerid)
	{
		return await context.Customers!.AsNoTracking().SingleOrDefaultAsync(u => u.CustomerId == customerid);
	}

	public async Task<Customer?> FindCustomerByEmailAsync(string email)
	{
		return await context.Customers!.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
	}
	
	public async Task UpdateCustomerAsync(Customer customer)
	{
		context.Customers!.Update(customer);

		await SaveChangesAsync();
	}

	private async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}
}