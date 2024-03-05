using System;
using System.Collections.Generic;
using System.Linq;

namespace Triangle;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Неправильный ввод");
            return;
        }
        List<double> verticals = new List<double>();

        foreach (var arg in args)
        {
            string num = arg.Replace(".", ",");
            if (double.TryParse(num, out var @_))
            {
                verticals.Add(_);
            }
            else
            {
                Console.WriteLine($"{arg} не является числом");
                return;
            }
        }
        
        Console.WriteLine(GetTriangleType(verticals));
    }

    private static string GetTriangleType(List<double> verticals)
    {
        if (verticals.Count != 3)
            return "Неправильный ввод";
            
        if (verticals.Any(_ => _ <= 0) || !TriangleCanExist(verticals[0], verticals[1], verticals[2]))
            return "Не треугольник";
        if (verticals[0] == verticals[1] && verticals[0] == verticals[2])
            return "Равносторонний";
        if (verticals[0] == verticals[1] || verticals[0] == verticals[2] || verticals[1] == verticals[2])
            return "Равнобедренный";
        return "Обычный";
    }

    private static bool TriangleCanExist(double a, double b, double c) => ((a + b > c) && (a + c > b) && (b + c > a));
}
