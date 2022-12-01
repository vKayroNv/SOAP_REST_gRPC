using PumpService.Interfaces;
using PumpService.Services;
using System.ServiceModel;

namespace PumpService
{
    public class PumpService : IPumpService
    {
        private readonly IScriptService _scriptService;
        private readonly IStatisticsService _statisticsService;
        private readonly ISettingsService _settingsService;

        public PumpService()
        {
            _statisticsService = new StatisticsService();
            _settingsService = new SettingsService();
            _scriptService = new ScriptService(_statisticsService, _settingsService, Callback);
        }

        public void RunScript()
        {
            _scriptService.Run();
        }

        public void UpdateAndCompileScript(string scriptName)
        {
            _settingsService.ScriptName = scriptName;
            _scriptService.Compile();
        }

        private IPumpServiceCallback Callback
        {
            get
            {
                if (OperationContext.Current != null)
                {
                    return OperationContext.Current.GetCallbackChannel<IPumpServiceCallback>();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
