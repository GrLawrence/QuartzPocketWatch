using System;
using Common.Logging;
using Nancy.Hosting.Self;
using Nancy.ViewEngines.Razor;
using Quartz;
using Quartz.Spi;

namespace QuartzPocketWatch.Plugin
{
    public class WebInterfacePlugin : ISchedulerPlugin
    {
        private string _pluginName;
        private static IScheduler _sched;
        private NancyHost host;

        private static readonly ILog Log = LogManager.GetLogger("QuartzWeb");

        public static IScheduler Scheduler
        {
            get { return _sched; }
        }

        public void Initialize(string pluginName, IScheduler sched)
        {
            TinyIoC.TinyIoCContainer.Current.Register<RazorViewEngine>();
            host = new NancyHost(new Uri("http://localhost:1234"));

            host.Start();

            _sched = sched;
            _pluginName = pluginName;
        }

        public void Start()
        {
            //not yet
        }

        public void Shutdown()
        {
            if(host != null)
                host.Stop();
        }
    }
}