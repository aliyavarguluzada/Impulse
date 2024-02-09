namespace Impulse.Interfaces
{
    public interface ITelegramService
    {
        Task NotifyNewVacancyAsync(string vacancyInfo);
    }
}
