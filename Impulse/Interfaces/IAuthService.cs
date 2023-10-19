using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Core.Responses;

namespace Impulse.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<CookieAuthResponse>> CookieAuth(CookieAuthRequest cookieAuthRequest);
    }
}
