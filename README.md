# To-Do CLI in C#

A simple command-line To-Do list application written in C#.

## Features

- Load existing tasks (if any)  
- Add new tasks  
- Mark tasks as done / undone  
- Delete tasks  
- View all tasks  

## Structure

- `Program.cs`: main logic and CLI interface  
- `TaskItem.cs`: definition of the task (to-do item) class  
- `TaskManager.cs`: logic for managing tasks (add, remove, toggle, save, load)  
- `To-Do List.csproj`: project file  

## How to Use

1. Clone the repository  
2. Open the project in Visual Studio or Visual Studio Code (with .NET SDK)  
3. Build the project (targeting .NET 8.0)  
4. Run the compiled application  
5. Follow the on-screen instructions to manage your tasks  

## Requirements

- .NET 8.0 SDK  
- Console (terminal)  

## Future Improvements (ideas)

- Persist tasks between sessions (file storage / CSV / JSON / database)  
- Better command parsing (e.g. support for subcommands)  
- Sorting and filtering of tasks  
- Support for task priorities or due dates  
- Export/import tasks (e.g. to CSV or JSON)  

## License

[MIT License](LICENSE)

---

Made by Alythia
