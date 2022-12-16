using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using WeatherServiceProtos;
using static WeatherServiceProtos.WeatherService;

namespace WeatherService
{
    [Authorize]
    public class WeatherService : WeatherServiceBase
    {
        private const int __waitSec = 1;

        private DateTime _currentTime;
        private readonly List<int> _weathers;

        public WeatherService()
        {
            _weathers = new();
            _currentTime = DateTime.UtcNow;
            for (int i = 0; i < 5; i++)
            {
                _weathers.Add(Random.Shared.Next(0, 21));
            }

            TemperatureUpdater();
        }

        public override Task<GetWeatherResponse> GetWeather(GetWeatherRequest request, ServerCallContext context)
        {
            if (request.CityId < 0 || request.CityId >= _weathers.Count) 
            {
                return Task.FromResult(new GetWeatherResponse()
                {
                    ErrorCode = 404,
                    ErrorMessage = $"Город {request.CityId} не найден"
                });
            }

            return Task.FromResult(new GetWeatherResponse()
            {
                Time = Timestamp.FromDateTime(_currentTime),
                Temperature = _weathers[request.CityId]
            });
        }

        public override async Task GetWeatherStream(GetWeatherRequest request, IServerStreamWriter<GetWeatherResponse> responseStream, ServerCallContext context)
        {
            if (request.CityId < 0 || request.CityId >= _weathers.Count)
            {
                return;
            }

            await Task.Delay(1000, context.CancellationToken);

            while (!context.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000 * __waitSec, context.CancellationToken);

                GetWeatherResponse response = new()
                {
                    Time = Timestamp.FromDateTime(_currentTime),
                    Temperature = _weathers[request.CityId]
                };

                await responseStream.WriteAsync(response);
            }
        }

        private void TemperatureUpdater()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000 * __waitSec);

                    _currentTime = DateTime.UtcNow;
                    for (int i = 0; i < _weathers.Count; i++)
                    {
                        _weathers[i] += Random.Shared.Next(-1, 2);
                    }
                }
            });
        }
    }
}
