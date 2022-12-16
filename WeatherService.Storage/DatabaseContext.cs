using WeatherService.Storage.Interfaces;
using WeatherService.Storage.Models;

namespace WeatherService.Storage
{
    public class DatabaseContext : IDatabaseContext
    {
        private List<Account> _accounts;

        public IList<Account> Accounts => _accounts;

        public DatabaseContext()
        {
            _accounts = new();
        }
    }
}