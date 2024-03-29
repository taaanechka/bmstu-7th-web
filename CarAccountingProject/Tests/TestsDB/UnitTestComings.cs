using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using EntityFrameworkCoreMock;

using Xunit;
using Xunit.Abstractions;
using Moq;

using DB;
using BL;

namespace TestsDB;

public class UnitTestComings: IDisposable
{
    public DbContextOptions<ApplicationContext> DummyOptions { get; } = new DbContextOptionsBuilder<ApplicationContext>().Options;

    private DbContextMock<ApplicationContext> dbContextMock;
    private DbSetMock<DB.Coming> ComingsDbSetMock;
    private DB.ComingsRepository Rep;

    public UnitTestComings()
    {
        DateTime Date1 = DateTime.ParseExact("2022-04-10", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);

        DateTime Date2 = DateTime.ParseExact("2022-04-11", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);

        var initialEntities = new[]
        {
            new DB.Coming {Id = 1, UserId = 1, ComingDate = Date1},
            new DB.Coming {Id = 2, UserId = 1, ComingDate = Date1},
            new DB.Coming {Id = 3, UserId = 2, ComingDate = Date2}
        };

        // Arrange
        dbContextMock = new DbContextMock<ApplicationContext>(DummyOptions);
        ComingsDbSetMock = dbContextMock.CreateDbSetMock(x => x.Comings, initialEntities);

        Rep = new DB.ComingsRepository(dbContextMock.Object);
    }

    public void Dispose() {}

    [Fact]
    public void TestGetComings()
    {
        // Act
        List<BL.Coming> Comings = Rep.GetComings();

        // Assert
        Assert.Equal(3, dbContextMock.Object.Comings.Count());
        Assert.Equal(3, Comings.Count);
    }

    [Fact]
    public void TestGetComingByIdCorrect()
    {
        // Act
        BL.Coming Coming = Rep.GetComingById(1);

        // Assert
        Assert.NotNull(Coming);
    }

    [Fact]
    public void TestGetComingByIdUncorrect()
    {
        // Assert
        Assert.Throws<DB.ComingNotFoundException>(()=> Rep.GetComingById(5));
    }

    // [Fact]
    // public async Task TestAddComingAsyncCorrect()
    // {   
    //     BL.Car car = new BL.Car("Id11", 1, 1, 5, 1);

    //     DateTime Date2 = DateTime.ParseExact("2022-04-11", "yyyy-MM-dd",
    //                                     System.Globalization.CultureInfo.InvariantCulture);
    //     Date2 = DateTime.SpecifyKind(Date2, DateTimeKind.Utc);  
    //     var Coming = new BL.Coming(4, 3, Date2);

    //     // Assert: initial
    //     Assert.Equal(3, dbContextMock.Object.Comings.Count());

    //     // Act
    //     await Rep.AddComingAsync(Coming, car);

    //     // Assert: final
    //     ComingsDbSetMock.Verify(m => m.Add(It.IsAny<DB.Coming>()), Times.Once());
    //     dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

    //     Assert.Equal(4, dbContextMock.Object.Comings.Count());
    // }

    [Fact]
    public async Task TestAddComingAsyncUncorrect()
    {
        BL.Car car = new BL.Car("Id11", 1, 1, 5, 1);

        DateTime Date2 = DateTime.ParseExact("2022-04-11", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
        Date2 = DateTime.SpecifyKind(Date2, DateTimeKind.Utc);
        var Coming = new BL.Coming(4, -3, Date2);

        // Assert
        await Assert.ThrowsAsync<DB.ComingsValidatorFailException>(()=> Rep.AddComingAsync(Coming, car));
    }

    [Fact]
    public async Task TestDeleteComingAsyncCorrect()
    {
        // Act
        await Rep.DeleteComingAsync(3);

        ComingsDbSetMock.Verify(m => m.Remove(It.IsAny<DB.Coming>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        // Assert
        Assert.Equal(2, dbContextMock.Object.Comings.Count());
    }

    [Fact]
    public async Task TestDeleteComingAsyncUncorrect()
    {
        // Assert
        await Assert.ThrowsAsync<DB.ComingNotFoundException>(()=> Rep.DeleteComingAsync(5));
    }
}