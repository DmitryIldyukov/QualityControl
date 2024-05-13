using Microsoft.EntityFrameworkCore;
using Moq;
using Mountebank.Data;
using Mountebank.Data.Configurations;
using Mountebank.Data.Interfaces;
using Mountebank.Exceptions;
using Mountebank.Services;
using Xunit;

namespace MountebankTests;

public class MountebankServiceTests
{
    private readonly Mock<AppDbContext> _mockDbContext;

    public MountebankServiceTests()
    {
        _mockDbContext = new Mock<AppDbContext>();
    }

    [Fact]
    public async Task Positive_GetAllCurrencies()
    {
        // Arrange
        var currencies = new List<Currency>()
        {
            new Currency { Id = 1, Name = "RUB", Rate = 1.0 },
            new Currency { Id = 2, Name = "USD", Rate = 73.4419 },
            new Currency { Id = 3, Name = "EUR", Rate = 79.7264 }
        };
        
        _mockDbContext.Setup(db => db.Currencies).Returns(() => MockDbSet(currencies));
        
        var mountebankService = new MountebankService(_mockDbContext.Object);
        
        // Act
        var result = mountebankService.GetAll();
        
        // Assert
        Assert.Equal(currencies, result);
    }
    
    [Fact]
    public async Task Positive_GetCurrencyById()
    {
        // Arrange
        var currencies = new List<Currency>()
        {
            new Currency { Id = 1, Name = "RUB", Rate = 1.0 },
            new Currency { Id = 2, Name = "USD", Rate = 73.4419 },
            new Currency { Id = 3, Name = "EUR", Rate = 79.7264 }
        };
        
        _mockDbContext.Setup(db => db.Currencies).Returns(() => MockDbSet(currencies));
        
        var mountebankService = new MountebankService(_mockDbContext.Object);
        
        // Act
        var result = mountebankService.GetById(1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("RUB", result.Name);
        Assert.Equal(1.0, result.Rate);
    }
    
    [Fact]
    public async Task Negative_GetCurrencyById()
    {
        // Arrange
        var currencies = new List<Currency>()
        {
            new Currency { Id = 1, Name = "RUB", Rate = 1.0 },
            new Currency { Id = 2, Name = "USD", Rate = 73.4419 },
            new Currency { Id = 3, Name = "EUR", Rate = 79.7264 }
        };
        
        _mockDbContext.Setup(db => db.Currencies).Returns(() => MockDbSet(currencies));
        
        var mountebankService = new MountebankService(_mockDbContext.Object);
        
        // Act & Assert
        Assert.Throws<RecordNotFound>(() => mountebankService.GetById(4));
    }
    
    [Fact]
    public async Task Positive_GetCurrencyByName()
    {
        // Arrange
        var currencies = new List<Currency>()
        {
            new Currency { Id = 1, Name = "RUB", Rate = 1.0 },
            new Currency { Id = 2, Name = "USD", Rate = 73.4419 },
            new Currency { Id = 3, Name = "EUR", Rate = 79.7264 }
        };
        
        _mockDbContext.Setup(db => db.Currencies).Returns(() => MockDbSet(currencies));
        
        var mountebankService = new MountebankService(_mockDbContext.Object);
        
        // Act
        var result = mountebankService.GetByName("Rub");
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("RUB", result.Name);
        Assert.Equal(1.0, result.Rate);
    }
    
    [Fact]
    public async Task Negative_GetCurrencyByName()
    {
        // Arrange
        var currencies = new List<Currency>()
        {
            new Currency { Id = 1, Name = "RUB", Rate = 1.0 },
            new Currency { Id = 2, Name = "USD", Rate = 73.4419 },
            new Currency { Id = 3, Name = "EUR", Rate = 79.7264 }
        };
        
        _mockDbContext.Setup(db => db.Currencies).Returns(() => MockDbSet(currencies));
        
        var mountebankService = new MountebankService(_mockDbContext.Object);
        
        // Act & Assert
        Assert.Throws<RecordNotFound>(() => mountebankService.GetByName("BYN"));
    }
    
    public static DbSet<T> MockDbSet<T>(IEnumerable<T> list) where T : class
    {
        var queryable = list.AsQueryable();

        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        return dbSetMock.Object;
    }
}