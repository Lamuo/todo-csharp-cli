using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List
{
    public class TaskItem
    {
        public string title { get; set; }
        public string description { get; set; }
        public PriorityLevel priority { get; set; }
        public DateTime dueDate { get; set; }
        public bool isCompleted { get; set; }
 
        public TaskItem(string title, string description, PriorityLevel priority, DateTime dueDate, bool isCompleted)
        {
            this.title = title;
            this.description = description;
            this.priority = priority;
            this.dueDate = dueDate;
            this.isCompleted = isCompleted;
        }

        public enum PriorityLevel
        {
            Low, 
            Medium, 
            High
        }
    }
}