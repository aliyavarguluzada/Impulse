﻿using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Impulse.DTOs.Vacancies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Impulse.ViewModels.Pagination;
using Impulse.ViewModels.Search;
using System.Net;

namespace Impulse.Controllers
{
    public class VacancyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public VacancyController(ApplicationDbContext context,
                                                    IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {

            var vacancies = await _context
                .Vacancies
                .OrderByDescending(c => c.Id)
                .Include(c => c.City)
                .Include(c => c.Company)
                .Include(c => c.JobType)
                .Include(c => c.JobCategory)
                .Include(c => c.Education)
                .Include(c => c.Experience)
                .Select(c => new VacancyDto
                {
                    VacancyId = c.Id,
                    VacancyName = c.Name,
                    CityName = c.City.Name,
                    JobTypeName = c.JobType.Name,
                    JobCategoryName = c.JobType.Name,
                    EducationName = c.Education.Name,
                    ExperienceName = c.Experience.Name,
                    CompanyLogoImage = c.CompanyLogoImage,
                    CompanyName = c.CompanyName,
                    StartDate = c.StartDate,
                    ExpireDate = c.ExpireDate
                })
                .Skip((page - 1) * 10)
                .Take(10)
                .ToListAsync();


            var count = await _context.Vacancies.CountAsync();

            decimal pageCount = Math.Ceiling(count / (decimal)10);

            ViewBag.Pagination = new PaginationModel
            {
                Url = _configuration["VacancyPath:Path"],
                Count = pageCount,
                Page = (int)pageCount
            };

            var jobCategories = await _context
             .JobCategories
             .Select(c => new JobCategoryDto
             {
                 JobCategoryId = c.Id,
                 JobCategoryName = c.Name

             }).ToListAsync();

            var jobTypes = await _context
             .JobTypes
             .Select(c => new JobTypeDto
             {
                 JobTypeId = c.Id,
                 JobTypeName = c.Name

             }).ToListAsync();

            var workForms = await _context
              .WorkForms
              .Select(c => new WorkFormDto
              {
                  WorkFormId = c.Id,
                  WorkFormName = c.Name
              }).ToListAsync();

            ViewBag.JobTypes = jobTypes;
            ViewBag.JobCategories = jobCategories;
            ViewBag.WorkForms = workForms;

            return View(vacancies);
        }



        [HttpPost]
        public async Task<JsonResult> Index(SearchModel searchModel)
        {
            var newVacancies = await _context
                .Vacancies
                .Select(c => new VacancyDto
                {
                    VacancyId = c.Id,
                    VacancyName = c.Name,
                    CityName = c.City.Name,
                    JobTypeName = c.JobType.Name,
                    JobCategoryName = c.JobType.Name,
                    EducationName = c.Education.Name,
                    ExperienceName = c.Experience.Name,
                    CompanyLogoImage = c.CompanyLogoImage,
                    CompanyName = c.CompanyName,
                    JobCategoryId = c.JobType.Id,
                    JobTypeId = c.JobType.Id,
                    WorkFormId = c.WorkFormId,
                    StartDate = c.StartDate,
                    ExpireDate = c.ExpireDate
                })
                .Where(c => c.JobTypeId == searchModel.JobTypeId

                &&

                c.JobCategoryId == searchModel.JobCategoryId

                &&

                c.WorkFormId == searchModel.WorkFormId)

                .ToListAsync();

            return Json(new
            {
                status = HttpStatusCode.OK,
                data = newVacancies
            });
        }





        [HttpGet]
        public async Task<IActionResult> Chosen(int id)
        {
            var vacancy = await _context
                .Vacancies
                .Include(c => c.City)
                .Include(c => c.Company)
                .Include(c => c.JobType)
                .Include(c => c.JobCategory)
                .Include(c => c.Education)
                .Include(c => c.Experience)
                .Select(c => new VacancyDto
                {
                    VacancyId = c.Id,
                    VacancyName = c.Name,
                    CityName = c.City.Name,
                    JobTypeName = c.JobType.Name,
                    JobCategoryName = c.JobType.Name,
                    EducationName = c.Education.Name,
                    ExperienceName = c.Experience.Name,
                    CompanyLogoImage = c.CompanyLogoImage,
                    CompanyName = c.CompanyName,
                    Email = c.Email,
                    StartDate = c.StartDate,
                    ExpireDate = c.ExpireDate,
                    Description = c.Description
                })
                .Where(c => c.VacancyId == id)
                .ToListAsync();

            return View(vacancy);
        }
    }
}
