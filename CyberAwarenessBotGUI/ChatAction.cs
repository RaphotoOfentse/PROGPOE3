using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberAwarenessBotGUI
{
    public class ChatAction
    {
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }

        public override string ToString()
        {
            if (ReminderDate.HasValue)
            {
                return $"Reminder set for \"{Description}\" on {ReminderDate.Value.ToShortDateString()}";
            }
            else
            { 
                return $"Task added: \"{Description}\" (no reminder set)";
            }
        }
    }
}
