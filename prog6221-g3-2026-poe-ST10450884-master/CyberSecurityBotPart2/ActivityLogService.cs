using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CyberSecurityBotPart2
{
    public class ActivityLogService
    {
        private readonly List<string> activityLog;

        public ActivityLogService()
        {
            activityLog = new List<string>();
        }

        public void AddActivity(string action)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm}] {action}";
            activityLog.Insert(0, logEntry);

            if (activityLog.Count > 50)
            {
                activityLog.RemoveAt(activityLog.Count - 1);
            }
        }

        public string ShowRecentActivities(int count = 10)
        {
            if (activityLog.Count == 0)
            {
                return "No activities have been recorded yet.";
            }

            var recentActivities = activityLog.Take(count).ToList();

            string result = "Here is a summary of recent actions:\n\n";

            for (int i = 0; i < recentActivities.Count; i++)
            {
                result += $"{i + 1}. {recentActivities[i]}\n";
            }

            return result;
        }
    }
}
