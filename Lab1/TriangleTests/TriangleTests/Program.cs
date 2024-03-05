using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TriangleTests;

class Program
{
    private const string path = "tests.txt";
    
    static void Main()
    {
        try
        {
            string[] tests = File.ReadAllLines(path);
            foreach (var test in tests)
            {
                string[] allArguments = test.Split(' ');
                string expectedAnswer = allArguments[allArguments.Length - 1];
                List<string> arguments = allArguments.ToList();
                if (arguments.Count > 0)
                    arguments.RemoveAt(arguments.Count - 1);
                string argument = string.Join(' ', arguments);
                string relativePath = "Triangle.exe";
                string currentDirectory = Directory.GetCurrentDirectory();
                string absolutePath = Path.Combine(currentDirectory, relativePath);
                Process process = new Process();
                process.StartInfo.FileName = absolutePath;
                process.StartInfo.Arguments = argument;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(output.ToLower().Replace(" ", string.Empty).Replace("\r\n", string.Empty) == expectedAnswer
                    ? "success"
                    : "error");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Произошла ошибка при считывании с файла: {e}");
        }
    }
}