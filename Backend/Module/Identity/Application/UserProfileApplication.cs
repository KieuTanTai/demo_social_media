using Identity.Interfaces.IApplication;
using Identity.Interfaces.IRepository;
using Identity.Models.Profile;
using Shared.Interfaces;

namespace Identity.Application
{
    public class UserProfileApplication(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository) : IUserProfileApplication
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        IUserProfileRepository _userProfileRepository = userProfileRepository;


        #region GET

        public async Task<UserProfileModel> GetProfileInfoAsync(string identityCode, CancellationToken cancellationToken = default)
        {
            var result = await _userProfileRepository.GetByIdAsync(identityCode, cancellationToken);
            return result ?? throw new Exception("User profile not found.");
        }

        #endregion

        #region POST

        public async Task<UserProfileModel> UpdateProfileInfoAsync(UserProfileModel userProfile, CancellationToken cancellationToken = default)
        {
            return userProfile;
        }

        public async Task<UserProfileModel> CreateBaseProfileInfoAsync(string identityCode, Guid accountId, CancellationToken cancellationToken = default)
        {
            try
            {
                var baseProfile = new UserProfileModel(identityCode, accountId);
                await _userProfileRepository.AddAsync(baseProfile, cancellationToken);
                return baseProfile;
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        
    }
}