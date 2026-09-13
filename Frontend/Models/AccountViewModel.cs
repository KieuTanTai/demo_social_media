using Shared.ModelHelper;

namespace Frontend.Models
{
    public class AccountViewModel(string email, bool isActive, List<string> roleNames, UserProfileViewModel userProfile, DateTime createdAt, DateTime updatedAt)
    {
        public string Email { get; private set; } = email;
        public bool IsActive { get; private set; } = isActive;
        public List<string> RoleNames { get; private set; } = roleNames;
        public UserProfileViewModel UserProfile { get; private set; } = userProfile;
        public DateTime CreatedAt { get; private set; } = createdAt;
        public DateTime UpdatedAt { get; private set; } = updatedAt;


        #region SET

        public void SetEmail(string email)
        {
            Email = ModelFieldGuard.Required(email, 255, nameof(email));
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void SetRoleName(List<string> roleName)
        {
            RoleNames = roleName;
        }

        public void SetUserProfile(UserProfileViewModel userProfile)
        {
            UserProfile = userProfile;
        }
        
        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
        
        public void SetUpdatedAt(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }

        #endregion
    }
}