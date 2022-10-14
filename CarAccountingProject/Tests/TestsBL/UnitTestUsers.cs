using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using BL;

namespace TestsBL;

public class UnitTestUsers
{
    [Fact]
    public void TestGetUsers()
    {
        // RepositoriesFactory
        Mock<BL.IUsersRepository> mockUsersRep = new Mock<BL.IUsersRepository>();
        mockUsersRep.Setup(r => r.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns<int, int>((offset, limit) => MockGetUsers(offset, limit));

        BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                    f.CreateUsersRepository() == mockUsersRep.Object);

        // Facade
        BL.Facade facade = new BL.Facade(mockRepFactory);

        // Test
        List<BL.User> users = facade.GetUsers(0, 2);
        Assert.NotNull(users);
        Assert.Equal(2, users.Count);

        users = facade.GetUsers(0, 5);
        Assert.NotNull(users);
        Assert.Equal(4, users.Count);

        users = facade.GetUsers(1, 3);
        Assert.NotNull(users);
        Assert.Equal(3, users.Count);

        users = facade.GetUsers(-1, 3);
        Assert.NotNull(users);
        Assert.Equal(3, users.Count);
    }

    private List<BL.User> MockGetUsers(int offset = 0, int limit = -1)
    {
        List<BL.User> users = new List<BL.User>();
    
        users.Add(new BL.User(1, "Name1", "Surname1", "Login1", "Password1", (BL.Permissions) 3));
        users.Add(new BL.User(2, "Name2", "Surname2", "Login2", "Password2", (BL.Permissions) 2));
        users.Add(new BL.User(3, "Name3", "Surname3", "Login3", "Password3", (BL.Permissions) 2));
        users.Add(new BL.User(4, "Name4", "Surname4", "Login4", "Password4", (BL.Permissions) 1));

        List<BL.User> res = new List<BL.User>();
        int len = users.Count;

        if (offset < 0)
        {
            offset = 0;
        }

        int resCount = offset + limit;

        if (limit == -1 || len < resCount)
        {
            resCount = len;
        }

        for (int i = offset; i < resCount; i++)
        {
            res.Add(users[i]);
        }

        return res;
    }

    [Fact]
    public void TestGetUserById()
    {
        BL.User user = new BL.User(1, "Name1", "Surname1", "Login1", "Password1", (BL.Permissions) 2);

        // RepositoriesFactory
        Mock<BL.IUsersRepository> mockUsersRep = new Mock<BL.IUsersRepository>();
        mockUsersRep.Setup(rep => rep.GetUserById(It.IsAny<int>())).Returns(user);

        BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                    f.CreateUsersRepository() == mockUsersRep.Object);

        // Facade
        BL.Facade facade = new BL.Facade(mockRepFactory);

        // Test
        BL.User res = facade.GetUserById(1);
        Assert.NotNull(res);

        Assert.Equal("Name1", res.Name);
        Assert.Equal("Surname1", res.Surname);
        Assert.Equal("Login1", res.Login);
        Assert.Equal("Password1", res.Password);
        Assert.Equal((BL.Permissions) 2, res.UserType);
    }

    [Fact]
    public void TestGetUserByLogin()
    {
        BL.User user = new BL.User(1, "Name1", "Surname1", "Login1", "Password1", (BL.Permissions) 2);

        // RepositoriesFactory
        Mock<BL.IUsersRepository> mockUsersRep = new Mock<BL.IUsersRepository>();
        mockUsersRep.Setup(rep => rep.GetUserByLogin(It.IsAny<string>())).Returns(user);

        BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                    f.CreateUsersRepository() == mockUsersRep.Object);

        // Facade
        BL.Facade facade = new BL.Facade(mockRepFactory);

        // Test
        BL.User res = facade.GetUserByLogin("Login1");
        Assert.NotNull(res);

        Assert.Equal("Name1", user.Name);
        Assert.Equal("Surname1", user.Surname);
        Assert.Equal("Login1", user.Login);
        Assert.Equal("Password1", user.Password);
        Assert.Equal((BL.Permissions) 2, user.UserType);
    }

    [Fact]
    public void TestAddUser()
    {
        // RepositoriesFactory
        Mock<BL.IUsersRepository> mockUsersRep = new Mock<BL.IUsersRepository>();
        mockUsersRep.Setup(rep => rep.AddUser(It.IsAny<BL.User>())).Verifiable();

        BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                    f.CreateUsersRepository() == mockUsersRep.Object);

        // Facade
        BL.Facade facade = new BL.Facade(mockRepFactory);

        // Test
        facade.AddUser(new BL.User(1, "Name1", "Surname1", "Login1", "Password1", (BL.Permissions) 3));
        mockUsersRep.VerifyAll();
    }

    [Fact]
    public void TestUpdateUser()
    {
        BL.User user = new BL.User(1, "Name1", "Surname1", "Login1", "Password1", (BL.Permissions) 2);

        // RepositoriesFactory
        Mock<BL.IUsersRepository> mockUsersRep = new Mock<BL.IUsersRepository>();
        mockUsersRep.Setup(rep => rep.GetUserById(It.IsAny<int>())).Returns(user);
        mockUsersRep.Setup(rep => rep.UpdateUser(It.IsAny<int>(), It.IsAny<BL.User>())).Verifiable();

        BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                    f.CreateUsersRepository() == mockUsersRep.Object);

        // Facade
        BL.Facade facade = new BL.Facade(mockRepFactory);

        // Test
        facade.UpdateUser(1, user);
        mockUsersRep.VerifyAll();
    }

    [Fact]
    public void TestBlockUser()
    {
        // RepositoriesFactory
        Mock<BL.IUsersRepository> mockUsersRep = new Mock<BL.IUsersRepository>();
        mockUsersRep.Setup(rep => rep.BlockUser(It.IsAny<int>())).Verifiable();

        BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                    f.CreateUsersRepository() == mockUsersRep.Object);

        // Facade
        BL.Facade facade = new BL.Facade(mockRepFactory);

        // Test
        facade.BlockUser(1);
        mockUsersRep.VerifyAll();
    }    
}
