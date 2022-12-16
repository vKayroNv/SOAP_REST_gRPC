using WeatherService.Storage.Models;

namespace WeatherService.Storage.Interfaces
{
    public interface IDatabaseContext
    {
        IList<Account> Accounts { get; }
    }
}
