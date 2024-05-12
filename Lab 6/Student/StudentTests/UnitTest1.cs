using Student;

namespace StudentTests;

public class UnitTest1
{
    [Fact]
    public void Constructor_Valid_Arguments_Object_Created()
    {
        // Arrange
        string name = "John";
        string surname = "Doe";
        string patronymic = "Smith";
        int age = 25;

        // Act
        var student = new CStudent(name, surname, patronymic, age);

        // Assert
        Assert.Equal(name, student.GetName());
        Assert.Equal(surname, student.GetSurname());
        Assert.Equal(patronymic, student.GetPatronymic());
        Assert.Equal(age, student.GetAge());
    }
    
    [Fact]
    public void Constructor_Valid_Arguments_With_Empry_Patronymic_Object_Created()
    {
        // Arrange
        string name = "John";
        string surname = "Doe";
        string patronymic = "";
        int age = 25;

        // Act
        var student = new CStudent(name, surname, patronymic, age);

        // Assert
        Assert.Equal(name, student.GetName());
        Assert.Equal(surname, student.GetSurname());
        Assert.Equal("У этого студента нет отчества.", student.GetPatronymic());
        Assert.Equal(age, student.GetAge());
    }

    [Theory]
    [InlineData("", "Doe", "Smith", 25)]
    [InlineData("John", "", "Smith", 25)]
    [InlineData(null, "Doe", "Smith", 25)]
    [InlineData("John", null, "Smith", 25)]
    public void Constructor_Null_Or_Empty_Name_Or_Surname_Throws_Exception(string name, string surname, string patronymic, int age)
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CStudent(name, surname, patronymic, age));
    }

    [Theory]
    [InlineData("John Smith", "Doe", "Smith", 25)]
    [InlineData("John", "Doe Smith", "Smith", 25)]
    [InlineData("John", "Doe", "Smith Smith", 25)]
    public void Constructor_Name_Surname_Or_Patronymic_With_Space_Throws_Exception(string name, string surname, string patronymic, int age)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new CStudent(name, surname, patronymic, age));
    }

    [Theory]
    [InlineData("John", "Doe", "Smith", 13)]
    [InlineData("John", "Doe", "Smith", 61)]
    public void Constructor_Invalid_Age_Throws_Exception(string name, string surname, string patronymic, int age)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new CStudent(name, surname, patronymic, age));
    }

    [Fact]
    public void Rename_Valid_Arguments_Name_Surname_Changed()
    {
        // Arrange
        var student = new CStudent("John", "Doe", "Smith", 25);
        string newName = "Jane";
        string newSurname = "Doe";

        // Act
        student.Rename(newName, newSurname);

        // Assert
        Assert.Equal(newName, student.GetName());
        Assert.Equal(newSurname, student.GetSurname());
    }

    [Fact]
    public void Set_Age_ValidAge_Age_Changed()
    {
        // Arrange
        var student = new CStudent("John", "Doe", "Smith", 25);
        int newAge = 30;

        // Act
        student.SetAge(newAge);

        // Assert
        Assert.Equal(newAge, student.GetAge());
    }
}