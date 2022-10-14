using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using BL;

namespace TestsBL
{
    public class UnitTestLinksOwnerCarDeparture
    {
        [Fact]
        public void TestGetLinkOwnerCarDepartureById()
        {
            BL.LinkOwnerCarDeparture LinkOwnerCarDeparture = new BL.LinkOwnerCarDeparture(1, 1, "Id11", 1);

            // RepositoriesFactory
            Mock<BL.ILinksOwnerCarDepartureRepository> mockLinksOwnerCarDepartureRep = new Mock<BL.ILinksOwnerCarDepartureRepository>();
            mockLinksOwnerCarDepartureRep.Setup(rep => rep.GetLinkOwnerCarDepartureById(It.IsAny<int>())).Returns(LinkOwnerCarDeparture);

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateLinksOwnerCarDepartureRepository() == mockLinksOwnerCarDepartureRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            BL.LinkOwnerCarDeparture res = facade.GetLinkOwnerCarDepartureById(1);
            Assert.NotNull(res);
        }

        [Fact]
        public void TestAddLinkOwnerCarDeparture()
        {
            BL.LinkOwnerCarDeparture LinkOwnerCarDeparture = new BL.LinkOwnerCarDeparture(1, 1, "Id11", 1);

            // RepositoriesFactory
            Mock<BL.ILinksOwnerCarDepartureRepository> mockLinksOwnerCarDepartureRep = new Mock<BL.ILinksOwnerCarDepartureRepository>();
            mockLinksOwnerCarDepartureRep.Setup(rep => rep.AddLinkOwnerCarDeparture(It.IsAny<BL.LinkOwnerCarDeparture>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateLinksOwnerCarDepartureRepository() == mockLinksOwnerCarDepartureRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.AddLinkOwnerCarDeparture(LinkOwnerCarDeparture);
            mockLinksOwnerCarDepartureRep.VerifyAll();
        }

        [Fact]
        public void TestDeleteLinkOwnerCarDeparture()
        {
            // RepositoriesFactory
            Mock<BL.ILinksOwnerCarDepartureRepository> mockLinksOwnerCarDepartureRep = new Mock<BL.ILinksOwnerCarDepartureRepository>();
            mockLinksOwnerCarDepartureRep.Setup(rep => rep.DeleteLinkOwnerCarDeparture(It.IsAny<int>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateLinksOwnerCarDepartureRepository() == mockLinksOwnerCarDepartureRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.DeleteLinkOwnerCarDeparture(1);
            mockLinksOwnerCarDepartureRep.VerifyAll();
        }
    }
}