using Grpc.Core;
using WeatherService.Storage.Interfaces;
using WeatherService.Storage.Models;
using WeatherService.Utils;
using WeatherServiceProtos;
using static WeatherServiceProtos.AuthService;

namespace WeatherService
{
    public class AuthService : AuthServiceBase
    {
        private readonly IAccountRepository _repository;

        public AuthService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public override Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            (string salt, string hash) = PasswordUtils.CreatePasswordHash(request.Password);
            var id = Guid.NewGuid();

            Account account = new()
            {
                AccountId = id,
                Username = request.Username,
                PasswordSalt = salt,
                PasswordHash = hash,
                Token = TokenUtils.GenerateJwtToken(id.ToString())
            };

            var result = _repository.Create(account);

            RegisterResponse response;

            if (!result)
            {
                response = new()
                {
                    ErrorCode = 1,
                    ErrorMessage = "Пользователь уже существует"
                };
            }
            else
            {
                response = new()
                {
                    Token = account.Token
                };
            }

            return Task.FromResult(response);
        }

        public override Task<AuthResponse> Authenticate(AuthRequest request, ServerCallContext context)
        {
            AuthResponse response;
            var account = _repository.GetByUsername(request.Username);

            if (account == null)
            {
                response = new()
                {
                    ErrorCode = 1,
                    ErrorMessage = "Пользователь не найден"
                };
                return Task.FromResult(response);
            }

            var result = PasswordUtils.VerifyPassword(request.Password, account.PasswordSalt, account.PasswordHash);

            if (!result)
            {
                response = new()
                {
                    ErrorCode = 2,
                    ErrorMessage = "Неверный пароль"
                };
            }
            else
            {
                response = new()
                {
                    Token = account.Token
                };
            }

            return Task.FromResult(response);
        }
    }
}
