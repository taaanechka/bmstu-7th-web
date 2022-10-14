namespace BL
{
    public interface IRepositoriesFactory
    {
        public IUsersRepository CreateUsersRepository();
        public IComingsRepository CreateComingsRepository();
        public IDeparturesRepository CreateDeparturesRepository();
        public ICarsRepository CreateCarsRepository();
        public ICarOwnersRepository CreateCarOwnersRepository();
        public ILinksOwnerCarDepartureRepository CreateLinksOwnerCarDepartureRepository();
        public IModelsRepository CreateModelsRepository();
        public IBrandsRepository CreateBrandsRepository();
        public IEquipmentsRepository CreateEquipmentsRepository();
        public IColorsRepository CreateColorsRepository();
        public IVIPCarOwnersRepository CreateVIPCarOwnersRepository();
    }
}