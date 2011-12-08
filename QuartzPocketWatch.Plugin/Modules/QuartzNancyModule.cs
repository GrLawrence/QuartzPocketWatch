using Nancy;
using Quartz;
using QuartzPocketWatch.Plugin.Models;

namespace QuartzPocketWatch.Plugin.Modules
{
    public class QuartzNancyModule : NancyModule
    {
        public QuartzNancyModule()
        {
            Get["/"] = x =>
            {
                return View["home.cshtml", new SchedulerModel(WebInterfacePlugin.Scheduler)];
            };

            Get["/FireJob/{JobGroup}/{JobName}"] = x => 
            {
                WebInterfacePlugin.Scheduler.TriggerJob(new JobKey(x.JobName, x.JobGroup));
                return Response.AsRedirect("/");
            };
        }
    }
}
