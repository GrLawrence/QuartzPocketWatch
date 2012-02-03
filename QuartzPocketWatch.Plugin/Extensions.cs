using System;
using Quartz;

namespace QuartzPocketWatch.Plugin
{
    public static class Extensions
    {
        public static bool IsJob(this Type type)
        {
            if(typeof(IJob).IsAssignableFrom(type))
            {
                if(type.FullName.StartsWith("Quartz.Job"))
                    return true;

                if(type.FullName.StartsWith("Quartz."))
                    return false;
                
                return true;
            }

            return false;
        }
    }
}