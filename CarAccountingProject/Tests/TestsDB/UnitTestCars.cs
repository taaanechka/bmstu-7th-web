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

public class UnitTestCars: IDisposable
{
    public DbContextOptions<ApplicationContext> DummyOptions { get; } = new DbContextOptionsBuilder<ApplicationContext>().Options;

    private DbContextMock<ApplicationContext> dbContextMock;
    private DbSetMock<DB.Car> CarsDbSetMock;
    private DB.CarsRepository Rep;

    public UnitTestCars()
    {
        var initialEntities = new[]
        {
            new DB.Car {Id = "Number1", ModelId = 1, EquipmentId = 1, ColorId = 1, ComingId = 1},
            new DB.Car {Id = "Number2", ModelId = 2, EquipmentId = 2, ColorId = 5, ComingId = 2},
            new DB.Car {Id = "Number3", ModelId = 3, EquipmentId = 1, ColorId = 3, ComingId = 3}
        };

        // Arrange
        dbContextMock = new DbContextMock<ApplicationContext>(DummyOptions);
        CarsDbSetMock = dbContextMock.CreateDbSetMock(x => x.Cars, initialEntities);

        Rep = new DB.CarsRepository(dbContextMock.Object);
    }

    public void Dispose() {}

    [Fact]
    public void TestGetCars()
    {
        // Act
        List<BL.Car> Cars = Rep.GetCars();

        // Assert
        Assert.Equal(3, dbContextMock.Object.Cars.Count());
        Assert.Equal(3, Cars.Count);
    }

    [Fact]
    public void TestGetCarByIdCorrect()
    {
        // Act
        BL.Car Car = Rep.GetCarById("Number1");

        // Assert
        Assert.NotNull(Car);
    }

    [Fact]
    public void TestGetCarByIdUncorrect()
    {
        // Assert
        Assert.Throws<DB.CarNotFoundException>(()=> Rep.GetCarById("Number5"));
    }

    [Fact]
    public void TestAddCarCorrect()
    {
        var Car = new BL.Car("Number4", 5, 3, 4, 4);

        // Assert: initial
        Assert.Equal(3, dbContextMock.Object.Cars.Count());

        // Act
        Rep.AddCar(Car);

        // Assert: final
        CarsDbSetMock.Verify(m => m.Add(It.IsAny<DB.Car>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        Assert.Equal(4, dbContextMock.Object.Cars.Count());
    }

    [Fact]
    public void TestAddCarUncorrect()
    {
        var Car = new BL.Car("Number4", -5, 3, 4, 4);

        // Assert
        Assert.Throws<DB.CarsValidatorFailException>(()=> Rep.AddCar(Car));
    }

    [Fact]
    public void TestUpdateCarCorrect()
    {
        BL.Car CarUpd = new BL.Car("Number3", 5, 3, 4, 4);

        // Act
        Rep.UpdateCar("Number3", CarUpd);

        var CarNew = Rep.GetCarById("Number3");

        // Assert
        Assert.Equal(3, CarNew.EquipmentId);
        Assert.Equal(4, CarNew.ColorId);
    }

    [Fact]
    public void TestUpdateCarUncorrect()
    {
        BL.Car CarUpd = new BL.Car("Number3", 5, -3, 4, 4);

        // Assert
        Assert.Throws<DB.CarsValidatorFailException>(()=> Rep.UpdateCar("Number3", CarUpd));
    }

    [Fact]
    public void TestDeleteCarCorrect()
    {
        // Act
        Rep.DeleteCar("Number3");

        CarsDbSetMock.Verify(m => m.Remove(It.IsAny<DB.Car>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        Assert.Equal(2, dbContextMock.Object.Cars.Count());
    }

    [Fact]
    public void TestDeleteCarUncorrect()
    {
        // Assert
        Assert.Throws<DB.CarNotFoundException>(()=> Rep.DeleteCar("Number5"));
    }
}