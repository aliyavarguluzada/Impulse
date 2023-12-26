using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Core.Responses;
using Impulse.Data;
using Impulse.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ServiceResult<CvUploadResponse>> CvUpload(CvAddRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (request.Cvs is null)
                    transaction.RollbackAsync();

                foreach (var file in request.Cvs)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(_configuration["Cv:Path"], fileName);
                        var count = await _context.Cvs.CountAsync();

                        var cvs = new Cv();
                        int MainPageCount = int.Parse(_configuration["MainPage:Count"]);
                        bool main = count <= MainPageCount ? true : false;

                        cvs.MainPage = main;

                        using (var filestream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(filestream);
                        }
                        cvs.ImageName = fileName;
                        cvs.FilePath = filePath;

                        await _context.Cvs.AddAsync(cvs);
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
