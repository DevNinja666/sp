// ======================= CalcConsole =======================
// File: Program.cs
using System;
using System.IO;
using System.Reflection;

class Program
{
    static void Main()
    {
        string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CalcLibrary.dll");

        AppDomain domain = AppDomain.CreateDomain("CalcDomain");

        Assembly asm = domain.Load(AssemblyName.GetAssemblyName(dllPath));

        Type type = asm.GetType("CalcLibrary.CalculatorService");

        dynamic calc = domain.CreateInstanceAndUnwrap(
            asm.FullName,
            type.FullName
        );

        Console.Write("Enter expression (example: 10+5): ");
        string expr = Console.ReadLine();

        double result = calc.Calculate(expr);
        Console.WriteLine("Result = " + result);

        AppDomain.Unload(domain);

        Console.WriteLine("DLL unloaded.");
        Console.ReadKey();
    }
}
