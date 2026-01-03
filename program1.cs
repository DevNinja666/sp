using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    static string blackListFile = "blacklist.txt";
    static List<string> blackList = new List<string>();

    static void Main()
    {
        LoadBlackList();
        new Thread(BlackListWatcher).Start();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== TASK MANAGER ===");
            Console.WriteLine("1. Show all processes");
            Console.WriteLine("2. Start process");
            Console.WriteLine("3. Kill process by ID");
            Console.WriteLine("4. Kill process by name");
            Console.WriteLine("5. Add to blacklist");
            Console.WriteLine("6. Remove from blacklist");
            Console.WriteLine("7. Exit");
            Console.Write("\nChoose: ");

            switch (Console.ReadLine())
            {
                case "1": ShowProcesses(); break;
                case "2": StartProcess(); break;
                case "3": KillById(); break;
                case "4": KillByName(); break;
                case "5": AddToBlackList(); break;
                case "6": RemoveFromBlackList(); break;
                case "7": return;
            }
        }
    }
    static void ShowProcesses()
    {
        Console.Clear();
        foreach (var p in Process.GetProcesses())
        {
            Console.WriteLine($"{p.Id,-8} {p.ProcessName}");
        }
        Pause();
    }
    static void StartProcess()
    {
        Console.Write("Enter process name (example: notepad): ");
        string name = Console.ReadLine();
        try
        {
            Process.Start(name);
        }
        catch
        {
            Console.WriteLine("Cannot start process");
        }
        Pause();
    }
    static void KillById()
    {
        Console.Write("Enter process ID: ");
        int id = int.Parse(Console.ReadLine());

        try
        {
            Process.GetProcessById(id).Kill();
            Console.WriteLine("Process killed");
        }
        catch
        {
            Console.WriteLine("Process not found");
        }
        Pause();
    }

    
    static void KillByName()
    {
        Console.Write("Enter process name: ");
        string name = Console.ReadLine();

        var processes = Process.GetProcessesByName(name);
        foreach (var p in processes)
            p.Kill();

        Console.WriteLine("Processes killed");
        Pause();
    }

    static void AddToBlackList()
    {
        Console.Write("Enter process name: ");
        string name = Console.ReadLine().ToLower();

        if (!blackList.Contains(name))
        {
            blackList.Add(name);
            SaveBlackList();
        }

        Console.WriteLine("Added to blacklist");
        Pause();
    }

    static void RemoveFromBlackList()
    {
        Console.Write("Enter process name: ");
        string name = Console.ReadLine().ToLower();

        blackList.Remove(name);
        SaveBlackList();

        Console.WriteLine("Removed");
        Pause();
    }

   
    static void BlackListWatcher()
    {
        while (true)
        {
            foreach (var name in blackList)
            {
                foreach (var p in Process.GetProcessesByName(name))
                    p.Kill();
            }
            Thread.Sleep(2000);
        }
    }

    static void LoadBlackList()
    {
        if (File.Exists(blackListFile))
            blackList = new List<string>(File.ReadAllLines(blackListFile));
    }

    static void SaveBlackList()
    {
        File.WriteAllLines(blackListFile, blackList);
    }

    static void Pause()
    {
        Console.WriteLine("\nPress Enter...");
        Console.ReadLine();
    }
}
