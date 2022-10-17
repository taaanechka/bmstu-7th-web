using System;

using BL;

namespace DB
{
    public class RepositoriesFactory: IRepositoriesFactory
    {
        private ApplicationContext db;

        public RepositoriesFactory(ApplicationContext context)
        {
            db = context;
        }

        public IUsersRepository CreateUsersRepository()
        {
            return new UsersRepository(db);
        }

        public IComingsRepository CreateComingsRepository()
        {
            return new ComingsRepository(db);
        }

        public IDeparturesRepository CreateDeparturesRepository()
        {
            return new DeparturesRepository(db);
        }

        public ICarsRepository CreateCarsRepository()
        {
            return new CarsRepository(db);
        }

        public ICarOwnersRepository CreateCarOwnersRepository()
        {
            return new CarOwnersRepository(db);
        }

        public ILinksOwnerCarDepartureRepository CreateLinksOwnerCarDepartureRepository()
        {
            return new LinksOwnerCarDepartureRepository(db);
        }

        public IModelsRepository CreateModelsRepository()
        {
            return new ModelsRepository(db);
        }

        public IBrandsRepository CreateBrandsRepository()
        {
            return new BrandsRepository(db);
        }

        public IEquipmentsRepository CreateEquipmentsRepository()
        {
            return new EquipmentsRepository(db);
        }

        public IColorsRepository CreateColorsRepository()
        {
            return new ColorsRepository(db);
        }

        public IVIPCarOwnersRepository CreateVIPCarOwnersRepository()
        {
            return new VIPCarOwnersRepository(db);
        }
    }
}