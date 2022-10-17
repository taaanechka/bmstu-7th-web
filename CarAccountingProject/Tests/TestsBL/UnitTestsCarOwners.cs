using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using BL;

namespace TestsBL
{
    public class UnitTestsCarOwners
    {
        [Fact]
        public void TestGetCarOwners()
        {
            // RepositoriesFactory
            Mock<BL.ICarOwnersRepository> mockCarOwnersRep = new Mock<BL.ICarOwnersRepository>();
            mockCarOwnersRep.Setup(r => r.GetCarOwners(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns<int, int>((offset, limit) => MockGetCarOwners(offset, limit));

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateCarOwnersRepository() == mockCarOwnersRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            List<BL.CarOwner> CarOwners = facade.GetCarOwners();
            Assert.NotEmpty(CarOwners);
            Assert.Equal(4, CarOwners.Count);

            CarOwners = facade.GetCarOwners(3);
            Assert.NotEmpty(CarOwners);
            Assert.Single(CarOwners);

            CarOwners = facade.GetCarOwners(1, 1);
            Assert.Single(CarOwners);

            CarOwners = facade.GetCarOwners(-1, 5);
            Assert.NotEmpty(CarOwners);
            Assert.Equal(4, CarOwners.Count);
        }

        private List<BL.CarOwner> MockGetCarOwners(int offset = 0, int limit = -1)
        {
            List<BL.CarOwner> CarOwners = new List<BL.CarOwner>();
        
            CarOwners.Add(new CarOwner(1, "Owner1", "Surname1", "email11"));
            CarOwners.Add(new CarOwner(2, "Owner2", "Surname2", "email12"));
            CarOwners.Add(new CarOwner(3, "Owner3", "Surname3", "email13"));
            CarOwners.Add(new CarOwner(4, "Owner4", "Surname4", "email14"));

            List<BL.CarOwner> res = new List<BL.CarOwner>();
            int len = CarOwners.Count;

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
                res.Add(CarOwners[i]);
            }

            return res;
        }

        [Fact]
        public void TestGetCarById()
        {
            BL.CarOwner owner = new BL.CarOwner(1, "Owner1", "Surname1", "Email1");

            // RepositoriesFactory
            Mock<BL.ICarOwnersRepository> mockCarOwnersRep = new Mock<BL.ICarOwnersRepository>();
            mockCarOwnersRep.Setup(rep => rep.GetCarOwnerById(It.IsAny<int>())).Returns(owner);

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateCarOwnersRepository() == mockCarOwnersRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            BL.CarOwner res = facade.GetCarOwnerById(1);
            Assert.NotNull(res);
        }

        [Fact]
        public void TestAddCarOwner()
        {
            BL.CarOwner owner = new BL.CarOwner(1, "Owner1", "Surname1", "Email1");

            // RepositoriesFactory
            Mock<BL.ICarOwnersRepository> mockCarOwnersRep = new Mock<BL.ICarOwnersRepository>();
            mockCarOwnersRep.Setup(rep => rep.AddCarOwner(It.IsAny<BL.CarOwner>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateCarOwnersRepository() == mockCarOwnersRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.AddCarOwner(owner);
            mockCarOwnersRep.VerifyAll();
        }

        [Fact]
        public void TestUpdateCarOwner()
        {
            BL.CarOwner owner = new BL.CarOwner(1, "Owner1", "Surname1", "Email1");

            // RepositoriesFactory
            Mock<BL.ICarOwnersRepository> mockCarOwnersRep = new Mock<BL.ICarOwnersRepository>();
            mockCarOwnersRep.Setup(rep => rep.GetCarOwnerById(It.IsAny<int>())).Returns(owner);
            mockCarOwnersRep.Setup(rep => rep.UpdateCarOwner(It.IsAny<int>(), It.IsAny<BL.CarOwner>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateCarOwnersRepository() == mockCarOwnersRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.UpdateCarOwner(1, owner);
            mockCarOwnersRep.VerifyAll();
        }

        [Fact]
        public void TestDeleteCarOwner()
        {
            // RepositoriesFactory
            Mock<BL.ICarOwnersRepository> mockCarOwnersRep = new Mock<BL.ICarOwnersRepository>();
            mockCarOwnersRep.Setup(rep => rep.DeleteCarOwner(It.IsAny<int>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateCarOwnersRepository() == mockCarOwnersRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.DeleteCarOwner(1);
            mockCarOwnersRep.VerifyAll();
        }
    }
}