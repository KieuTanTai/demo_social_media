using Backend.Module.Premise.Infrastructures.Persistence.DBContext;
using Backend.Module.Premise.Interfaces.IRepository;
using Backend.Module.Premise.Models.Premise;

namespace Backend.Module.Premise.Infrastructures.Repository.PremiseRepository
{
    public class PremiseRepository : IPremiseRepository
    {
        private readonly PremiseDbContext _context;

        public PremiseRepository(PremiseDbContext context)
        {
            _context = context;
        }

        public async Task<PremiseModel?> GetPremiseByIdAsync(Guid premiseId)
        {
            return await _context.Premises
                .Include(p => p.PremiseBusinessTypes)
                    .ThenInclude(pbt => pbt.BusinessType)
                .FirstOrDefaultAsync(p => p.PremiseId == premiseId);
        }

        public async Task<IEnumerable<PremiseModel>> GetAllPremisesAsync()
        {
            return await _context.Premises
                .Include(p => p.PremiseBusinessTypes)
                    .ThenInclude(pbt => pbt.BusinessType)
                .ToListAsync();
        }

        public async Task AddPremiseAsync(PremiseModel premise)
        {
            await _context.Premises.AddAsync(premise);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePremiseAsync(PremiseModel premise)
        {
            _context.Premises.Update(premise);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePremiseAsync(Guid premiseId)
        {
            var premise = await GetPremiseByIdAsync(premiseId);
            if (premise != null)
            {
                _context.Premises.Remove(premise);
                await _context.SaveChangesAsync();
            }
        }
    }
}