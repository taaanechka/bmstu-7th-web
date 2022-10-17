using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using BL;

namespace TestsBL
{
    public class UnitTestModels
    {
        [Fact]
        public void TestGetModelById()
        {
            BL.Model model = new BL.Model(1, 1, "model1");

            // RepositoriesFactory
            Mock<BL.IModelsRepository> mockModelsRep = new Mock<BL.IModelsRepository>();
            mockModelsRep.Setup(rep => rep.GetModelById(It.IsAny<int>())).Returns(model);

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateModelsRepository() == mockModelsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            BL.Model Model = facade.GetModelById(1);
            Assert.NotNull(Model);

            Assert.Equal(1, Model.BrandId);
            Assert.Equal("model1", Model.Name);
        }

        [Fact]
        public void TestAddModel()
        {
            // RepositoriesFactory
            Mock<BL.IModelsRepository> mockModelsRep = new Mock<BL.IModelsRepository>();
            mockModelsRep.Setup(rep => rep.AddModel(It.IsAny<BL.Model>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateModelsRepository() == mockModelsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.AddModel(new BL.Model(1, 1, "model1"));
            mockModelsRep.VerifyAll();
        }

        [Fact]
        public void TestUpdateModel()
        {
            BL.Model model = new BL.Model(1, 1, "Model1");

            // RepositoriesFactory
            Mock<BL.IModelsRepository> mockModelsRep = new Mock<BL.IModelsRepository>();
            mockModelsRep.Setup(rep => rep.GetModelById(It.IsAny<int>())).Returns(model);
            mockModelsRep.Setup(rep => rep.UpdateModel(It.IsAny<int>(), It.IsAny<BL.Model>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateModelsRepository() == mockModelsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.UpdateModel(1, model);
            mockModelsRep.VerifyAll();
        }

        [Fact]
        public void TestDeleteModel()
        {
            // RepositoriesFactory
            Mock<BL.IModelsRepository> mockModelsRep = new Mock<BL.IModelsRepository>();
            mockModelsRep.Setup(rep => rep.DeleteModel(It.IsAny<int>())).Verifiable();

            BL.IRepositoriesFactory mockRepFactory = Mock.Of<BL.IRepositoriesFactory>(f => 
                                                        f.CreateModelsRepository() == mockModelsRep.Object);

            // Facade
            BL.Facade facade = new BL.Facade(mockRepFactory);

            // Test
            facade.DeleteModel(1);
            mockModelsRep.VerifyAll();
        } 
    }
}