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