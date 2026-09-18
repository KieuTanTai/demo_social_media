using System.Text.RegularExpressions;
using Identity.Interfaces.IApplication;
using Identity.Interfaces.IRepository;
using Identity.Models.Profile;
using Shared.Interfaces;

namespace Identity.Application
{
    public partial class UserProfileApplication(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository) : IUserProfileApplication
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly IUserProfileRepository _userProfileRepository = userProfileRepository;


        #region GET

        public async Task<UserProfileModel> GetProfileInfoAsync<T>(T id, CancellationToken cancellationToken = default)
        {
            var result = id switch
            {
                string identityCode => await _userProfileRepository.GetByIdAsync(identityCode, cancellationToken),
                Guid accountId => await _userProfileRepository.GetUserProfileAsync(accountId, cancellationToken),
                _ => throw new ArgumentException("Invalid id type")
            };
            return result ?? throw new Exception("fail to get profile");
        }

        #endregion

        #region POST

        public async Task<UserProfileModel> UpdateProfileInfoAsync(UserProfileModel userProfile, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(userProfile.UserProfileId) || userProfile.UserProfileId.Length != 12)
            {
                throw new ArgumentException("identity code is required and must be 12 digits");
            }
            if (!string.IsNullOrWhiteSpace(userProfile.UserProfilePhoneNumber))
            {
                var resultFormated = TryProcessPhoneNumber(userProfile.UserProfilePhoneNumber, out var phoneNumber);
                if (resultFormated)
                {
                    userProfile.SetUserProfilePhoneNumber(phoneNumber);
                }
                else
                {
                    throw new ArgumentException("Invalid phone number format.");
                }
            }

            try
            {
                var existingProfile = await _userProfileRepository.GetByIdAsync(userProfile.UserProfileId, cancellationToken);
                if (existingProfile == null)
                {
                    throw new ArgumentException("User profile not found.");
                }
                await _userProfileRepository.UpdateAsync(userProfile, cancellationToken);
                var affectRows = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return affectRows == 0 ? throw new ArgumentException("Failed to update user profile.") : userProfile;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Failed to update user profile.", ex);
            }
        }

        public async Task<UserProfileModel> CreateBaseProfileInfoAsync(string identityCode, Guid accountId, CancellationToken cancellationToken = default)
        {
            // method will raise an argument exception or argument null exception if failed to create a user profile. (check on constructor)
            var baseProfile = new UserProfileModel(identityCode, accountId);
            await _userProfileRepository.AddAsync(baseProfile, cancellationToken);

            var affectRows = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return affectRows == 0 ? throw new Exception("Failed to create user profile.") : baseProfile;
        }

        #endregion

        #region PRIVATE

        private static bool TryProcessPhoneNumber(string input, out string stringPhoneNumber)
        {
            stringPhoneNumber = "";

            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            // 1. Strip all non-numeric characters (spaces, dashes, plus sign, parentheses)
            var cleaned = MyRegex().Replace(input, "");

            // 2. Standardize international prefix +84 or 84 to local 0
            if (cleaned.StartsWith("84") && cleaned.Length == 11)
            {
                cleaned = string.Concat("0", cleaned.AsSpan(2));
            }

            // 3. Regex validation for Vietnamese mobile operators
            // Prefixes: 03, 05, 07, 08, 09 followed by exactly 8 digits (10 digits total)
            var pattern = @"^(03|05|07|08|09)\d{8}$";
            var match = Regex.Match(cleaned, pattern);
            var carrierCode = match.Groups[2].Value;
            var restOfNumber = cleaned[^8..];
            return true;
        }

        [GeneratedRegex(@"[^\d]")]
        private static partial Regex MyRegex();

        #endregion
    }
}