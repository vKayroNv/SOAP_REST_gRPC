using PumpClient.PumpServiceReference;
using System;

namespace PumpClient
{
    internal class CallbackHandler : IPumpServiceCallback
    {
        public void UpdateStatistics(StatisticsService statistics)
        {
            Console.Clear();
            Console.WriteLine("Обновление по статистике выполнения скрипта");
            Console.WriteLine($"Всего     выполнений: {statistics.AllExecutions}");
            Console.WriteLine($"Успешных  выполнений: {statistics.SuccessExecutions}");
            Console.WriteLine($"Ошибочных выполнений: {statistics.ErrorExecutions}");
        }
    }
}
