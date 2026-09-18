using Identity.Interfaces;
using Identity.Interfaces.IApplication;
using Identity.Presentation.Record.Account;
using Microsoft.AspNetCore.Mvc;
using Shared.Persistence.Record.Auth;

namespace Identity.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(
        IAccountApplication accountApplication,
        IUserProfileApplication userProfileApplication,
        IAccountHelper helper,
        IApiHelper apiHelper) : ControllerBase
    {
        private readonly IAccountApplication _accountApplication = accountApplication;

        private readonly IApiHelper _apiHelper = apiHelper;

        private readonly IAccountHelper _helper = helper;

        private readonly IUserProfileApplication _userProfileApplication = userProfileApplication;

        #region POST

        [RequireHttps]
        [HttpPost("register")]
        public async Task<ActionResult<RecordAuthResponse>> RegisterAsync([FromBody] RecordAuthRequest requestDto, CancellationToken cancellationToken = default)
        {
            var (isValid, errorMessage) = _helper.ValidateEmailAndPassword(requestDto.Email, requestDto.Password);
            if (!isValid)
            {
                return BadRequest(errorMessage);
            }

            try
            {
                var result = await _accountApplication.RegisterAsync(requestDto.Email, requestDto.Password, cancellationToken);
                var response = _apiHelper.MappingAuthResult(result);
                return Ok(response);
            }
            catch (OperationCanceledException ex)
            {
                return BadRequest($"request canceled! \n {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred \n {ex.Message}");
            }
        }

        [RequireHttps]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] RecordAuthRequest requestDto, CancellationToken cancellationToken = default)
        {
            var (isValid, errorMessage) = _helper.ValidateEmailAndPassword(requestDto.Email, requestDto.Password);
            if (!isValid)
            {
                return BadRequest(errorMessage);
            }
            try
            {
                var result = await _accountApplication.LoginAsync(requestDto.Email, requestDto.Password, cancellationToken);
                var response = _apiHelper.MappingAuthResult(result);
                if (result.UserProfile == null)
                {
                    return Ok(response);
                }
                var profileResponse = _apiHelper.MappingProfileResult(result.UserProfile);
                return Ok(new { response, profileResponse }); //return both account response and profile response (applications get all of this in one req)
            }
            catch (OperationCanceledException ex)
            {
                return BadRequest($"request canceled! \n {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred \n {ex.Message}");
            }
        }

        [RequireHttps]
        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] RecordUpdateAccountPasswordRequest requestDto, CancellationToken cancellationToken = default)
        {
            var (isValid, errorMessage) = _helper.ValidateEmailAndPassword(requestDto.Email, requestDto.OldPassword);
            if (!isValid)
            {
                return BadRequest(errorMessage);
            }
            if (string.IsNullOrWhiteSpace(requestDto.NewPassword))
            {
                return BadRequest("New password is required.");
            }
            if (string.CompareOrdinal(requestDto.OldPassword, requestDto.NewPassword) == 0)
            {
                return BadRequest("New password must be different from old password.");
            }

            try
            {
                var result = await _accountApplication.ChangePasswordAsync(requestDto.Email, requestDto.OldPassword, requestDto.NewPassword, cancellationToken);
                if (result == 0)
                {
                    return BadRequest("Could not change password.");
                }
                return Ok(result);
            }
            catch (OperationCanceledException ex)
            {
                return BadRequest($"request canceled! \n {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred \n {ex.Message}");
            }
        }

        #endregion

        #region DELETE

        [RequireHttps]
        [HttpDelete("delete")]
        public async Task<IActionResult> InactiveAccountAsync([FromBody] RecordInactiveAccountRequest requestDto, CancellationToken cancellationToken = default)
        {
            var (isValid, errorMessage) = _helper.ValidateEmailAndPassword(requestDto.Email, requestDto.Password);
            if (!isValid)
            {
                return BadRequest(errorMessage);
            }
            if (string.CompareOrdinal(requestDto.Password, requestDto.ConfirmPassword) != 0)
            {
                return BadRequest("Password and confirm password do not match.");
            }

            try
            {
                var result = await _accountApplication.InactiveAccountAsync(requestDto.Email, requestDto.Password, cancellationToken);
                if (result == 0)
                {
                    return BadRequest("Could not inactive account.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [RequireHttps]
        [HttpDelete("admin/delete/{accountId}")]
        public async Task<IActionResult> InactiveAccountByAdminAsync([FromRoute] string accountId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                return BadRequest("Account id is required.");
            }

            try
            {
                var result = await _accountApplication.InactiveAccountByAdminAsync(new Guid(accountId), cancellationToken);
                if (result == 0)
                {
                    return BadRequest("Could not inactive account.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}