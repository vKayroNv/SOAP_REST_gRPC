using WeatherService.Storage.Interfaces;
using WeatherService.Storage.Models;

namespace WeatherService.Storage
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDatabaseContext _context;

        public AccountRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public bool Create(Account account)
        {
            var result = _context.Accounts.FirstOrDefault(s => s.Username == account.Username);
            if (result != null)
            {
                return false;
            }

            _context.Accounts.Add(account);
            return true;
        }

        public Account GetByUsername(string username)
        {
            var result = _context.Accounts.FirstOrDefault(s => s.Username == username);
            if (result == null)
            {
                return null!;
            }

            return result;
        }
    }
}
