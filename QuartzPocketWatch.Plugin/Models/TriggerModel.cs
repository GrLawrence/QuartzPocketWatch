using System;
using System.Collections.Generic;
using Quartz;

namespace QuartzPocketWatch.Plugin.Models
{
    public class TriggerModel
    {
        public TriggerModel(ITrigger trigger)
        {
            TriggerGroupName = trigger.Key.Group;
            TriggerName = trigger.Key.Name;
            StartTime = trigger.StartTimeUtc;
            EndTime = trigger.EndTimeUtc;
            FinalFireTime = trigger.FinalFireTimeUtc;
            Priority = trigger.Priority;
            Description = trigger.Description;
            JobDataMap = trigger.JobDataMap;
        }

        public string TriggerGroupName { get; private set; }
        public string TriggerName { get; private set; }
        public DateTimeOffset StartTime { get; private set; }
        public DateTimeOffset? EndTime { get; private set; }
        public DateTimeOffset? FinalFireTime { get; private set; }
        public int Priority { get; private set; }
        public string Description { get; private set; }
        public IDictionary<string, Object> JobDataMap { get; set; }
    }
}