using Grpc.Core;
using Grpc.Net.Client;
using WeatherServiceProtos;
using static WeatherServiceProtos.AuthService;
using static WeatherServiceProtos.WeatherService;

namespace WeatherClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://localhost:5001");
            AuthServiceClient authClient = new(channel);

            Thread.Sleep(5000);

            var registerRequest = new RegisterRequest()
            {
                Username = "username",
                Password = "password",
            };
            var response = authClient.Register(registerRequest);

            if (response.ErrorCode != 0)
            {
                Console.WriteLine($"Error Code: {response.ErrorCode}");
                Console.WriteLine($"Message: {response.ErrorMessage}");
                return;
            }

            var headers = new Metadata
            {
                { "Authorization", $"Bearer {response.Token}" }
            };

            WeatherServiceClient weatherClient = new(channel);
            var request = new GetWeatherRequest()
            {
                CityId = 2
            };
            var streamResponse = weatherClient.GetWeatherStream(request, headers);
            
            while (true)
            {
                if (streamResponse.ResponseStream.MoveNext(default).Result)
                {
                    ProcessResponse(request, streamResponse.ResponseStream.Current);
                }
            }
        }

        static void ProcessResponse(GetWeatherRequest request, GetWeatherResponse response)
        {
            if (response.ErrorCode == 0)
            {
                Console.WriteLine($"Температура для города {request.CityId} равна {response.Temperature} ({response.Time.ToDateTime()} UTC)");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Код ошибки: {response.ErrorCode}\nСообщение: {response.ErrorMessage}");
                Console.ResetColor();
            }
        }
    }
}