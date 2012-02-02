using System;
using NUnit.Framework;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;
using QuartzPocketWatch.Plugin.Models;

namespace QuartzPocketWatch.Tests.ModelTests.TestBaseClasses
{
    [TestFixture]
    public abstract class BaseSchedulerModelTest
    {
        private const string TriggerName = "trigger1";
        private IScheduler _sched;
        private readonly ISchedulerFactory _schedulerFactory = new StdSchedulerFactory();

        protected SchedulerModel Context;

        [TestFixtureSetUp]
        public void CreateContext()
        {
            string schedName = GetType().ToString();
            DirectSchedulerFactory.Instance.CreateScheduler(schedName, schedName, new SimpleThreadPool(), new RAMJobStore());
            _sched = _schedulerFactory.GetScheduler(GetType().ToString());

            CustomizeContext();
        }

        protected virtual void CustomizeContext()
        {
            
        }

        protected void ScheduleJobWithDefaultTrigger<T>(string jobGroupName, string jobName) where T:IJob
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

            Context = new SchedulerModel(_sched);

        }
    }
}