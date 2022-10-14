using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using BL;

namespace TestsBL
{
    public class UnitTestDepartures
    {
        [Fact]
        public void TestGetDepartures()
        {
            // RepositoriesFactory
            Mock<BL.IDeparturesRepository> mockDeparturesRep = new Mock<BL.IDeparturesRepository>();
            mockDeparturesRep.Setup(r => r.GetDepartures(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns<int, int>((offset, limit) => MockGetDepartures(offset, limit));

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateDeparturesRepository() == mockDeparturesRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            List<BL.Departure> Departures = facade.GetDepartures();
            Assert.NotEmpty(Departures);

            Departures = facade.GetDepartures(3);
            Assert.NotEmpty(Departures);

            Departures = facade.GetDepartures(1, 2);
            Assert.NotEmpty(Departures);

            Departures = facade.GetDepartures(-1, 5);
            Assert.NotEmpty(Departures);
        }

        private List<BL.Departure> MockGetDepartures(int offset = 0, int limit = -1)
        {
            List<BL.Departure> Departures = new List<BL.Departure>();

            DateTime myDate1 = DateTime.ParseExact("2022-04-10", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);

            DateTime myDate2 = DateTime.ParseExact("2022-04-11", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
        
            Departures.Add(new BL.Departure(1, 1, myDate1));
            Departures.Add(new BL.Departure(2, 2, myDate2));
            Departures.Add(new BL.Departure(3, 3, myDate2));
            Departures.Add(new BL.Departure(4, 4, myDate2));

            List<BL.Departure> res = new List<BL.Departure>();
            int len = Departures.Count;

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
                res.Add(Departures[i]);
            }

            return res;
        }

        [Fact]
        public void TestGetDepartureById()
        {
            DateTime myDate1 = DateTime.ParseExact("2022-04-10", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
            BL.Departure departure = new BL.Departure(1, 1, myDate1);

            // RepositoriesFactory
            Mock<BL.IDeparturesRepository> mockDeparturesRep = new Mock<BL.IDeparturesRepository>();
            mockDeparturesRep.Setup(rep => rep.GetDepartureById(It.IsAny<int>())).Returns(departure);

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateDeparturesRepository() == mockDeparturesRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            BL.Departure res = facade.GetDepartureById(1);
            Assert.NotNull(res);
        }

        [Fact]
        public void TestAddDeparture()
        {
            BL.LinkOwnerCarDeparture LinkOwnerCarDeparture = new BL.LinkOwnerCarDeparture(1, 1, "Id11", 1);

            DateTime myDate1 = DateTime.ParseExact("2022-04-10", "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
            BL.Departure departure = new BL.Departure(1, 1, myDate1);

            // RepositoriesFactory
            Mock<BL.IDeparturesRepository> mockDeparturesRep = new Mock<BL.IDeparturesRepository>();
            mockDeparturesRep.Setup(rep => rep.AddDeparture(It.IsAny<BL.Departure>(), It.IsAny<BL.LinkOwnerCarDeparture>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateDeparturesRepository() == mockDeparturesRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.AddDeparture(departure, LinkOwnerCarDeparture);
            mockDeparturesRep.VerifyAll();
        }

        [Fact]
        public void TestDeleteDeparture()
        {
            // RepositoriesFactory
            Mock<BL.IDeparturesRepository> mockDeparturesRep = new Mock<BL.IDeparturesRepository>();
            mockDeparturesRep.Setup(rep => rep.DeleteDeparture(It.IsAny<int>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateDeparturesRepository() == mockDeparturesRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.DeleteDeparture(1);
            mockDeparturesRep.VerifyAll();
        }
    }
}