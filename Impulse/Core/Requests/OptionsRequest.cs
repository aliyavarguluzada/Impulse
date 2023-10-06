using Impulse.Core.RequestValidation;

namespace Impulse.Core.Requests
{
    public class OptionsRequest
    {
        [SafeString]
        public string? JobType { get; set; }

        [SafeString]

        public string? JobCategory { get; set; }

        [SafeString]

        public string? WorkForm { get; set; }

        [SafeString]
        public string? City { get; set; }

        [SafeString]

        public string? Education { get; set; }

        [SafeString]

        public string? Experience { get; set; }

    }
}
