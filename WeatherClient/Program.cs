using Grpc.Net.Client;
using WeatherServiceProtos;
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
            WeatherServiceClient client = new(channel);

            Thread.Sleep(5000);

            var request = new GetWeatherRequest()
            {
                CityId = 2
            };
            ProcessResponse(request, client.GetWeather(request));
            Console.WriteLine();

            request.CityId = 10;
            ProcessResponse(request, client.GetWeather(request));
            Console.WriteLine();

            Console.ReadKey(true);

            request.CityId = 1;
            var streamResponse = client.GetWeatherStream(request);
            
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