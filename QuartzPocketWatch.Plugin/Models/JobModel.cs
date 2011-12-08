using System;
using System.Collections.Generic;
using Quartz;

namespace QuartzPocketWatch.Plugin.Models
{
    public class JobModel
    {
        private readonly List<TriggerModel> _triggersForJob = new List<TriggerModel>();

        public JobModel(IScheduler scheduler, JobKey jobKey)
        {
            foreach (ITrigger trigger in scheduler.GetTriggersOfJob(jobKey))
            {
                _triggersForJob.Add(new TriggerModel(trigger));
            }

            MapValuesFromJobDetail(scheduler.GetJobDetail(jobKey));
        }

        public JobModel(IJobDetail jobDetail)
        {
            MapValuesFromJobDetail(jobDetail);
        }

        private void MapValuesFromJobDetail(IJobDetail jobDetail)
        {
            JobName = jobDetail.Key.Name;
            JobGroup = jobDetail.Key.Group;
            Description = jobDetail.Description;
            RequestsRecovery = jobDetail.RequestsRecovery;
            StoreDurable = jobDetail.Durable;
            JobType = jobDetail.JobType;

            System.Reflection.MemberInfo inf = jobDetail.JobType;
            object[] concurrentExecutionAttributes = inf.GetCustomAttributes(typeof(DisallowConcurrentExecutionAttribute), true);
            object[] persisJobDataAttributes = inf.GetCustomAttributes(typeof(PersistJobDataAfterExecutionAttribute), true);

            ConcurrentExecutionAllowed = concurrentExecutionAttributes.Length <= 0;
            PersistJobDataAfterExecution = persisJobDataAttributes.Length > 0;

            JobDataMap = jobDetail.JobDataMap;
        }

        public string JobName { get; private set; }
        public string Description { get; private set; }
        public string JobGroup { get; private set; }
        public bool ConcurrentExecutionAllowed { get; private set; }
        public bool RequestsRecovery { get; private set; }
        public bool StoreDurable { get; private set; }
        public Type JobType { get; private set; }
        public bool PersistJobDataAfterExecution { get; private set; }
        public IDictionary<string, object> JobDataMap { get; private set; }
        public IList<TriggerModel> TriggersForJob { get { return _triggersForJob.AsReadOnly(); } }
    }
}