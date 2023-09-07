﻿namespace Impulse.Models
{
    public class CompanySocial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public Company Company { get; set; }
    }
}
