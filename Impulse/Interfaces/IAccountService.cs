using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Core.Responses;

namespace Impulse.Interfaces
{
    public interface IAccountService
    {
       Task<ServiceResult<LoginResponse>> Login(LoginRequest loginRequest, bool isAdmin = false,bool isCompany = true);
       Task<ServiceResult<RegisterResponse>> Register(RegisterRequest registerRequest);
    }
}
