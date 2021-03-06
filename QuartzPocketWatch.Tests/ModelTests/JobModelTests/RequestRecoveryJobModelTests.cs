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

using FluentAssertions;
using NUnit.Framework;
using Quartz;
using QuartzPocketWatch.Tests.Fakes;
using QuartzPocketWatch.Tests.ModelTests.TestBaseClasses;

namespace QuartzPocketWatch.Tests.ModelTests.JobModelTests
{
    public class RequestRecoveryJobModelTests : BaseJobModelTest<FakeJob>
    {
        protected override void CustomizeJob(JobBuilder builder)
        {
            builder.RequestRecovery();
        }

        [Test]
        public void JobRequestsRecovery()
        {
            Context.RequestsRecovery.Should().BeTrue();
        }
    }
}