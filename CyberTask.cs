using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityBotPart2
{
    public class CyberTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public string GetStatus()
        {
            return IsCompleted ? "Completed" : "Pending";
        }

        public string GetReminderText()
        {
            if (ReminderDate.HasValue)
            {
                return ReminderDate.Value.ToString("yyyy-MM-dd HH:mm");
            }

            return "No reminder";
        }
    }
}
