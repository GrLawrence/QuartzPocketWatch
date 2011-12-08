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

using Nancy;
using Quartz;
using QuartzPocketWatch.Plugin.Models;

namespace QuartzPocketWatch.Plugin.Modules
{
    public class QuartzNancyModule : NancyModule
    {
        public QuartzNancyModule()
        {
            Get["/"] = x =>
            {
                return View["home.cshtml", new SchedulerModel(WebInterfacePlugin.Scheduler)];
            };

            Get["/FireJob/{JobGroup}/{JobName}"] = x => 
            {
                WebInterfacePlugin.Scheduler.TriggerJob(new JobKey(x.JobName, x.JobGroup));
                return Response.AsRedirect("/");
            };
        }
    }
}
