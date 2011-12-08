using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QuartzPocketWatch.Plugin.Models
{
    public class JobGroup
    {
        private readonly List<JobModel> _jobs;

        public JobGroup(string groupName, List<JobModel> jobs)
        {
            GroupName = groupName;
            _jobs = jobs;
        }

        public string GroupName { get; private set; }
        public ReadOnlyCollection<JobModel> Jobs { get { return _jobs.AsReadOnly(); } }
    }
}