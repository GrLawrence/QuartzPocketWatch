using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Quartz;
using QuartzPocketWatch.Plugin.Models;

namespace QuartzPocketWatch.Tests.ModelTests.TriggerTests
{
    [TestFixture]
    public class TriggerModelTests
    {
        private const int Priority = 5;
        private const string GroupName = "group1";
        private const string TriggerName = "trigger1";
        private const string Description = "description";
        private TriggerModel _context;
        private DateTimeOffset _runTime;
        private DateTimeOffset _now;
        private DateTimeOffset _endTime;
        private JobDataMap _jobDataMap;
        private IDictionary<string, object> _jobData;
        private ITrigger _trigger;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _now = DateTimeOffset.UtcNow;
            _runTime = DateBuilder.EvenMinuteDate(_now);
            _jobData = new Dictionary<string, object> {{"object1", new {PropertyValue = 3}}};
            _jobDataMap = new JobDataMap(_jobData);
            _endTime = DateTime.UtcNow.AddMonths(1);

            _trigger = TriggerBuilder.Create()
                .WithIdentity(TriggerName, GroupName)
                .WithDescription(Description)
                .StartAt(_runTime)
                .WithPriority(Priority)
                .WithSchedule(CalendarIntervalScheduleBuilder.Create().WithIntervalInMinutes(1))
                .UsingJobData(_jobDataMap)
                .EndAt(_endTime)
                .Build();

            _context = new TriggerModel(_trigger);
        }

        [Test]
        public void GroupNameIsCorrect()
        {
            _context.TriggerGroupName.Should().BeEquivalentTo(_trigger.Key.Group);
        }

        [Test]
        public void NameIsCorrect()
        {
            _context.TriggerName.Should().BeEquivalentTo(_trigger.Key.Name);
        }

        [Test]
        public void StartTimeIsCorrect()
        {
            _context.StartTime.Should().Be(_trigger.StartTimeUtc);
        }

        [Test]
        public void DescriptionIsCorrect()
        {
            _context.Description.Should().BeEquivalentTo(_trigger.Description);
        }

        [Test]
        public void EndTimeIsCorrect()
        {
            _context.EndTime.Should().Be(_trigger.EndTimeUtc);
        }

        [Test]
        public void FinalFireTimeIsCorrect()
        {
            _context.FinalFireTime.Should().Be(_trigger.FinalFireTimeUtc);
        }

        [Test]
        public void NextFireTimeIsCorrect()
        {
            _context.NextFireTime.Should().Be(_trigger.GetNextFireTimeUtc());
        }

        [Test]
        public void JobDataMapIsCorrect()
        {
            _context.JobDataMap.Should().NotBeEmpty();
            _context.JobDataMap.Should().Equal(_trigger.JobDataMap);
        }

        [Test]
        public void PriorityIsCorrect()
        {
            _context.Priority.Should().Be(_trigger.Priority);
        }
    }
}