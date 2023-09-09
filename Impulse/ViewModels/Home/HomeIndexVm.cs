using Impulse.DTOs.Cvs;
using Impulse.DTOs.SecondarySiteSettings;
using Impulse.DTOs.SiteSettings;
using Impulse.Models;

namespace Impulse.ViewModels.Home
{
    public class HomeIndexVm
    {
        public List<SiteSettingsHomeIndexDto> siteSettings { get; set; }
        public List<CvsDto> Cvs { get; set; }
        public List<SecondarySiteSettingsDto> SecondarySiteSettings { get; set; }
    }
}
