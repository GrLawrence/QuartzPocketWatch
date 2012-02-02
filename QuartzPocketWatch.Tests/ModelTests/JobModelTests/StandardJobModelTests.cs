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

using NUnit.Framework;
using QuartzPocketWatch.Tests.Fakes;
using FluentAssertions;
using QuartzPocketWatch.Tests.ModelTests.TestBaseClasses;

namespace QuartzPocketWatch.Tests.ModelTests.JobModelTests
{
    public class StandardJobModelTests : BaseJobModelTest<FakeJob>
    {
        [Test]
        public void JobDescriptionIsMappedCorrectly()
        {
            Context.Description.Should().BeEquivalentTo(JobDescription);
        }

        [Test]
        public void JobNameIsMappedCorrectly()
        {
            Context.JobName.Should().BeEquivalentTo(JobName);
        }

        [Test]
        public void JobGroupIsMappedCorrectly()
        {
            Context.JobGroup.Should().BeEquivalentTo(GroupName);
        }

        [Test]
        public void JobRequestsRecoveryIsMappedCorrectly()
        {
            Context.RequestsRecovery.Should().BeFalse();
        }

        [Test]
        public void StoreDurableIsMappedCorrectly()
        {
            Context.StoreDurable.Should().BeFalse();
        }

        [Test]
        public void JobTypeIsMappedCorrectly()
        {
            Context.JobType.Should().BeEquivalentTo(typeof(FakeJob).ToString());
        }

        [Test]
        public void ConcurrentExecutionIsAllowed()
        {
            Context.ConcurrentExecutionAllowed.Should().BeTrue();
        }

        [Test]
        public void PersistJobDataIsFalse()
        {
            Context.PersistJobDataAfterExecution.Should().BeFalse();
        }
    }
}