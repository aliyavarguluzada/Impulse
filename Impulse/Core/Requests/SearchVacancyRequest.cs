using System.ComponentModel.DataAnnotations;

namespace Impulse.Core.Requests
{
    public class SearchVacancyRequest
    {
        public string? VacancyName { get; set; }
        public int? WorkFormId { get; set; }

        public int? JobTypeId { get; set; }

        public int? JobCategoryId { get; set; }
    }
}
