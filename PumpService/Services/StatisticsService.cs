using PumpService.Interfaces;

namespace PumpService.Services
{
    public class StatisticsService : IStatisticsService
    {
        public int SuccessExecutions { get; set; }
        public int ErrorExecutions { get; set; }
        public int AllExecutions { get; set; }
    }
}