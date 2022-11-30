using PumpService.Services;
using System.ServiceModel;

namespace PumpService.Interfaces
{
    [ServiceContract]
    public interface IPumpServiceCallback
    {
        [OperationContract]
        void UpdateStatistics(StatisticsService statistics);
    }
}
