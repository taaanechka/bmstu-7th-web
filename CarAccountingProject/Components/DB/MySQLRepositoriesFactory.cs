using System;

using BL;

namespace DB
{
    public class MySQLRepositoriesFactory: IRepositoriesFactory
    {
        private MySQLApplicationContext db;

        public MySQLRepositoriesFactory(MySQLApplicationContext context)
        {
            db = context;
        }

        public IUsersRepository CreateUsersRepository()
        {
            return new MySQLUsersRepository(db);
        }

        public IComingsRepository CreateComingsRepository()
        {
            return new MySQLComingsRepository(db);
        }

        public IDeparturesRepository CreateDeparturesRepository()
        {
            return new MySQLDeparturesRepository(db);
        }

        public ICarsRepository CreateCarsRepository()
        {
            return new MySQLCarsRepository(db);
        }

        public ICarOwnersRepository CreateCarOwnersRepository()
        {
            return new MySQLCarOwnersRepository(db);
        }

        public ILinksOwnerCarDepartureRepository CreateLinksOwnerCarDepartureRepository()
        {
            return new MySQLLinksOwnerCarDepartureRepository(db);
        }

        public IModelsRepository CreateModelsRepository()
        {
            return new MySQLModelsRepository(db);
        }

        public IBrandsRepository CreateBrandsRepository()
        {
            return new MySQLBrandsRepository(db);
        }

        public IEquipmentsRepository CreateEquipmentsRepository()
        {
            return new MySQLEquipmentsRepository(db);
        }

        public IColorsRepository CreateColorsRepository()
        {
            return new MySQLColorsRepository(db);
        }

        public IVIPCarOwnersRepository CreateVIPCarOwnersRepository()
        {
            return new MySQLVIPCarOwnersRepository(db);
        }
    }
}