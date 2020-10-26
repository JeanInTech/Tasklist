using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace TaskList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SetTask> tasks = new List<SetTask>
        {
            new SetTask("Jean", "Finish this project", DateTime.Parse("10/26/2020")),
            new SetTask("Jean", "Code some more", DateTime.Parse("10/27/2020")),
            new SetTask("Grant", "Update student workbooks", DateTime.Parse("11/12/2020")),
        };

            Console.WriteLine($"Welcome to the Task Manager!" + Environment.NewLine);
            bool userContinue = true;
            while (userContinue)
            {
                GetMenu();
                string input = GetUserInput($"\nWhat would you like to do? ");
                if (input == "1")
                {
                    input = GetUserInput("\nWould you like to lists tasks by number or name? ").ToLower();

                    if (input == "name" || input == "number") 
                    { 
                        Console.WriteLine("\n================== TASK LIST =================");
                        ListTasks(tasks, input);
                        Console.WriteLine("==============================================\n");
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input, please try again.\n");
                    }
                }
                else if (input == "2")
                {
                    Console.WriteLine("\n================== ADD TASK ==================\n");
                    input = GetUserInput("Team Member: ");
                    tasks.Add(AddTask(input));
                    Console.WriteLine("\n============ Task has been added! ============\n");
                }
                else if (input == "3")
                {
                    Console.WriteLine("\n================ DELETE TASK =================\n");

                    for (int i = 0; i < tasks.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {tasks[i].Name}: {tasks[i].Description} (due:{tasks[i].DueDate:d})");
                    }
                    input = GetUserInput($"\nWhich task would you like to remove? [Select 1-{tasks.Count}] ");
                    int index = ParseInput(input, tasks) - 1;
                    Console.WriteLine("\nAre you sure you want to delete this task?");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n   {index + 1}. {tasks[index].Name}: {tasks[index].Description} (due:{tasks[index].DueDate:d})\n");
                    Console.ResetColor();
                    input = GetUserInput("To continue, please enter [y]: ");
                    if(input == "y")
                    {
                        tasks.RemoveAt(index);
                        Console.WriteLine("\n=========== Task has been removed! ============\n");
                    }
                    else
                    {
                        Console.WriteLine("\n======= Task removal has been canceled! =======\n");
                    }

                }
                else if (input == "4")
                {
                    Console.WriteLine("\n============== MARK AS COMPLETED =============\n");
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        Console.WriteLine($"Task {i + 1}. {tasks[i].Name}: {tasks[i].Description} [due: {tasks[i].DueDate:d}]");
                    }
                    input = GetUserInput($"\nWhich task would you like to mark as completed? [Select 1-{tasks.Count}] ");
                    int index = ParseInput(input, tasks) - 1;
                    Console.WriteLine("\nAre you sure you want to mark this as completed?");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n   Task {index + 1}. {tasks[index].Name}: {tasks[index].Description} [due: {tasks[index].DueDate:d}]\n");
                    Console.ResetColor();
                    input = GetUserInput("To continue, please enter [y]: ");
                    if (input == "y")
                    {
                        tasks[index].Completed=true;
                        Console.WriteLine("\n========== Task has been completed! ===========\n");

                    }
                    else
                    {
                        Console.WriteLine("\n====== Task completion has been canceled! ======\n");
                    }
                }
                else if (input == "5")
                {

                    Console.WriteLine("\n================== EDIT TASK =================\n");
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        Console.WriteLine($"Task {i + 1}. {tasks[i].Name}: {tasks[i].Description} [due: {tasks[i].DueDate:d}] || status: {IsItCompleted(tasks, i, tasks[i].Completed)}]\n");
                    }
                    input = GetUserInput($"\nWhich task would you like to edit? [Select 1-{tasks.Count}] ");
                    int index = ParseInput(input, tasks) - 1;

                    Console.WriteLine("\nYou have selected the following line:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n   Task {index + 1}. {tasks[index].Name}: {tasks[index].Description} [due: {tasks[index].DueDate:d} || status: {IsItCompleted(tasks, index, tasks[index].Completed)}]\n");
                    Console.ResetColor();

                    input = GetUserInput("What would you like to edit? Please enter [1. Name, 2. Description, 3. Due Date, 4. Status or 5. Cancel] ").ToLower();

                    if (input == "name" || input == "1")
                    {
                        input = GetUserInput("Okay, let's change the name. Please enter a new name: ");
                        tasks[index].Name = input;
                        Console.WriteLine("\n============ Task has been edited! ===========\n");
                    }
                    else if (input == "description" || input == "2")
                    {
                        input = GetUserInput("Okay, let's change the description. Please enter a new description: ");
                        tasks[index].Description = input;
                        Console.WriteLine("\n============ Task has been edited! ===========\n");
                    }
                    else if (input == "due date" || input == "3")
                    {
                        input = GetUserInput("Okay, let's change the due date. Please enter a new due date: ");
                        tasks[index].DueDate = DateTime.Parse(input);
                        Console.WriteLine("\n============ Task has been edited! ===========\n");
                    }
                    else if (input == "status" || input == "4")
                    {
                        input = GetUserInput("Okay, let's change the status. Please enter a new status: ");
                        if(input == "finsihed")
                        {
                            tasks[index].Completed = true;
                        }
                        else if(input == "not finished")
                        {
                            tasks[index].Completed = false;
                        }
                        Console.WriteLine("\n============ Task has been edited! ===========\n");
                    }
                    else if (input == "cancel" || input == "5")
                    {
                        Console.WriteLine("\n====== Task editing has been canceled! =======\n");
                        continue;
                    }
                }
                else if (input == "6")
                {
                    userContinue = false;
                    Console.WriteLine(Environment.NewLine + "Have a great day!");
                }
            }
        }
        public static void GetMenu()
        {
            Console.WriteLine($"  1.  List tasks");
            Console.WriteLine($"  2.  Add task");
            Console.WriteLine($"  3.  Delete task");
            Console.WriteLine($"  4.  Mark task completed");
            Console.WriteLine($"  5.  Edit task");
            Console.WriteLine($"  6.  Quit");
        }
        public static string GetUserInput(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine().Trim();
            return input;
        }
        public static void ListTasks(List<SetTask> list, string input)
        {
            Console.WriteLine();
            if (input == "number") 
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine($"   Task {i + 1} - Due: {list[i].DueDate:d}");
                    Console.WriteLine($"   Team Member: {list[i].Name}");
                    Console.WriteLine($"   Task Description: {list[i].Description}");
                    if (list[i].Completed)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"   Status: Finished!\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"   Status: Not finished\n");
                        Console.ResetColor();
                    }
                }
            } 
            else if (input == "name")
            {
                string name = GetUserInput("What's the name of this team member? ");
                for (int i = 0; i < list.Count; i++)
                {
                    if(name == list[i].Name)
                    {
                        Console.WriteLine($"   Team Member: {list[i].Name}");
                        Console.WriteLine($"   Due: {list[i].DueDate:d}");
                        Console.WriteLine($"   Task Description: {list[i].Description}");
                        if (list[i].Completed)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"   Status: Finished!\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"   Status: Not finished\n");
                            Console.ResetColor();
                        }
                    }
                }
            }
        }
        public static string IsItCompleted(List<SetTask> list, int index, bool completionStatus)
        {
            if (list[index].Completed)
            {
                return "finished";
            }
            else
            {
                return "not finished";
            }
        }
        public static SetTask AddTask(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = ValidateInput(name);
            }

            string description = GetUserInput("Task Description: ");
            if (string.IsNullOrEmpty(description))
            {
                description = ValidateInput(description);
            }

            string deadline = GetUserInput("Due Date: ");
            DateTime dueDate;

            bool success = DateTime.TryParse(deadline, out dueDate);
            while(!success)
            {
                Console.WriteLine("Invalid input. Try again.");
                deadline = GetUserInput("Due Date: ");
                success = DateTime.TryParse(deadline, out dueDate);
            }
            DateTime.TryParse(deadline, out dueDate);

            return new SetTask(name, description, dueDate);
        }
        public static string ValidateInput(string input)
        {
            while (true)
            {
                string updatedInput = GetUserInput("No information detected. Please input the requested information\n");

                if (string.IsNullOrEmpty(updatedInput))
                {
                    continue;
                }
                else
                {
                    return updatedInput;
                }
            }
        }
        public static int ParseInput(string input, List<SetTask>list)
        {

            while (true)
            {
                if (Int32.TryParse(input, out int index))
                {
                    if (index < 1 || index > list.Count)
                    {
                        input = GetUserInput($"\nInvalid input. [Select 1-{list.Count}] ");
                        continue;
                    }
                    else
                        return index;
                }
                else
                {
                    input = GetUserInput($"\nInvalid input. [Select 1-{list.Count}] ");
                }
            }
        }
    }
}
