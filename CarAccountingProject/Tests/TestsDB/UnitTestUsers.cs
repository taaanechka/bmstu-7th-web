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

public class UnitTestUsers: IDisposable
{
    public DbContextOptions<ApplicationContext> DummyOptions { get; } = new DbContextOptionsBuilder<ApplicationContext>().Options;

    private DbContextMock<ApplicationContext> dbContextMock;
    private DbSetMock<DB.User> UsersDbSetMock;
    private DB.UsersRepository Rep;

    public UnitTestUsers()
    {
        var initialEntities = new[]
        {
            new DB.User {Id = 1, Name ="Name1", Surname = "Surname1", Login = "Login1", Password = "Password1", UserType = 1},
            new DB.User {Id = 2, Name ="Name2", Surname = "Surname2", Login = "Login2", Password = "Password2", UserType = 1},
            new DB.User {Id = 3, Name ="Name3", Surname = "Surname3", Login = "Login3", Password = "Password3", UserType = 1}
        };

        // Arrange
        dbContextMock = new DbContextMock<ApplicationContext>(DummyOptions);
        UsersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Users, initialEntities);

        Rep = new DB.UsersRepository(dbContextMock.Object);
    }

    public void Dispose() {}

    [Fact]
    public void TestGetUsers()
    {
        // Act
        List<BL.User> users = Rep.GetUsers();

        // Assert
        Assert.Equal(3, dbContextMock.Object.Users.Count());
        Assert.Equal(3, users.Count);
    }

    [Fact]
    public void TestGetUserByIdCorrect()
    {
        // Act
        BL.User user = Rep.GetUserById(1);

        // Assert
        Assert.NotNull(user);
    }

    [Fact]
    public void TestGetUserByIdUncorrect()
    {
        // Assert
        Assert.Throws<DB.UserNotFoundException>(()=> Rep.GetUserById(5));
    }

    [Fact]
    public async Task TestAddUserAsyncCorrect()
    {
        var user = new BL.User(4, "Name4", "Surname4", "Login4", "Password4", (BL.Permissions) 1);

        // Assert: initial
        Assert.Equal(3, dbContextMock.Object.Users.Count());

        // Act
        await Rep.AddUserAsync(user);

        // Assert: final
        UsersDbSetMock.Verify(m => m.Add(It.IsAny<DB.User>()), Times.Once());
        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

        Assert.Equal(4, dbContextMock.Object.Users.Count());
    }

    [Fact]
    public async Task TestAddUserAsyncUncorrect()
    {
        var user = new BL.User(4, "Name4", "Surname4", "Login4", "Password4", (BL.Permissions) 4);

        // Assert
        await Assert.ThrowsAsync<DB.UsersValidatorFailException>(()=> Rep.AddUserAsync(user));
    }

    [Fact]
    public async Task TestUpdateUserAsyncCorrect()
    {
        BL.User userUpd = new BL.User(3, "SomeName", "Surname3", "NewLogin", "Password3", (BL.Permissions) 1);

        // Act
        await Rep.UpdateUserAsync(3, userUpd);

        var userNew = Rep.GetUserById(3);

        // Assert
        Assert.Equal("SomeName", userNew.Name);
        Assert.Equal("Surname3", userNew.Surname);
        Assert.Equal("NewLogin", userNew.Login);
        Assert.Equal("Password3", userNew.Password);
        Assert.Equal((BL.Permissions) 1, userNew.UserType);
    }

    // [Fact]
    // public async Task TestUpdateUserAsyncUncorrect()
    // {
    //     BL.User userUpd = new BL.User(3, "SomeName", "Surname3", "NewLogin", "Password3", (BL.Permissions) 4);

    //     // Assert
    //     await Assert.ThrowsAsync<DB.UsersValidatorFailException>(()=> Rep.UpdateUserAsync(3, userUpd));
    // }

    // [Fact]
    // public async Task TestDeleteUserAsyncCorrect()
    // {
    //     // Act
    //     await Rep.BlockUserAsync(3);

    //     UsersDbSetMock.Verify(m => m.Remove(It.IsAny<DB.User>()), Times.Once());
    //     dbContextMock.Verify(m => m.SaveChanges(), Times.Once());

    //     // Assert
    //     // Assert.Equal(2, dbContextMock.Object.Users.Count());
    //     Assert.Equal(1, dbContextMock.Object.Users.Where(u => u.UserType.Equals(0)).Count());
    // }

    [Fact]
    public async Task TestDeleteUserAsyncUncorrect()
    {
        // Assert
        await Assert.ThrowsAsync<DB.UserNotFoundException>(()=> Rep.BlockUserAsync(5));
    }
}