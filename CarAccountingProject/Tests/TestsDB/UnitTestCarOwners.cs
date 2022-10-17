using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using EntityFrameworkCoreMock;

using Xunit;
using Xunit.Abstractions;
using Moq;

using DB;
using BL;

namespace TestsDB;

public class UnitTestCarOwners: IDisposable
{
    public DbContextOptions<ApplicationContext> DummyOptions { get; } = new DbContextOptionsBuilder<ApplicationContext>().Options;

    private DbContextMock<ApplicationContext> dbContextMock;
    private DbSetMock<DB.CarOwner> CarOwnersDbSetMock;
    private DB.CarOwnersRepository Rep;

    public UnitTestCarOwners()
    {
        var initialEntities = new[]
        {
            new DB.CarOwner {Id = 1, Name ="Name1", Surname = "Surname1", Email = "Email1"},
            new DB.CarOwner {Id = 2, Name ="Name2", Surname = "Surname2", Email = "Email2"},
            new DB.CarOwner {Id = 3, Name ="Name3", Surname = "Surname3", Email = "Email3"}
        };

        // Arrange
        dbContextMock = new DbContextMock<ApplicationContext>(DummyOptions);
        CarOwnersDbSetMock = dbContextMock.CreateDbSetMock(x => x.CarOwners, initialEntities);

        Rep = new DB.CarOwnersRepository(dbContextMock.Object);
    }

    public void Dispose() {}

    [Fact]
    public void TestGetCarOwners()
    {
        // Act
        List<BL.CarOwner> CarOwners = Rep.GetCarOwners();

        // Assert
        Assert.Equal(3, dbContextMock.Object.CarOwners.Count());
        Assert.Equal(3, CarOwners.Count);
    }

    [Fact]
    public void TestGetCarOwnerByIdCorrect()
    {
        // Act
        BL.CarOwner CarOwner = Rep.GetCarOwnerById(1);

        // Assert
        Assert.NotNull(CarOwner);
    }

    [Fact]
    public void TestGetCarOwnerByIdUncorrect()
    {
        // Assert
        Assert.Throws<DB.CarOwnerNotFoundException>(()=> Rep.GetCarOwnerById(5));
    }

    [Fact]
    public void TestAddCarOwnerCorrect()
    {
        var CarOwner = new BL.CarOwner(4, "Name4", "Surname4", "Email4");

        // Assert: initial
        Assert.Equal(3, dbContextMock.Object.CarOwners.Count());

        // Act
        Rep.AddCarOwner(CarOwner);

        // Assert: final
        CarOwnersDbSetMock.Verify(m => m.Add(It.IsAny<DB.CarOwner>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        Assert.Equal(4, dbContextMock.Object.CarOwners.Count());
    }

    [Fact]
    public void TestAddCarOwnerUncorrect()
    {
        var CarOwner = new BL.CarOwner(4, "Name4", "Surname4", "");

        // Assert
        Assert.Throws<DB.CarOwnersValidatorFailException>(()=> Rep.AddCarOwner(CarOwner));
    }

    [Fact]
    public void TestUpdateCarOwnerCorrect()
    {
        BL.CarOwner CarOwnerUpd = new BL.CarOwner(3, "SomeName", "Surname3", "NewEmail");

        // Act
        Rep.UpdateCarOwner(3, CarOwnerUpd);

        var CarOwnerNew = Rep.GetCarOwnerById(3);

        // Assert
        Assert.Equal("SomeName", CarOwnerNew.Name);
        Assert.Equal("Surname3", CarOwnerNew.Surname);
        Assert.Equal("NewEmail", CarOwnerNew.Email);
    }

    [Fact]
    public void TestUpdateCarOwnerUncorrect()
    {
        BL.CarOwner CarOwnerUpd = new BL.CarOwner(3, "SomeName", "Surname3", "");

        // Assert
        Assert.Throws<DB.CarOwnersValidatorFailException>(()=> Rep.UpdateCarOwner(3, CarOwnerUpd));
    }

    [Fact]
    public void TestDeleteCarOwnerCorrect()
    {
        // Act
        Rep.DeleteCarOwner(3);

        CarOwnersDbSetMock.Verify(m => m.Remove(It.IsAny<DB.CarOwner>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        Assert.Equal(2, dbContextMock.Object.CarOwners.Count());
    }

    [Fact]
    public void TestDeleteCarOwnerUncorrect()
    {
        // Assert
        Assert.Throws<DB.CarOwnerNotFoundException>(()=> Rep.DeleteCarOwner(5));
    }
}