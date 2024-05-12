using System;

namespace Student
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CStudent student = new CStudent("Dmitry", "Ildyukov", "", 20);
                Console.WriteLine(student);
                Console.WriteLine();
                student.Rename("Dima", "Ildyukov", "Yurievich");
                Console.WriteLine(student);
                Console.WriteLine();
                student.Rename("", "", "");
                Console.WriteLine(student);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Сообщение ошибки: {e.Message}\nТип ошибки: {e.GetType()}");
                Console.WriteLine();
            }
        }
    }
}
