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
using Nancy.Hosting.Self;
using Nancy.ViewEngines.Razor;
using Quartz;
using Quartz.Spi;

namespace QuartzPocketWatch.Plugin
{
    public class WebInterfacePlugin : ISchedulerPlugin
    {
        private string _pluginName;
        private static IScheduler _sched;
        private NancyHost host;

        private static readonly ILog Log = LogManager.GetLogger("QuartzWeb");

        public static IScheduler Scheduler
        {
            get { return _sched; }
        }

        public void Initialize(string pluginName, IScheduler sched)
        {
            TinyIoC.TinyIoCContainer.Current.Register<RazorViewEngine>();
            host = new NancyHost(new Uri("http://localhost:1234"));

            host.Start();

            _sched = sched;
            _pluginName = pluginName;
        }

        public void Start()
        {
            //not yet
        }

        public void Shutdown()
        {
            if(host != null)
                host.Stop();
        }
    }
}