namespace PumpService.Interfaces
{
    public interface IStatisticsService
    {
        int SuccessExecutions { get; set; }
        int ErrorExecutions { get; set; }
        int AllExecutions { get; set; }
    }
}
