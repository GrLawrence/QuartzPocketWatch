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
using NUnit.Framework;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;
using QuartzPocketWatch.Plugin.Models;

namespace QuartzPocketWatch.Tests.ModelTests
{
    [TestFixture]
    public abstract class BaseJobModelTest<T> where T:IJob
    {
        protected const string JobName = "job1";
        protected const string GroupName = "group1";
        protected const string JobDescription = "Hello world job";
        protected const string TriggerName = "trigger1";
        protected ISchedulerFactory SchedulerFactory = new StdSchedulerFactory();
        protected IScheduler Sched;
        protected JobModel Context;

        public BaseJobModelTest()
        {
            string schedName = GetType().ToString();
            DirectSchedulerFactory.Instance.CreateScheduler(schedName, schedName, new SimpleThreadPool(), new RAMJobStore());
            Sched = SchedulerFactory.GetScheduler(GetType().ToString());
        }

        [TestFixtureSetUp]
        public void CreateContext()
        {
            JobBuilder builder = JobBuilder.Create<T>()
                .WithIdentity(JobName, GroupName)
                .WithDescription(JobDescription);

            CustomizeJob(builder);

            IJobDetail job = builder.Build();

            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(TriggerName, GroupName)
                .StartAt(runTime)
                .WithSchedule(CalendarIntervalScheduleBuilder.Create().WithIntervalInMinutes(1))
                .Build();

            Sched.ScheduleJob(job, trigger);

            Context = new JobModel(Sched, new JobKey(JobName, GroupName));
        }

        protected virtual void CustomizeJob(JobBuilder builder)
        {
        }
    }
}