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
using Common.Logging;
using Quartz;
using QuartzPocketWatch.Plugin.Services.Dto;
using ServiceStack.ServiceHost;

namespace QuartzPocketWatch.Plugin.Services
{
    public class FireJobService : IService<FireJob>
    {
        private readonly IScheduler _scheduler;
        private static readonly ILog Log = LogManager.GetLogger(typeof(WebInterfacePlugin).ToString());

        public FireJobService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public object Execute(FireJob request)
        {
            bool success = false;

            try
            {
                _scheduler.TriggerJob(new JobKey(request.JobName, request.JobGroup));
                Log.Info("job " + request.JobName + " in group " + request.JobGroup + " fired successfully");
                success = true;
            }
            catch
            {
                Log.Error("job " + request.JobName + " in group " + request.JobGroup + " could not be fired");
            }

            return new BasicResponse {Message = string.Empty, Success = success};
        }
    }
}