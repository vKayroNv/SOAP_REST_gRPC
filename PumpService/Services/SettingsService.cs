using PumpService.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PumpService.Services
{
    public class SettingsService : ISettingsService
    {
        private Dictionary<string, string> _scripts;

        public string ScriptName { get; set; }
        public Dictionary<string, string> Scripts => _scripts;

        public SettingsService()
        {
            Initialize();
        }

        private void Initialize()
        {
            _scripts = new Dictionary<string, string>();

            var path = Path.Combine(AppContext.BaseDirectory, "Scripts");

            var files = Directory.GetFiles(path);

            if (files.Length == 0)
            {
                return;
            }

            foreach (var file in files)
            {
                var scriptName = file.Split('\\').Last().Split('.').First();
                _scripts.Add(scriptName, file);
            }
        }
    }
}