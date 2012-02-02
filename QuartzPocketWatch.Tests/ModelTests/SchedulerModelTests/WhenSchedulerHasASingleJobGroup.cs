using FluentAssertions;
using NUnit.Framework;
using QuartzPocketWatch.Tests.Fakes;
using QuartzPocketWatch.Tests.ModelTests.TestBaseClasses;

namespace QuartzPocketWatch.Tests.ModelTests
{
    [TestFixture]
    public class WhenSchedulerHasASingleJobGroup : BaseSchedulerModelTest
    {
        public override void CustomizeContext()
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