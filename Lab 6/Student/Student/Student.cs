using System;

namespace Student
{
    public class CStudent
    {
        public CStudent(string name, string surname, string patronymic, int age)
        {
            try
            {
                IsCorrectName(name, surname, patronymic);
                IsCorrectAge(age);

                this.name = name;
                this.surname = surname;
                this.patronymic = patronymic;
                this.age = age;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сообщение ошибки: {ex.Message}\nТип ошибки: {ex.GetType()}");
                throw;
            }
        }

        private void IsCorrectName(string name, string surname, string patronymic)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Имя не может быть путым значением.");
            if (name.Contains(' '))
                throw new ArgumentException("Имя не должно содержать пробелы.");
            if (string.IsNullOrEmpty(surname))
                throw new ArgumentNullException("Фамилия не может быть пустым значением.");
            if (surname.Contains(' '))
                throw new ArgumentException("Фамилия не должно содержать пробелы.");
            if (patronymic.Contains(' '))
                throw new ArgumentException("Отчество не должно содержать пробелы.");
        }

        private void IsCorrectAge(int age)
        {
            if (age < 14 || age > 60)
                throw new ArgumentOutOfRangeException("Недопустимый возраст.");
        }
        
        public string GetName()
        {
            return name;
        }

        public string GetSurname()
        {
            return surname;
        }

        public string GetPatronymic()
        {
            if (string.IsNullOrEmpty(patronymic))
                return "У этого студента нет отчества.";
            return patronymic;
        }
        
        public int GetAge()
        {
            return age;
        }

        public void Rename(string name, string surname, string patronymic = "")
        {
            IsCorrectName(name, surname, patronymic);
            
            this.name = name;
            this.surname = surname;
            this.patronymic = patronymic;
        }
        
        public void SetAge(int age)
        {
            IsCorrectAge(age);

            this.age = age;
        }

        public override string ToString()
        {
            return $"Имя: {name}\nФамилия: {surname}\nОтчество: {patronymic}\nВозраст: {age}";
        }

        private string name;
        private string surname;
        private string patronymic = "";
        private int age;
    }
}