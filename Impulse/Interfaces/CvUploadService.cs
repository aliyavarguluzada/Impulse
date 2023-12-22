using Impulse.Core;
using Impulse.Core.Responses;
using Impulse.Data;
using Impulse.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Impulse.Interfaces
{
    public class CvUploadService : ICvUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public CvUploadService(IConfiguration configuration,
                                    ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<ServiceResult<CvUploadResponse>> CvUpload(IEnumerable<IFormFile> files)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (files is null)
                    transaction.RollbackAsync();

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(_configuration["Cv:Path"], fileName);
                        var count = _context.Cvs.Count();

                        var cv = new Cv();
                        int MainPageCount = int.Parse(_configuration["MainPage:Count"]);
                        bool main = count >= MainPageCount ? true : false;

                        cv.MainPage = main;

                        using (var filestream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(filestream);
                        }
                        cv.ImageName = fileName;

                        await _context.AddAsync(cv);
                    }
                }


                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                var response = new CvUploadResponse();


                return ServiceResult<CvUploadResponse>.OK(response);
            }
            catch (Exception)
            {
                transaction.RollbackAsync();
                return ServiceResult<CvUploadResponse>.ERROR("", "Uçdu ");

            }
        }
    }
}
