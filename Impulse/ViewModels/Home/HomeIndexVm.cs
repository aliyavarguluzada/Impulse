using Impulse.DTOs.Cvs;
using Impulse.DTOs.SiteSettings;

namespace Impulse.ViewModels.Home
{
    public class HomeIndexVm
    {
        public List<SiteSettingsHomeIndexDto> siteSettings { get; set; }
        public List<CvsDto> Cvs { get; set; }
    }
}
