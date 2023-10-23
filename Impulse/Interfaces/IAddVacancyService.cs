using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Core.Responses;

namespace Impulse.Interfaces
{
    public interface IAddVacancyService
    {
        Task<ServiceResult<AddVacancyResponse>> AddVacancy(AddVacancyRequest addRequest);
    }
}
