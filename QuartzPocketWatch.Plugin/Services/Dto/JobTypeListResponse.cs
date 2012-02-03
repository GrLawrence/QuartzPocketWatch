using System;
using System.Collections.Generic;

namespace QuartzPocketWatch.Plugin.Services.Dto
{
    public class JobTypeListResponse
    {
        private readonly List<string> _availableTypes =  new List<string>();

        public JobTypeListResponse(IEnumerable<Type> availableTypes)
        {
            foreach (Type type in availableTypes)
            {
                _availableTypes.Add(type.FullName);
            }
        }

        public List<string> AvailableTypes
        {
            get { return _availableTypes; }
        }
    }
}