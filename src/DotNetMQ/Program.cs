using System.ServiceProcess;
using Topshelf;
using Topshelf.Common.Logging;
using Topshelf.Hosts;

namespace DotNetMQ
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            HostFactory.New(x =>
            {
                x.UseCommonLogging();
            });

            HostFactory.Run(c =>
            {
                c.Service<DotNetMqService>(s =>
                {
                    s.ConstructUsing(f => new DotNetMqService());
                    s.WhenStarted((x, h) => x.Start(h));
                    s.WhenStopped((x, h) => x.Stop(h));
                });
                c.SetDescription("A Message Broker for integration of applications.");
                c.SetServiceName("DotNetMqService");
            });
        }
    }
}
