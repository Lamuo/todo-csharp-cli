﻿using System.Globalization;
using System.Threading.Tasks;

namespace To_Do_List

{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string path = "tasks.json";
            List<TaskItem> tasks = TaskManager.LoadFile(path);

            string? choice;
            do {
                ShowOptions();
                choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":                     
                        ShowTasks(ShowFilter(tasks));
                        break;
                    case "2":
                        AddTask(tasks);
                        break;
                    case "3":
                        EditTask(tasks);
                        break;
                    case "4":
                        CompleteTask(tasks);
                        break;
                    case "5":
                        DeleteTask(tasks);
                        break;
                    case "6":
                        TaskManager.SaveFile(tasks, path);
                        Console.WriteLine("Tasks saved. Exiting...");
                        await Task.Delay(1000);
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                Console.Write("Press any key to continue or esc to exit the program... ");
                if(Convert.ToString(Console.ReadKey().Key).Contains("scape"))
                {
                    choice = "6";
                }
                Console.Clear();
            } while(choice != "6");
        }

        static void ShowOptions()
        {
            Console.WriteLine("===== TO-DO LIST =====");
            Console.WriteLine("\n1. View Tasks");
            Console.WriteLine("2. Add Task");
            Console.WriteLine("3. Edit Task");
            Console.WriteLine("4. Complete Task");
            Console.WriteLine("5. Delete Task");
            Console.WriteLine("6. Save and Exit");
            Console.Write("\nChoose an option: ");
        }

        static List<TaskItem> ShowFilter(List<TaskItem> tasks)
        {
            Console.WriteLine("Choose one filter:");
            Console.WriteLine("1. View All Tasks");
            Console.WriteLine("2. View Active Tasks");
            Console.WriteLine("3. View Completed Tasks");
            Console.WriteLine("4. View Tasks by Priority");
            Console.WriteLine("5. View Tasks by Due Date");
            string? showInput = Console.ReadLine();
            List<TaskItem> showChoice;
            switch (showInput)
            {
                case "1":
                    showChoice = tasks;
                    break;
                case "2":
                    showChoice = tasks.Where(t => !t.isCompleted).ToList();
                    break;
                case "3":
                    showChoice = tasks.Where(t => t.isCompleted).ToList();
                    break;
                case "4":
                    showChoice = tasks.OrderByDescending(t => t.priority).ToList();
                    break;
                case "5":
                    showChoice = tasks.OrderBy(t => t.dueDate).ToList();
                    break;
                default:
                    Console.WriteLine("choice not supported, defaulting to All Tasks...");
                    showChoice = tasks;
                    break;
            }
            return showChoice;
        }

        static void ShowTasks(List<TaskItem> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }
            foreach (var task in tasks)
            {
                Console.WriteLine($"Title: {task.title}");
                Console.WriteLine($"Description: {task.description}");
                Console.WriteLine($"Priority: {task.priority}");
                Console.WriteLine($"Due Date: {task.dueDate.ToShortDateString()}");
                Console.WriteLine($"Completed: {task.isCompleted}");
                Console.WriteLine("-----------------------------");
            }
        }

        static void AddTask(List<TaskItem> tasks)
        {
            Console.WriteLine("Enter a new task:");
            string? taskTitle = Console.ReadLine();
            Console.WriteLine("Enter task description:");
            string? taskDescription = Console.ReadLine();
            Console.WriteLine("Enter the task priority (Low, Medium, High):");
            string? priorityInput = Console.ReadLine();
            Console.WriteLine("Enter the due date (yyyy-MM-dd):");
            string? dueDate = Console.ReadLine();
            TaskItem.PriorityLevel priority;
            tasks.Add(new TaskItem(
                title: taskTitle,
                description: taskDescription,
                priority: Enum.TryParse(priorityInput, true, out priority) ? priority : TaskItem.PriorityLevel.Medium,
                dueDate: DateTime.TryParse(dueDate, out DateTime parsedDate) ? parsedDate : DateTime.Now.AddDays(7),
                isCompleted: false
            ));
        }

        static void EditTask(List<TaskItem> tasks)
        {
            int option=0;
            foreach (var task in tasks)
            {
                option++;
                Console.WriteLine($"Task {option}: {task.title}");
            }
            Console.Write("Select a task to edit by number: ");
            option = Convert.ToInt32(Console.ReadLine())-1;

            Console.WriteLine($"Title: {tasks[option].title}");
            Console.WriteLine($"Description: {tasks[option].description}");
            Console.WriteLine($"Priority: {tasks[option].priority}");
            Console.WriteLine($"Due Date: {tasks[option].dueDate.ToShortDateString()}");
            Console.WriteLine($"Completed: {tasks[option].isCompleted}");

            Console.WriteLine("Enter new title (leave blank to keep current): ");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTitle))
            {
                tasks[option].title = newTitle;
            }
            Console.WriteLine("Enter new description (leave blank to keep current): ");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDescription))
            {
                tasks[option].description = newDescription;
            }
            Console.WriteLine("Enter new priority (Low, Medium, High) (leave blank to keep current): ");
            string newPriority = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPriority))
            {
                if (Enum.TryParse(newPriority, true, out TaskItem.PriorityLevel priority))
                {
                    tasks[option].priority = priority;
                }
                else
                {
                    Console.WriteLine("Invalid priority. Keeping current priority.");
                }
            }
            Console.WriteLine("Enter new due date (yyyy-MM-dd) (leave blank to keep current): ");
            string newDueDate = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDueDate))
            {
                if (DateTime.TryParse(newDueDate, out DateTime parsedDate))
                {
                    tasks[option].dueDate = parsedDate;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Keeping current due date.");
                }
            }
        }

        static void CompleteTask(List<TaskItem> tasks)
        {
            Console.WriteLine("Please select a task to mark as completed:");
            int option = 0;
            foreach (var task in tasks)
            {
                option++;
                if (task.isCompleted)
                {
                    continue; // Skip completed tasks
                }
                Console.WriteLine($"Task {option}: {task.title}");
            }
            if (option == 0)
            {
                Console.WriteLine("No tasks available to complete.");
                return;
            }
            Console.Write("Select a task to complete by number: ");
            option = Convert.ToInt32(Console.ReadLine()) - 1;

            if (option < 0 || option >= tasks.Count || tasks[option].isCompleted)
            {
                Console.WriteLine("Invalid task selection.");
                return;
            }
            tasks[option].isCompleted = true;
            Console.WriteLine($"Task '{tasks[option].title}' marked as completed.");
        }

        static void DeleteTask(List<TaskItem> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available to delete.");
                return;
            }
            Console.WriteLine("Please select a task to delete:");
            int option = 0;
            foreach (var task in tasks)
            {
                option++;
                Console.WriteLine($"Task {option}: {task.title}");
            }
            Console.Write("Select a task to delete by number: ");
            option = Convert.ToInt32(Console.ReadLine()) - 1;

            if (option < 0 || option >= tasks.Count)
            {
                Console.WriteLine("Invalid task selection.");
                return;
            }
            tasks.RemoveAt(option);
            option++;
            Console.WriteLine("Task number "+option+" deleted.");
        }
    }
}
