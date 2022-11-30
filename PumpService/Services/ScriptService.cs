using Microsoft.CSharp;
using PumpService.Interfaces;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PumpService.Services
{
    public class ScriptService : IScriptService
    {
        private CompilerResults results = null;
        private readonly IStatisticsService _statistics;
        private readonly ISettingsService _settings;
        private readonly IPumpServiceCallback _callback;

        public ScriptService(
            IStatisticsService statistics,
            ISettingsService settings,
            IPumpServiceCallback callback)
        {
            _statistics = statistics;
            _settings = settings;
            _callback = callback;
        }

        public bool Compile()
        {
            try
            {
                CompilerParameters compilerParameters = new CompilerParameters
                {
                    GenerateInMemory = true
                };
                compilerParameters.ReferencedAssemblies.Add("System.dll");
                compilerParameters.ReferencedAssemblies.Add("System.Core.dll");
                compilerParameters.ReferencedAssemblies.Add("System.Data.dll");
                compilerParameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
                compilerParameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

                FileStream fileStream = new FileStream(_settings.Scripts[_settings.ScriptName], FileMode.Open);
                byte[] buffer;
                try
                {
                    int length = (int)fileStream.Length;
                    buffer = new byte[length];
                    int count;
                    int sum = 0;
                    while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    {
                        sum += count;
                    }
                }
                finally
                {
                    fileStream.Close();
                }
                CSharpCodeProvider provider = new CSharpCodeProvider();
                results = provider.CompileAssemblyFromSource(compilerParameters, System.Text.Encoding.UTF8.GetString(buffer));
                if (results.Errors != null && results.Errors.Count != 0)
                {
                    string compileErrors = string.Empty;
                    for (int i = 0; i < results.Errors.Count; i++)
                    {
                        if (compileErrors != string.Empty)
                        {
                            compileErrors += "\n";
                        }
                        compileErrors += results.Errors[i];
                    }

                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Run()
        {
            if (results == null || (results != null && results.Errors != null && results.Errors.Count > 0))
            {
                if (Compile() == false)
                {
                    return;
                }
            }

            Type t = results.CompiledAssembly.GetType($"{_settings.ScriptName}.Program");
            if (t == null)
            {
                return;
            }
            MethodInfo entryPointMethod = t.GetMethods().First();
            if (entryPointMethod == null)
            {
                return;
            }

            Task.Run(() =>
            {
                if ((bool)entryPointMethod.Invoke(Activator.CreateInstance(t), null))
                {
                    _statistics.SuccessExecutions++;
                }
                else
                {
                    _statistics.ErrorExecutions++;
                }
                _statistics.AllExecutions++;
                _callback.UpdateStatistics((StatisticsService)_statistics);
            });
        }
    }
}