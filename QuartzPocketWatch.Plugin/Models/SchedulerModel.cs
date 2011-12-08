using System.Collections.Generic;
using System.Collections.ObjectModel;
using Quartz;
using Quartz.Impl.Matchers;

namespace QuartzPocketWatch.Plugin.Models
{
    public class SchedulerModel
    {
        private readonly List<JobGroup> _jobGroups = new List<JobGroup>();

        public SchedulerModel(IScheduler scheduler)
        {
            //get triggers

            //get job groups and jobs
            foreach (string groupName in scheduler.GetJobGroupNames())
            {
                var jobs = scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupStartsWith(groupName));
                List<JobModel> jobModels = new List<JobModel>();

                foreach (JobKey jobKey in jobs)
                { 
                    jobModels.Add(new JobModel(scheduler, jobKey));
                }

                _jobGroups.Add(new JobGroup(groupName,jobModels));
            }
        }

        public ReadOnlyCollection<JobGroup> JobGroups
        {
            get { return _jobGroups.AsReadOnly(); }
        }
    }
}