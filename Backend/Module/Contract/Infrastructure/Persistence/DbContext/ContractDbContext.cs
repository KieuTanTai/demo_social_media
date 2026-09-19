using Microsoft.EntityFrameworkCore;

namespace Contract.Infrastructure.Persistence.DbContext
{
    public class ContractDbContext(DbContextOptions<ContractDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        
    }
}