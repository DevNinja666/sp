//loadingservers.cs
static bool loading = false;
static int percent = 0;

static void StartLoading()
{
    loading = true;
    percent = 0;

    new Thread(() =>
    {
        while (loading)
        {
            Console.SetCursorPosition(0, 15);
            Console.Write($"Loading database... {percent}%   ");
            Thread.Sleep(100);
        }
    }).Start();
}


//loadinservices.cs
using System;
using System.Threading;

public static class LoadingService
{
    static bool loading = false;
    static int percent = 0;

    public static void StartLoading()
    {
        loading = true;
        percent = 0;

        new Thread(() =>
        {
            while (loading)
            {
                Console.SetCursorPosition(0, 15);
                Console.Write($"Loading database... {percent}%   ");
                Thread.Sleep(100);
            }
        }).Start();
    }

    public static void SetPercent(int value)
    {
        percent = value;
    }

    public static void StopLoading()
    {
        loading = false;
        Console.SetCursorPosition(0, 15);
        Console.WriteLine("Loading completed!        ");
    }
}

//AuthorService.cs

static void GetAll()
{
    Console.Clear();
    StartLoading();

    List<string> authors = new();

    Thread.Sleep(3000);   

    using var con = new SqlConnection(connectionString);
    con.Open();

    var cmd = new SqlCommand("SELECT Id, FirstName, LastName FROM Authors", con);
    var reader = cmd.ExecuteReader();

    while (reader.Read())
    {
        authors.Add($"{reader["Id"]}. {reader["FirstName"]} {reader["LastName"]}");
        percent = Math.Min(100, percent + 5);
    }

    StopLoading();
    Console.Clear();

    Console.WriteLine("📚 AUTHORS:");
    foreach (var a in authors)
        Console.WriteLine(a);

    Console.ReadKey();
}


static void AddMenu()
{
    Console.Clear();

    Console.Write("First name: ");
    string first = Console.ReadLine();

    Console.Write("Last name: ");
    string last = Console.ReadLine();

    using var con = new SqlConnection(connectionString);
    con.Open();

    var cmd = new SqlCommand(
        "INSERT INTO Authors VALUES ((SELECT MAX(Id)+1 FROM Authors), @f, @l)", con);

    cmd.Parameters.AddWithValue("@f", first);
    cmd.Parameters.AddWithValue("@l", last);

    cmd.ExecuteNonQuery();

    Console.WriteLine("Author added!");
    Console.ReadKey();
}
//edit.cs
static void EditMenu()
{
    Console.Clear();

    Console.Write("Author ID: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("New First name: ");
    string f = Console.ReadLine();

    Console.Write("New Last name: ");
    string l = Console.ReadLine();

    using var con = new SqlConnection(connectionString);
    con.Open();

    var cmd = new SqlCommand(
        "UPDATE Authors SET FirstName=@f, LastName=@l WHERE Id=@id", con);

    cmd.Parameters.AddWithValue("@id", id);
    cmd.Parameters.AddWithValue("@f", f);
    cmd.Parameters.AddWithValue("@l", l);

    cmd.ExecuteNonQuery();

    Console.WriteLine("Updated!");
    Console.ReadKey();
}
//menu.cs
if (count == 0) GetAll();
else if (count == 1) AddMenu();
else if (count == 2) EditMenu();
else if (count == 3) return;



static void GetAll() { Console.Clear(); StartLoading(); List<string> authors = new(); Thread.Sleep(3000); 
