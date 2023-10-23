using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Core.Responses;
using Impulse.Data;
using Impulse.Models;

namespace Impulse.Interfaces
{
    public class AddVacancyService : IAddVacancyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AddVacancyService(ApplicationDbContext context,
                                                        IHttpContextAccessor httpContextAccessor,
                                                                IConfiguration configuration)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public async Task<ServiceResult<AddVacancyResponse>> AddVacancy(AddVacancyRequest addRequest)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                if (String.IsNullOrEmpty(addRequest.VacancyName))
                    return ServiceResult<AddVacancyResponse>.ERROR("VacancyName", "The vacancy name is required.");

                if (String.IsNullOrEmpty(addRequest.Description))
                    return ServiceResult<AddVacancyResponse>.ERROR("Description", "Description is required.");

                if (String.IsNullOrEmpty(addRequest.Email))
                    return ServiceResult<AddVacancyResponse>.ERROR("Email", "Email is required.");


                string userName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "Name").Select(c => c.Value).FirstOrDefault();




                var requiredProperties = new[]
                {
                         addRequest.CityId,
                         addRequest.EducationId,
                         addRequest.ExperienceId,
                         addRequest.JobCategoryId,
                         addRequest.JobTypeId,
                         addRequest.WorkFormId
                    };

                if (requiredProperties.Any(value => value == null || value == 0))
                {
                    return ServiceResult<AddVacancyResponse>.ERROR(string.Empty, "Bütün bölmələr üzrə seçim edin.");
                }

                Vacancy vacancy = new()
                {
                    Name = addRequest.VacancyName,
                    Description = addRequest.Description,
                    Email = addRequest.Email,
                    StartDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddDays(30), // Hangfire

                    EducationId = addRequest.WorkFormId,
                    ExperienceId = addRequest.ExperienceId,
                    JobCategoryId = addRequest.JobCategoryId,
                    JobTypeId = addRequest.JobTypeId,
                    WorkFormId = addRequest.WorkFormId,
                    CityId = addRequest.CityId,
                    CompanyName = userName


                    /////

                };


                if (addRequest.Logo != null && addRequest.Logo.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(addRequest.Logo.FileName);
                    var filePath = Path.Combine(_configuration["LogoPath:Path"], fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await addRequest.Logo.CopyToAsync(fileStream);
                    }

                    vacancy.LogoFilePath = fileName;
                    vacancy.CompanyLogoImage = fileName;
                }

                await _context.AddAsync(vacancy);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var response = new AddVacancyResponse
                {
                    Email = addRequest.Email,
                    Description = addRequest.Description,
                    VacancyName = addRequest.VacancyName
                };


                return ServiceResult<AddVacancyResponse>.OK(response);

            }
            catch (Exception)
            {
                transaction.Rollback();
                return ServiceResult<AddVacancyResponse>.ERROR("", "Uçdu ");
            }
        }
    }
}
