using Backend.Module.Premise.Models.Premise;

namespace Backend.Module.Premise.Interfaces.IRepository
{
    public interface IPremiseRepository
    {
        Task<PremiseModel?> GetPremiseByIdAsync(Guid premiseId);

        Task<IEnumerable<PremiseModel>> GetAllPremisesAsync();

        Task AddPremiseAsync(PremiseModel premise);

        Task UpdatePremiseAsync(PremiseModel premise);

        Task DeletePremiseAsync(Guid premiseId);
    }
}
 