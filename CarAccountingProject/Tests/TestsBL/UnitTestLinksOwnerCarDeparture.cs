using System;
using System.Threading.Tasks;
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
        public async Task TestAddLinkOwnerCarDepartureAsync()
        {
            BL.LinkOwnerCarDeparture LinkOwnerCarDeparture = new BL.LinkOwnerCarDeparture(1, 1, "Id11", 1);

            // RepositoriesFactory
            Mock<BL.ILinksOwnerCarDepartureRepository> mockLinksOwnerCarDepartureRep = new Mock<BL.ILinksOwnerCarDepartureRepository>();
            mockLinksOwnerCarDepartureRep.Setup(rep => rep.AddLinkOwnerCarDepartureAsync(It.IsAny<BL.LinkOwnerCarDeparture>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateLinksOwnerCarDepartureRepository() == mockLinksOwnerCarDepartureRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            await facade.AddLinkOwnerCarDepartureAsync(LinkOwnerCarDeparture);
            mockLinksOwnerCarDepartureRep.VerifyAll();
        }

        [Fact]
        public async Task TestDeleteLinkOwnerCarDepartureAsync()
        {
            // RepositoriesFactory
            Mock<BL.ILinksOwnerCarDepartureRepository> mockLinksOwnerCarDepartureRep = new Mock<BL.ILinksOwnerCarDepartureRepository>();
            mockLinksOwnerCarDepartureRep.Setup(rep => rep.DeleteLinkOwnerCarDepartureAsync(It.IsAny<int>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateLinksOwnerCarDepartureRepository() == mockLinksOwnerCarDepartureRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            await facade.DeleteLinkOwnerCarDepartureAsync(1);
            mockLinksOwnerCarDepartureRep.VerifyAll();
        }
    }
}