using FluentAssertions;
using NUnit.Framework;
using QuartzPocketWatch.Tests.Fakes;
using QuartzPocketWatch.Tests.ModelTests.TestBaseClasses;

namespace QuartzPocketWatch.Tests.ModelTests.SchedulerModelTests
{
    [TestFixture]
    public class WhenSchedulerHasASingleJobGroup : BaseSchedulerModelTest
    {
        protected override void CustomizeContext()
        {
            ScheduleJobWithDefaultTrigger<FakeJob>("group1", "job1");
        }

        [Test]
        public void JobGroupListIsNotNull()
        {
            Context.Should().NotBeNull();
        }

        [Test]
        public void JobGroupListIsNotEmtpy()
        {
            Context.JobGroups.Should().NotBeEmpty();
        }

        [Test]
        public void JobGroupListContainsOneJobGroup()
        {
            Context.JobGroups.Should().HaveCount(1);
        }
    }
}