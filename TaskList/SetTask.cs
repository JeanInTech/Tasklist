using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList
{
    class SetTask
    {
        private string name;
        private string description;
        DateTime dueDate;
        bool completed;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }
        public bool Completed
        {
            get { return completed; }
            set { completed = value; }
        }
        public SetTask(string Name, string Description, DateTime DueDate)
        {
            name = Name;
            description = Description;
            dueDate = DueDate;
            completed = Completed;
        }
        public void GetTask()
        {
            Console.WriteLine($"   Team Member Name: {Name}");
            Console.WriteLine($"   Task Description: { Description}");
            Console.WriteLine($"   Task Due Date: {DueDate:d}\n");
        }
        public void QuickTaskList()
        {
            Console.WriteLine($"{name} - {description}");
        }
    }
}
