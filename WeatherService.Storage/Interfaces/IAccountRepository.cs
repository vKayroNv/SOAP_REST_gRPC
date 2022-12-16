using WeatherService.Storage.Models;

namespace WeatherService.Storage.Interfaces
{
    public interface IAccountRepository
    {
        bool Create(Account account);
        Account GetByUsername(string username);
    }
}
