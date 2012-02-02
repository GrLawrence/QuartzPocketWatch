using System;
using FluentAssertions;
using NUnit.Framework;
using Quartz;
using QuartzPocketWatch.Plugin.Models;

namespace QuartzPocketWatch.Tests.ModelTests.TriggerTests
{
    [TestFixture]
    public class TriggerModelTests
    {
        protected const string GroupName = "group1";
        private const string TriggerName = "trigger1";

        private TriggerModel _context;

        [TestFixtureSetUp]
        public void SetUp()
        {
            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(TriggerName, GroupName)
                .StartAt(runTime)
                .WithSchedule(CalendarIntervalScheduleBuilder.Create().WithIntervalInMinutes(1))
                .Build();

            _context = new TriggerModel(trigger);
        }

        [Test]
        public void TriggerGroupNameIsCorrect()
        {
            _context.TriggerGroupName.Should().BeEquivalentTo(GroupName);
        }

        [Test]
        public void TriggerNameIsCorrect()
        {
            _context.TriggerName.Should().BeEquivalentTo(TriggerName);
        }
        
    }
}