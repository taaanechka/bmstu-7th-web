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

public class UnitTestModels: IDisposable
{
    public DbContextOptions<ApplicationContext> DummyOptions { get; } = new DbContextOptionsBuilder<ApplicationContext>().Options;

    private DbContextMock<ApplicationContext> dbContextMock;
    private DbSetMock<DB.Model> ModelsDbSetMock;
    private DB.ModelsRepository Rep;

    public UnitTestModels()
    {
        var initialEntities = new[]
        {
            new DB.Model {Id = 1, BrandId = 1, Name ="Name1"},
            new DB.Model {Id = 2, BrandId = 1, Name ="Name2"},
            new DB.Model {Id = 3, BrandId = 2, Name ="Name3"}
        };

        // Arrange
        dbContextMock = new DbContextMock<ApplicationContext>(DummyOptions);
        ModelsDbSetMock = dbContextMock.CreateDbSetMock(x => x.Models, initialEntities);

        Rep = new DB.ModelsRepository(dbContextMock.Object);
    }

    public void Dispose() {}

    [Fact]
    public void TestGetModels()
    {
        // Act
        List<BL.Model> Models = Rep.GetModels();

        // Assert
        Assert.Equal(3, dbContextMock.Object.Models.Count());
        Assert.Equal(3, Models.Count);
    }

    [Fact]
    public void TestGetModelByIdCorrect()
    {
        // Act
        BL.Model Model = Rep.GetModelById(1);

        // Assert
        Assert.NotNull(Model);
    }

    [Fact]
    public void TestGetModelByIdUncorrect()
    {
        // Assert
        Assert.Throws<DB.ModelNotFoundException>(()=> Rep.GetModelById(5));
    }

    [Fact]
    public void TestAddModelCorrect()
    {        
        var Model = new BL.Model(4, 3, "Name4");

        // Assert: initial
        Assert.Equal(3, dbContextMock.Object.Models.Count());

        // Act
        Rep.AddModel(Model);

        // Assert: final
        ModelsDbSetMock.Verify(m => m.Add(It.IsAny<DB.Model>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        Assert.Equal(4, dbContextMock.Object.Models.Count());
    }

    [Fact]
    public void TestAddModelUncorrect()
    {
        var Model = new BL.Model(4, -3, "Name4");

        // Assert
        Assert.Throws<DB.ModelsValidatorFailException>(()=> Rep.AddModel(Model));
    }

    [Fact]
    public void TestUpdateModelCorrect()
    {
        var ModelUpd = new BL.Model(3, 2, "SomeName");

        // Act
        Rep.UpdateModel(3, ModelUpd);

        var ModelNew = Rep.GetModelById(3);

        // Assert
        Assert.Equal(2, ModelNew.BrandId);
        Assert.Equal("SomeName", ModelNew.Name);
    }

    [Fact]
    public void TestUpdateModelUncorrect()
    {
        var ModelUpd = new BL.Model(3, -2, "SomeName");

        // Assert
        Assert.Throws<DB.ModelsValidatorFailException>(()=> Rep.UpdateModel(3, ModelUpd));
    }

    [Fact]
    public void TestDeleteModelCorrect()
    {
        // Act
        Rep.DeleteModel(3);

        ModelsDbSetMock.Verify(m => m.Remove(It.IsAny<DB.Model>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        // Assert
        Assert.Equal(2, dbContextMock.Object.Models.Count());
    }

    [Fact]
    public void TestDeleteModelUncorrect()
    {
        // Assert
        Assert.Throws<DB.ModelNotFoundException>(()=> Rep.DeleteModel(5));
    }
}