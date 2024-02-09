namespace Impulse.Interfaces
{
    public class TelegramService : ITelegramService
    {
        private readonly HttpClient _httpClient;
        private readonly string _botToken;
        private readonly string _chatId;
        private readonly IConfiguration _configuration;
        public TelegramService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _botToken = _configuration["TelegramBot:Token"];
            _chatId = _configuration["TelegramBot:ChatId"];
        }

        public async Task NotifyNewVacancyAsync(string vacancyInfo)
        {
            string apiUrl = $"https://api.telegram.org/bot{_botToken}/sendMessage";
            string message = $"New Vacancy Created:\n{vacancyInfo}";

            var parameters = new
            {
                chat_id = _chatId,
                text = message
            };

            await _httpClient.GetStringAsync(apiUrl + $"?chat_id={_chatId}&text={message}");
        }
    }
}
