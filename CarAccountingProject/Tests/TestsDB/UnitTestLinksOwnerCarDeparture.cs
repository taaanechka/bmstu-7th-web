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

public class UnitTestLinksOwnerCarDeparture: IDisposable
{
    public DbContextOptions<ApplicationContext> DummyOptions { get; } = new DbContextOptionsBuilder<ApplicationContext>().Options;

    private DbContextMock<ApplicationContext> dbContextMock;
    private DbSetMock<DB.LinkOwnerCarDeparture> LinksOwnerCarDepartureDbSetMock;
    private DB.LinksOwnerCarDepartureRepository Rep;

    public UnitTestLinksOwnerCarDeparture()
    {
        var initialEntities = new[]
        {
            new DB.LinkOwnerCarDeparture {Id = 1, OwnerId = 1, CarId = "Number1", DepartureId = 1},
            new DB.LinkOwnerCarDeparture {Id = 2, OwnerId = 2, CarId = "Number2", DepartureId = 2},
            new DB.LinkOwnerCarDeparture {Id = 3, OwnerId = 3, CarId = "Number3", DepartureId = 3}
        };

        // Arrange
        dbContextMock = new DbContextMock<ApplicationContext>(DummyOptions);
        LinksOwnerCarDepartureDbSetMock = dbContextMock.CreateDbSetMock(x => x.LinksOwnerCarDeparture, initialEntities);

        Rep = new DB.LinksOwnerCarDepartureRepository(dbContextMock.Object);
    }

    public void Dispose() {}

    [Fact]
    public void TestGetLinksOwnerCarDeparture()
    {
        // Act
        List<BL.LinkOwnerCarDeparture> LinksOwnerCarDeparture = Rep.GetLinksOwnerCarDeparture();

        // Assert
        Assert.Equal(3, dbContextMock.Object.LinksOwnerCarDeparture.Count());
        Assert.Equal(3, LinksOwnerCarDeparture.Count);
    }

    [Fact]
    public void TestGetLinkOwnerCarDepartureByIdCorrect()
    {
        // Act
        BL.LinkOwnerCarDeparture LinkOwnerCarDeparture = Rep.GetLinkOwnerCarDepartureById(1);

        // Assert
        Assert.NotNull(LinkOwnerCarDeparture);
    }

    [Fact]
    public void TestGetLinkOwnerCarDepartureByIdUncorrect()
    {
        // Assert
        Assert.Throws<DB.LinkOwnerCarDepartureNotFoundException>(()=> Rep.GetLinkOwnerCarDepartureById(5));
    }

    [Fact]
    public async Task TestAddLinkOwnerCarDepartureAsyncCorrect()
    {
        var LinkOwnerCarDeparture = new BL.LinkOwnerCarDeparture(4, 4, "Number4", 4);

        // Assert: initial
        Assert.Equal(3, dbContextMock.Object.LinksOwnerCarDeparture.Count());

        // Act
        await Rep.AddLinkOwnerCarDepartureAsync(LinkOwnerCarDeparture);

        // Assert: final
        LinksOwnerCarDepartureDbSetMock.Verify(m => m.Add(It.IsAny<DB.LinkOwnerCarDeparture>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        Assert.Equal(4, dbContextMock.Object.LinksOwnerCarDeparture.Count());
    }

    [Fact]
    public async Task TestAddLinkOwnerCarDepartureAsyncUncorrect()
    {
        var LinkOwnerCarDeparture = new BL.LinkOwnerCarDeparture(4, -4, "Number4", 4);

        // Assert
        await Assert.ThrowsAsync<DB.LinksOwnerCarDepartureValidatorFailException>(()=> Rep.AddLinkOwnerCarDepartureAsync(LinkOwnerCarDeparture));
    }

    [Fact]
    public async Task TestDeleteLinkOwnerCarDepartureAsyncCorrect()
    {
        // Act
        await Rep.DeleteLinkOwnerCarDepartureAsync(3);

        LinksOwnerCarDepartureDbSetMock.Verify(m => m.Remove(It.IsAny<DB.LinkOwnerCarDeparture>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        Assert.Equal(2, dbContextMock.Object.LinksOwnerCarDeparture.Count());
    }

    [Fact]
    public async Task TestDeleteLinkOwnerCarDepartureAsyncUncorrect()
    {
        // Assert
        await Assert.ThrowsAsync<DB.LinkOwnerCarDepartureNotFoundException>(()=> Rep.DeleteLinkOwnerCarDepartureAsync(5));
    }
}