using Identity.Models.Account;
using Identity.Models.Profile;
using Identity.Presentation.Record.Profile;
using Shared.Persistence.Record.Auth;

namespace Identity.Interfaces
{
    public interface IApiHelper
    {
        public RecordAuthResponse MappingAuthResult(AccountModel result);
        public RecordProfileResponse MappingProfileResult(UserProfileModel model);
    }
}