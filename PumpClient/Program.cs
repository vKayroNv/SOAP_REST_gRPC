using PumpClient.PumpServiceReference;
using System;
using System.ServiceModel;

namespace PumpClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InstanceContext instanceContext = new InstanceContext(new CallbackHandler());
            PumpServiceClient client = new PumpServiceClient(instanceContext);

            client.UpdateAndCompileScript("Sample1");
            client.RunScript();

            client.UpdateAndCompileScript("Sample2");
            client.RunScript();

            Console.WriteLine("Please, Enter to exit ...");
            Console.ReadKey(true);
            client.Close();
        }
    }
}
