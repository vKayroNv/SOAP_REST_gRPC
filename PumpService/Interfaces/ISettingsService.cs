using System.Collections.Generic;

namespace PumpService.Interfaces
{
    public interface ISettingsService
    {
        Dictionary<string, string> Scripts { get; }
        string ScriptName { get; set; }
    }
}
