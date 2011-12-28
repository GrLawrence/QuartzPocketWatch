#region License
/* 
 * All content copyright Grant Pexsa, unless otherwise indicated. All rights reserved. 
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not 
 * use this file except in compliance with the License. You may obtain a copy 
 * of the License at 
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0 
 *   
 * Unless required by applicable law or agreed to in writing, software 
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations 
 * under the License.
 * 
 */
#endregion

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