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
using Quartz.Spi;
using QuartzPocketWatch.Plugin.Services;
using ServiceStack.Configuration;

namespace QuartzPocketWatch.Plugin
{
    public class WebInterfacePlugin : ISchedulerPlugin
    {
        private string _pluginName;
        private static IScheduler _sched;
        private AppHost _ssHost;
        private readonly string _httpLocalhost = ConfigUtils.GetAppSetting("QuartzPocketWatchUrl");

        private static readonly ILog Log = LogManager.GetLogger(typeof(WebInterfacePlugin).ToString());

        public static IScheduler Scheduler
        {
            get { return _sched; }
        }

        public void Initialize(string pluginName, IScheduler sched)
        {
            _sched = sched;
            _pluginName = pluginName;
        }

        public void Start()
        {
            _ssHost = new AppHost();
            _ssHost.Init();

            _ssHost.Start(_httpLocalhost);

            Log.Info(_pluginName + " started listening on: " + _httpLocalhost);
            Log.Info(string.Format("AppHost Created at {0}, listening on {1}", DateTime.Now, _httpLocalhost));
        }

        public void Shutdown()
        {
            if(_ssHost != null)
                _ssHost.Stop();
        }
    }
}