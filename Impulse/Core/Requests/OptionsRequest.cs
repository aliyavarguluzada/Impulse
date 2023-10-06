using Impulse.Core.RequestValidation;

namespace Impulse.Core.Requests
{
    public class OptionsRequest
    {
        [SafeString]
        string? JobType { get; set; }

        [SafeString]

        string? JobCategory { get; set; }

        [SafeString]

        string? WorkForm { get; set; }

        [SafeString]
        string? City { get; set; }

        [SafeString]

        string? Education { get; set; }

        [SafeString]

        string? Experience { get; set; }

    }
}
