using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using BL;

namespace TestsBL
{
    public class UnitTestsCars
    {
        [Fact]
        public void TestGetCars()
        {
            // RepositoriesFactory
            Mock<BL.ICarsRepository> mockCarsRep = new Mock<BL.ICarsRepository>();
            mockCarsRep.Setup(r => r.GetCars(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns<int, int>((offset, limit) => MockGetCars(offset, limit));
            
            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateCarsRepository() == mockCarsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            List<BL.Car> Cars = facade.GetCars();
            Assert.NotEmpty(Cars);
            Assert.Equal(4, Cars.Count);

            Cars = facade.GetCars(3);
            Assert.NotEmpty(Cars);
            Assert.Single(Cars);

            Cars = facade.GetCars(1, 1);
            Assert.Single(Cars);

            Cars = facade.GetCars(-1, 5);
            Assert.NotEmpty(Cars);
            Assert.Equal(4, Cars.Count);
        }

        private List<BL.Car> MockGetCars(int offset = 0, int limit = -1)
        {
            List<BL.Car> Cars = new List<BL.Car>();
            
            Cars.Add(new BL.Car("Id11", 1, 1, 5, 1));
            Cars.Add(new BL.Car("Id12", 2, 2, 3, 2));
            Cars.Add(new BL.Car("Id13", 1, 2, 5, 3));
            Cars.Add(new BL.Car("Id14", 2, 1, 5, 4));

            List<BL.Car> res = new List<BL.Car>();
            int len = Cars.Count;

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
                res.Add(Cars[i]);
            }

            return res;
        }

        [Fact]
        public void TestGetCarById()
        {
            BL.Car car = new BL.Car("Id11", 1, 1, 5, 1);

            // RepositoriesFactory
            Mock<BL.ICarsRepository> mockCarsRep = new Mock<BL.ICarsRepository>();
            mockCarsRep.Setup(rep => rep.GetCarById(It.IsAny<string>())).Returns(car);

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateCarsRepository() == mockCarsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            BL.Car res = facade.GetCarById("Id11");
            Assert.NotNull(res);
        }

        // [Fact]
        // public void TestAddCar()
        // {
        //     BL.Car car = new BL.Car("Id11", 1, 1, 5, 1);

        //     // RepositoriesFactory
        //     Mock<BL.ICarsRepository> mockCarsRep = new Mock<BL.ICarsRepository>();
        //     mockCarsRep.Setup(rep => rep.AddCar(It.IsAny<BL.Car>())).Verifiable();

        //     BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
        //                                                 f.CreateCarsRepository() == mockCarsRep.Object);

        //     // Facade
        //     BL.Facade facade = new BL.Facade(mockRepFactory);

        //     // Test
        //     facade.AddCar(car);
        //     mockCarsRep.VerifyAll();
        // }

        [Fact]
        public void TestUpdateCar()
        {
            BL.Car car = new BL.Car("Id11", 1, 1, 5, 1);

            // RepositoriesFactory
            Mock<BL.ICarsRepository> mockCarsRep = new Mock<BL.ICarsRepository>();
            mockCarsRep.Setup(rep => rep.GetCarById(It.IsAny<string>())).Returns(car);
            mockCarsRep.Setup(rep => rep.UpdateCar(It.IsAny<string>(), It.IsAny<BL.Car>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateCarsRepository() == mockCarsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.UpdateCar("Id11", car);
            mockCarsRep.VerifyAll();
        }

        // [Fact]
        // public void TestDeleteCar()
        // {
        //     // RepositoriesFactory
        //     Mock<BL.ICarsRepository> mockCarsRep = new Mock<BL.ICarsRepository>();
        //     mockCarsRep.Setup(rep => rep.DeleteCar(It.IsAny<string>())).Verifiable();

        //     BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
        //                                                 f.CreateCarsRepository() == mockCarsRep.Object);

        //     // Facade
        //     BL.Facade facade = new BL.Facade(mockRepFactory);

        //     // Test
        //     facade.DeleteCar("Id11");
        //     mockCarsRep.VerifyAll();
        // }
    }
}