using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using BL;

namespace TestsBL
{
    public class UnitTestComings
    {
        [Fact]
        public void TestGetComings()
        {
            // RepositoriesFactory
            Mock<BL.IComingsRepository> mockComingsRep = new Mock<BL.IComingsRepository>();
            mockComingsRep.Setup(r => r.GetComings(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns<int, int>((offset, limit) => MockGetComings(offset, limit));
            
            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateComingsRepository() == mockComingsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            List<BL.Coming> Comings = facade.GetComings();
            Assert.NotEmpty(Comings);

            Comings = facade.GetComings(3);
            Assert.NotEmpty(Comings);

            Comings = facade.GetComings(1, 1);
            Assert.Single(Comings);

            Comings = facade.GetComings(-1, 5);
            Assert.NotEmpty(Comings);
        }

        private List<BL.Coming> MockGetComings(int offset = 0, int limit = -1)
        {
            List<BL.Coming> Comings = new List<BL.Coming>();

            DateTime myDate1 = DateTime.ParseExact("2022-04-10", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);

            DateTime myDate2 = DateTime.ParseExact("2022-04-11", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
        
            Comings.Add(new BL.Coming(1, 1, myDate1));
            Comings.Add(new BL.Coming(2, 2, myDate2));
            Comings.Add(new BL.Coming(3, 3, myDate2));
            Comings.Add(new BL.Coming(4, 4, myDate2));

            List<BL.Coming> res = new List<BL.Coming>();
            int len = Comings.Count;

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
                res.Add(Comings[i]);
            }

            return res;
        }

        [Fact]
        public void TestGetComingById()
        {
            DateTime myDate1 = DateTime.ParseExact("2022-04-10", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
            BL.Coming coming = new BL.Coming(1, 1, myDate1);

            // RepositoriesFactory
            Mock<BL.IComingsRepository> mockComingsRep = new Mock<BL.IComingsRepository>();
            mockComingsRep.Setup(rep => rep.GetComingById(It.IsAny<int>())).Returns(coming);

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateComingsRepository() == mockComingsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            BL.Coming res = facade.GetComingById(1);
            Assert.NotNull(res);
        }

        [Fact]
        public void TestAddComing()
        {
            BL.Car car = new BL.Car("Id11", 1, 1, 5, 1);

            // RepositoriesFactory
            Mock<BL.IComingsRepository> mockComingsRep = new Mock<BL.IComingsRepository>();
            mockComingsRep.Setup(rep => rep.AddComing(It.IsAny<BL.Coming>(), It.IsAny<BL.Car>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateComingsRepository() == mockComingsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            DateTime myDate1 = DateTime.ParseExact("2022-04-10", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);

            facade.AddComing(new BL.Coming(1, 1, myDate1), car);
            mockComingsRep.VerifyAll();
        }

        [Fact]
        public void TestDeleteComing()
        {
            // RepositoriesFactory
            Mock<BL.IComingsRepository> mockComingsRep = new Mock<BL.IComingsRepository>();
            mockComingsRep.Setup(rep => rep.DeleteComing(It.IsAny<int>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateComingsRepository() == mockComingsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.DeleteComing(1);
            mockComingsRep.VerifyAll();
        }
    }
}