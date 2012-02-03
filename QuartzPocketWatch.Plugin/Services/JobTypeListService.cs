using QuartzPocketWatch.Plugin.Services.Dto;
using ServiceStack.ServiceHost;

namespace QuartzPocketWatch.Plugin.Services
{
    public class JobTypeListService : IService<JobTypeList>
    {
        public object Execute(JobTypeList request)
        {
            return new JobTypeListResponse(WebInterfacePlugin.AvailableJobTypes);
        }
    }
}