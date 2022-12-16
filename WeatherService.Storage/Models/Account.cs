namespace WeatherService.Storage.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
