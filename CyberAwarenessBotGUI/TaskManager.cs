using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberAwarenessBotGUI
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TaskManager
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        // Updated method to accept 3 parameters
        public void AddTask(string title, string description, DateTime reminderDate)
        {
            tasks.Add(new TaskItem
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate,
                IsCompleted = false
            });
        }

        // Renamed to match chatbot logic
        public List<TaskItem> GetAllTasks()
        {
            return tasks;
        }

        public TaskItem MarkRandomTaskComplete()
        {
            var incomplete = tasks.Where(t => !t.IsCompleted).ToList();
            if (incomplete.Count == 0)
                return null;

            var random = new Random();
            var selected = incomplete[random.Next(incomplete.Count)];
            selected.IsCompleted = true;
            return selected;
        }
    }
}
