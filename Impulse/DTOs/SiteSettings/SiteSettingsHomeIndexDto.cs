﻿namespace Impulse.DTOs.SiteSettings
{
    public record SiteSettingsHomeIndexDto
    {
        public string MainImage { get; set; }
        public string SecondaryImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
