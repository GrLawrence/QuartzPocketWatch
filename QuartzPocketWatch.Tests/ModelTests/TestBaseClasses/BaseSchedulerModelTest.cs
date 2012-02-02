using System;
using NUnit.Framework;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;
using QuartzPocketWatch.Plugin.Models;

namespace QuartzPocketWatch.Tests.ModelTests.TestBaseClasses
{
    [TestFixture]
    public class BaseSchedulerModelTest
    {
        protected const string JobName = "job1";
        protected const string GroupName = "group1";
        protected const string JobDescription = "Hello world job";
        protected const string TriggerName = "trigger1";
        private IScheduler _sched;
        protected ISchedulerFactory SchedulerFactory = new StdSchedulerFactory();

        protected SchedulerModel Context;

        [TestFixtureSetUp]
        public void CreateContext()
        {
            string schedName = GetType().ToString();
            DirectSchedulerFactory.Instance.CreateScheduler(schedName, schedName, new SimpleThreadPool(), new RAMJobStore());
            _sched = SchedulerFactory.GetScheduler(GetType().ToString());

            CustomizeContext();

            Context = new SchedulerModel(_sched);
        }

        public virtual void CustomizeContext()
        {
            
        }

        public void ScheduleJobWithDefaultTrigger<T>(string jobGroupName, string jobName) where T:IJob
        {
            JobBuilder builder = JobBuilder.Create<T>()
                .WithIdentity(jobName, jobGroupName)
                .WithDescription(jobGroupName + "." + jobName);

            IJobDetail job = builder.Build();

            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(TriggerName, jobGroupName)
                .StartAt(runTime)
                .WithSchedule(CalendarIntervalScheduleBuilder.Create().WithIntervalInMinutes(1))
                .Build();

            _sched.ScheduleJob(job, trigger);
        }

    }
}