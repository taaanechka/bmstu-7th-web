using System;
using System.Collections.Generic;

#nullable disable

namespace BL
{
    public class Facade
    {
        private readonly IRepositoriesFactory RepositoriesFactory;

        public Facade(IRepositoriesFactory repFactory)
        {
            if (repFactory == null)
            {
                throw new ArgumentNullException(nameof(repFactory));
            }

            RepositoriesFactory = repFactory;
        }

        // !Users
        public User LogIn(string login, string password)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                User user = userRep.GetUserByLogin(login);
                UsersValidator.ValidateUser(user);

                if ((int)user.UserType < 1)
                {
                    throw new AccessPermissionsException();
                }

                if (password != user.Password)
                {
                    throw new AuthorizationFailException();
                }
                
                return user;
            }
            catch (AuthorizationFailException)
            {
                throw new UserNotFoundException();

                // if (exc.GetType().IsAssignableFrom(typeof(UsersValidatorFailException)))
                // {
                //     throw new UsersValidatorFailException("Invalid Format.", exc.InnerException);
                // }
                // else
                // {
                //     throw new UserNotFoundException();
                // }
            }
        }

        public User LogOut(string login)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                User user = userRep.GetUserByLogin(login);
                UsersValidator.ValidateUser(user);

                return user;
            }
            catch (System.Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(UsersValidatorFailException)))
                {
                    throw new UsersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
            
        }
        
        public List<User> GetUsers(int offset = 0, int limit = -1)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                List<User> res = userRep.GetUsers(offset, limit);

                foreach (var elem in  res)
                {
                    UsersValidator.ValidateUser(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(UsersValidatorFailException)))
                {
                    throw new UsersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }       
        }

        public List<User> GetUsersByName(string name)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                List<User> res = userRep.GetUsersByName(name);

                foreach (var elem in  res)
                {
                    UsersValidator.ValidateUser(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(UsersValidatorFailException)))
                {
                    throw new UsersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
        }

        public List<User> GetUsersBySurname(string surname)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                List<User> res = userRep.GetUsersBySurname(surname);

                foreach (var elem in  res)
                {
                    UsersValidator.ValidateUser(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(UsersValidatorFailException)))
                {
                    throw new UsersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
        }

        public List<User> GetUsersByUserType(int userType)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                List<User> res = userRep.GetUsersByUserType(userType);

                foreach (var elem in  res)
                {
                    UsersValidator.ValidateUser(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(UsersValidatorFailException)))
                {
                    throw new UsersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
        }

        public User GetUserByLogin(string login)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                User res = userRep.GetUserByLogin(login);
                UsersValidator.ValidateUser(res);
            
                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(UsersValidatorFailException)))
                {
                    throw new UsersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
        }

        public User GetUserById(int id)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                User res = userRep.GetUserById(id);
                UsersValidator.ValidateUser(res);
            
                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(UsersValidatorFailException)))
                {
                    throw new UsersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
        }

        public int AddUser(User user)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                UsersValidator.ValidateUser(user);
                userRep.AddUser(user);
            
                return 0;
            }
            catch (Exception)
            {
                throw new UserAddException();
            }
        }

        public int UpdateUser(int id, User newUser)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                var user = userRep.GetUserById(id);
                var userTmp = new User(user.Id, newUser.Name, newUser.Surname, newUser.Login, newUser.Password, newUser.UserType);
                UsersValidator.ValidateUser(userTmp);

                Console.WriteLine("Validated UserBL");

                userRep.UpdateUser(id, newUser);

                Console.WriteLine("Updated UserDB");
            
                return 0;
            }
            catch (Exception)
            {
                throw new UserUpdateException();
            }
        }

        public int BlockUser(int id)
        {
            IUsersRepository userRep = RepositoriesFactory.CreateUsersRepository();
            if (userRep == null)
            {
                throw new UsersRepositoryNullException("RepositoriesFactory.CreateUsersRepository() Error.");
            }

            try
            {
                userRep.BlockUser(id);
            
                return 0;
            }
            catch (Exception)
            {
                throw new UserBlockException();
            }
        }

        // !Comings
        public List<Coming> GetComings(int offset = 0, int limit = -1)
        {
            IComingsRepository comRep = RepositoriesFactory.CreateComingsRepository();
            if (comRep == null)
            {
                throw new ComingsRepositoryNullException("RepositoriesFactory.CreateComingsRepository() Error.");
            }

            try
            {
                List<Coming> res = comRep.GetComings(offset, limit);

                foreach (var elem in  res)
                {
                    ComingsValidator.ValidateComing(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ComingsValidatorFailException)))
                {
                    throw new ComingsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ComingNotFoundException();
                }
            }
        }

        public List<Coming> GetComingsByDate(DateTime date)
        {
            IComingsRepository comRep = RepositoriesFactory.CreateComingsRepository();
            if (comRep == null)
            {
                throw new ComingsRepositoryNullException("RepositoriesFactory.CreateComingsRepository() Error.");
            }

            try
            {
                List<Coming> res = comRep.GetComingsByDate(date);

                foreach (var elem in  res)
                {
                    ComingsValidator.ValidateComing(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ComingsValidatorFailException)))
                {
                    throw new ComingsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ComingNotFoundException();
                }
            }
        }

        public List<Coming> GetComingsBetweenDates(DateTime date1, DateTime date2)
        {
            IComingsRepository comRep = RepositoriesFactory.CreateComingsRepository();
            if (comRep == null)
            {
                throw new ComingsRepositoryNullException("RepositoriesFactory.CreateComingsRepository() Error.");
            }

            try
            {
                List<Coming> res = comRep.GetComingsBetweenDates(date1, date2);

                foreach (var elem in  res)
                {
                    ComingsValidator.ValidateComing(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ComingsValidatorFailException)))
                {
                    throw new ComingsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ComingNotFoundException();
                }
            }
        }

        public List<Coming> GetComingsByUserId(int uId)
        {
            IComingsRepository comRep = RepositoriesFactory.CreateComingsRepository();
            if (comRep == null)
            {
                throw new ComingsRepositoryNullException("RepositoriesFactory.CreateComingsRepository() Error.");
            }

            try
            {
                List<Coming> res = comRep.GetComingsByUserId(uId);

                foreach (var elem in  res)
                {
                    ComingsValidator.ValidateComing(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ComingsValidatorFailException)))
                {
                    throw new ComingsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ComingNotFoundException();
                }
            }
        }

        public Coming GetComingById(int id)
        {
            IComingsRepository comRep = RepositoriesFactory.CreateComingsRepository();
            if (comRep == null)
            {
                throw new ComingsRepositoryNullException("RepositoriesFactory.CreateComingsRepository() Error.");
            }

            try
            {
                Coming res = comRep.GetComingById(id);
                ComingsValidator.ValidateComing(res);
            
               return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ComingsValidatorFailException)))
                {
                    throw new ComingsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ComingNotFoundException();
                }
            }
        }

        public int AddComing(Coming com, Car car)
        {
            IComingsRepository comRep = RepositoriesFactory.CreateComingsRepository();
            if (comRep == null)
            {
                throw new ComingsRepositoryNullException("RepositoriesFactory.CreateComingsRepository() Error.");
            }

            try
            {
                CarsValidator.ValidateCar(car);
                ComingsValidator.ValidateComing(com);
                comRep.AddComing(com, car);
            
               return 0;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ComingsValidatorFailException)))
                {
                    throw new ComingsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else if (exc.GetType().IsAssignableFrom(typeof(CarsValidatorFailException)))
                {
                    throw new CarsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ComingAddException("ADD error.", exc);
                }
            }
        }

        public int DeleteComing(int id)
        {
            IComingsRepository comRep = RepositoriesFactory.CreateComingsRepository();
            if (comRep == null)
            {
                throw new ComingsRepositoryNullException("RepositoriesFactory.CreateComingsRepository() Error.");
            }

            try
            {
                comRep.DeleteComing(id);
            
               return 0;
            }
            catch (Exception)
            {
                throw new ComingDeleteException();
            }
        }


        // !Departures
        public List<Departure> GetDepartures(int offset = 0, int limit = -1)
        {
            IDeparturesRepository depRep = RepositoriesFactory.CreateDeparturesRepository();
            if (depRep == null)
            {
                throw new DeparturesRepositoryNullException("RepositoriesFactory.CreateDeparturesRepository() Error.");
            }

            try
            {
                List<Departure> res = depRep.GetDepartures(offset, limit);

                foreach (var elem in  res)
                {
                    DeparturesValidator.ValidateDeparture(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(DeparturesValidatorFailException)))
                {
                    throw new DeparturesValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new DepartureNotFoundException();
                }
            }
        }

        public List<Departure> GetDeparturesByDate(DateTime date)
        {
            IDeparturesRepository depRep = RepositoriesFactory.CreateDeparturesRepository();
            if (depRep == null)
            {
                throw new DeparturesRepositoryNullException("RepositoriesFactory.CreateDeparturesRepository() Error.");
            }

            try
            {
                List<Departure> res = depRep.GetDeparturesByDate(date);

                foreach (var elem in  res)
                {
                    DeparturesValidator.ValidateDeparture(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(DeparturesValidatorFailException)))
                {
                    throw new DeparturesValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new DepartureNotFoundException();
                }
            }
        }

        public List<Departure> GetDeparturesBetweenDates(DateTime date1, DateTime date2)
        {
            IDeparturesRepository depRep = RepositoriesFactory.CreateDeparturesRepository();
            if (depRep == null)
            {
                throw new DeparturesRepositoryNullException("RepositoriesFactory.CreateDeparturesRepository() Error.");
            }

            try
            {
                List<Departure> res = depRep.GetDeparturesBetweenDates(date1, date2);

                foreach (var elem in  res)
                {
                    DeparturesValidator.ValidateDeparture(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(DeparturesValidatorFailException)))
                {
                    throw new DeparturesValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new DepartureNotFoundException();
                }
            }
        }

        public List<Departure> GetDeparturesByUserId(int uId)
        {
            IDeparturesRepository depRep = RepositoriesFactory.CreateDeparturesRepository();
            if (depRep == null)
            {
                throw new DeparturesRepositoryNullException("RepositoriesFactory.CreateDeparturesRepository() Error.");
            }

            try
            {
                List<Departure> res = depRep.GetDeparturesByUserId(uId);

                foreach (var elem in  res)
                {
                    DeparturesValidator.ValidateDeparture(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(DeparturesValidatorFailException)))
                {
                    throw new DeparturesValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new DepartureNotFoundException();
                }
            }
        }

        public Departure GetDepartureById(int id)
        {
            IDeparturesRepository depRep = RepositoriesFactory.CreateDeparturesRepository();
            if (depRep == null)
            {
                throw new DeparturesRepositoryNullException("RepositoriesFactory.CreateDeparturesRepository() Error.");
            }

            try
            {
                Departure res = depRep.GetDepartureById(id);
                DeparturesValidator.ValidateDeparture(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(DeparturesValidatorFailException)))
                {
                    throw new DeparturesValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new DepartureNotFoundException();
                }
            }
        }

        public int AddDeparture(Departure dep, LinkOwnerCarDeparture link)
        {
            IDeparturesRepository depRep = RepositoriesFactory.CreateDeparturesRepository();
            if (depRep == null)
            {
                throw new DeparturesRepositoryNullException("RepositoriesFactory.CreateDeparturesRepository() Error.");
            }

            try
            {
                DeparturesValidator.ValidateDeparture(dep);
                depRep.AddDeparture(dep, link);

                return 0;
            }
            catch (Exception)
            {
                throw new DepartureAddException();
            }
        }

        public int DeleteDeparture(int id)
        {
            IDeparturesRepository depRep = RepositoriesFactory.CreateDeparturesRepository();
            if (depRep == null)
            {
                throw new DeparturesRepositoryNullException("RepositoriesFactory.CreateDeparturesRepository() Error.");
            }

            try
            {
                depRep.DeleteDeparture(id);

                return 0;
            }
            catch (Exception)
            {
                throw new DepartureDeleteException();
            }
        }
        

        // !Cars
        public List<Car> GetCars(int offset = 0, int limit = -1)
        {
            ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
            if (carRep == null)
            {
                throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
            }

            try
            {
                List<Car> res = carRep.GetCars(offset, limit);

                foreach (var elem in  res)
                {
                    CarsValidator.ValidateCar(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarsValidatorFailException)))
                {
                    throw new CarsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
        }

        public List<Car> GetCarsByModelId(int mId)
        {
            ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
            if (carRep == null)
            {
                throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
            }

            try
            {
                List<Car> res = carRep.GetCarsByModelId(mId);

                foreach (var elem in  res)
                {
                    CarsValidator.ValidateCar(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarsValidatorFailException)))
                {
                    throw new CarsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
        }

        public List<Car> GetCarsByEquipmentId(int eId)
        {
            ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
            if (carRep == null)
            {
                throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
            }

            try
            {
                List<Car> res = carRep.GetCarsByEquipmentId(eId);

                foreach (var elem in  res)
                {
                    CarsValidator.ValidateCar(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarsValidatorFailException)))
                {
                    throw new CarsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
        }

        public List<Car> GetCarsByColorId(int cId)
        {
            ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
            if (carRep == null)
            {
                throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
            }

            try
            {
                List<Car> res = carRep.GetCarsByColorId(cId);

                foreach (var elem in  res)
                {
                    CarsValidator.ValidateCar(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarsValidatorFailException)))
                {
                    throw new CarsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
        }

        public Car GetCarsByComingId(int comId)
        {
            ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
            if (carRep == null)
            {
                throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
            }

            try
            {
                Car res = carRep.GetCarByComingId(comId);
                CarsValidator.ValidateCar(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarsValidatorFailException)))
                {
                    throw new CarsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
        }

        public Car GetCarById(string id)
        {
            ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
            if (carRep == null)
            {
                throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
            }

            try
            {
                Car res = carRep.GetCarById(id);
                CarsValidator.ValidateCar(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarsValidatorFailException)))
                {
                    throw new CarsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
        }

        public int GetComingIdByCarId(string id)
        {
            ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
            if (carRep == null)
            {
                throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
            }

            try
            {
                int res = carRep.GetComingIdByCarId(id);

                return res;
            }
            catch (Exception)
            {
                
                throw new ComingNotFoundException();
            }
        }

        // public int AddCar(Car car)
        // {
        //     ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
        //     if (carRep == null)
        //     {
        //         throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
        //     }

        //     try
        //     {
        //         CarsValidator.ValidateCar(car);
        //         carRep.AddCar(car);

        //         return 0;
        //     }
        //     catch (Exception)
        //     {
        //         throw new CarAddException();
        //     }
        // }

        public int UpdateCar(string id, Car newCar)
        {
            ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
            if (carRep == null)
            {
                throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
            }

            try
            {
                var car = carRep.GetCarById(id);
                var carTmp = new Car(car.Id, car.ModelId, newCar.EquipmentId, newCar.ColorId, car.ComingId);
                CarsValidator.ValidateCar(carTmp);
                carRep.UpdateCar(id, carTmp);

                return 0;
            }
            catch (Exception)
            {
                throw new CarUpdateException();
            }
        }

        // public int DeleteCar(string id)
        // {
        //     ICarsRepository carRep = RepositoriesFactory.CreateCarsRepository();
        //     if (carRep == null)
        //     {
        //         throw new CarsRepositoryNullException("RepositoriesFactory.CreateCarsRepository() Error.");
        //     }

        //     try
        //     {
        //         carRep.DeleteCar(id);

        //         return 0;
        //     }
        //     catch (Exception)
        //     {
        //         throw new CarDeleteException();
        //     }
        // }

        // !CarOwners
        public List<CarOwner> GetCarOwners(int offset = 0, int limit = -1)
        {
            ICarOwnersRepository carOwnersRep = RepositoriesFactory.CreateCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new CarOwnersRepositoryNullException("RepositoriesFactory.CreateCarOwnersRepository() Error.");
            }

            try
            {
                List<CarOwner> res = carOwnersRep.GetCarOwners(offset, limit);

                foreach (var elem in  res)
                {
                    CarOwnersValidator.ValidateCarOwner(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarOwnersValidatorFailException)))
                {
                    throw new CarOwnersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarOwnerNotFoundException();
                }
            }
        }

        public List<CarOwner> GetCarOwnersByName(string name)
        {
            ICarOwnersRepository carOwnersRep = RepositoriesFactory.CreateCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new CarOwnersRepositoryNullException("RepositoriesFactory.CreateCarOwnersRepository() Error.");
            }

            try
            {
                List<CarOwner> res = carOwnersRep.GetCarOwnersByName(name);

                foreach (var elem in  res)
                {
                    CarOwnersValidator.ValidateCarOwner(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarOwnersValidatorFailException)))
                {
                    throw new CarOwnersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarOwnerNotFoundException();
                }
            }
        }

        public List<CarOwner> GetCarOwnersBySurname(string surname)
        {
            ICarOwnersRepository carOwnersRep = RepositoriesFactory.CreateCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new CarOwnersRepositoryNullException("RepositoriesFactory.CreateCarOwnersRepository() Error.");
            }

            try
            {
                List<CarOwner> res = carOwnersRep.GetCarOwnersBySurname(surname);

                foreach (var elem in  res)
                {
                    CarOwnersValidator.ValidateCarOwner(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarOwnersValidatorFailException)))
                {
                    throw new CarOwnersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarOwnerNotFoundException();
                }
            }
        }

        public CarOwner GetCarOwnerByEmail(string email)
        {
            ICarOwnersRepository carOwnersRep = RepositoriesFactory.CreateCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new CarOwnersRepositoryNullException("RepositoriesFactory.CreateCarOwnersRepository() Error.");
            }

            try
            {
                CarOwner res = carOwnersRep.GetCarOwnerByEmail(email);
                CarOwnersValidator.ValidateCarOwner(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarOwnersValidatorFailException)))
                {
                    throw new CarOwnersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarOwnerNotFoundException();
                }
            }
        }

        public CarOwner GetCarOwnerById(int id)
        {
            ICarOwnersRepository carOwnersRep = RepositoriesFactory.CreateCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new CarOwnersRepositoryNullException("RepositoriesFactory.CreateCarOwnersRepository() Error.");
            }

            try
            {
                CarOwner res = carOwnersRep.GetCarOwnerById(id);
                CarOwnersValidator.ValidateCarOwner(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarOwnersValidatorFailException)))
                {
                    throw new CarOwnersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarOwnerNotFoundException();
                }
            }
        }

        public int AddCarOwner(CarOwner owner)
        {
            ICarOwnersRepository carOwnersRep = RepositoriesFactory.CreateCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new CarOwnersRepositoryNullException("RepositoriesFactory.CreateCarOwnersRepository() Error.");
            }

            try
            {
                CarOwnersValidator.ValidateCarOwner(owner);
                carOwnersRep.AddCarOwner(owner);

                return 0;
            }
            catch (Exception)
            {
                throw new CarOwnerAddException();
            }
        }

        public int UpdateCarOwner(int id, CarOwner newCarOwner)
        {
            ICarOwnersRepository carOwnersRep = RepositoriesFactory.CreateCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new CarOwnersRepositoryNullException("RepositoriesFactory.CreateCarOwnersRepository() Error.");
            }

            try
            {
                var carOwner = carOwnersRep.GetCarOwnerById(id);
                var carOwnerTmp = new CarOwner(carOwner.Id, newCarOwner.Name, newCarOwner.Surname, newCarOwner.Email);
                CarOwnersValidator.ValidateCarOwner(carOwnerTmp);
                carOwnersRep.UpdateCarOwner(id, carOwnerTmp);

                return 0;
            }
            catch (Exception)
            {
                throw new CarOwnerUpdateException();
            }
        }

        public int DeleteCarOwner(int id)
        {
            ICarOwnersRepository carOwnersRep = RepositoriesFactory.CreateCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new CarOwnersRepositoryNullException("RepositoriesFactory.CreateCarOwnersRepository() Error.");
            }

            try
            {
                carOwnersRep.DeleteCarOwner(id);

                return 0;
            }
            catch (Exception)
            {
                throw new CarOwnerDeleteException();
            }
        }

        // !LinksOwnerCarDeparture
        public List<LinkOwnerCarDeparture> GetLinksOwnerCarDeparture(int offset = 0, int limit = -1)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                List<LinkOwnerCarDeparture> res = LinksOwnerCarDepartureRep.GetLinksOwnerCarDeparture(offset, limit);

                foreach (var elem in  res)
                {
                    LinksOwnerCarDepartureValidator.ValidateLinkOwnerCarDeparture(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(LinksOwnerCarDepartureValidatorFailException)))
                {
                    throw new LinksOwnerCarDepartureValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new LinkOwnerCarDepartureNotFoundException();
                }
            }
        }

        public List<LinkOwnerCarDeparture> GetLinksOwnerCarDepartureByOwnerId(int oId)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                List<LinkOwnerCarDeparture> res = LinksOwnerCarDepartureRep.GetLinksOwnerCarDepartureByOwnerId(oId);

                foreach (var elem in  res)
                {
                    LinksOwnerCarDepartureValidator.ValidateLinkOwnerCarDeparture(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(LinksOwnerCarDepartureValidatorFailException)))
                {
                    throw new LinksOwnerCarDepartureValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new LinkOwnerCarDepartureNotFoundException();
                }
            }
        }

        public LinkOwnerCarDeparture GetLinkOwnerCarDepartureById(int id)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                LinkOwnerCarDeparture res = LinksOwnerCarDepartureRep.GetLinkOwnerCarDepartureById(id);
                LinksOwnerCarDepartureValidator.ValidateLinkOwnerCarDeparture(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(LinksOwnerCarDepartureValidatorFailException)))
                {
                    throw new LinksOwnerCarDepartureValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new LinkOwnerCarDepartureNotFoundException();
                }
            }
        }

        public List<Car> GetCarsNotInLinksOwnerCarDeparture()
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                List<Car> res = LinksOwnerCarDepartureRep.GetCarsNotInLinksOwnerCarDeparture();

                foreach (var elem in res)
                {
                    CarsValidator.ValidateCar(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarsValidatorFailException)))
                {
                    throw new CarsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
        }

        public Car GetCarIfNotInLinksOwnerCarDeparture(string id)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                Car res = LinksOwnerCarDepartureRep.GetCarIfNotInLinksOwnerCarDeparture(id);
                CarsValidator.ValidateCar(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(CarsValidatorFailException)))
                {
                    throw new CarsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
        }

        public LinkOwnerCarDeparture GetLinkOwnerCarDepartureByCarId(string cId)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                LinkOwnerCarDeparture res = LinksOwnerCarDepartureRep.GetLinkOwnerCarDepartureByCarId(cId);
                LinksOwnerCarDepartureValidator.ValidateLinkOwnerCarDeparture(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(LinksOwnerCarDepartureValidatorFailException)))
                {
                    throw new LinksOwnerCarDepartureValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new LinkOwnerCarDepartureNotFoundException();
                }
            }
        }

        public LinkOwnerCarDeparture GetLinkOwnerCarDepartureByDepartureId(int depId)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                LinkOwnerCarDeparture res = LinksOwnerCarDepartureRep.GetLinkOwnerCarDepartureByDepartureId(depId);
                LinksOwnerCarDepartureValidator.ValidateLinkOwnerCarDeparture(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(LinksOwnerCarDepartureValidatorFailException)))
                {
                    throw new LinksOwnerCarDepartureValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new LinkOwnerCarDepartureNotFoundException();
                }
            }
        }

        public string GetCarIdByDepartureId(int depId)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                string res = LinksOwnerCarDepartureRep.GetCarIdByDepartureId(depId);

                return res;
            }
            catch (Exception)
            {
                throw new LinkOwnerCarDepartureNotFoundException();
            }
        }

        public int GetDepartureIdByCarId(string carId)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                int res = LinksOwnerCarDepartureRep.GetDepartureIdByCarId(carId);

                return res;
            }
            catch (Exception)
            {
                throw new LinkOwnerCarDepartureNotFoundException();
            }
        }

        public int AddLinkOwnerCarDeparture(LinkOwnerCarDeparture oc)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                LinksOwnerCarDepartureValidator.ValidateLinkOwnerCarDeparture(oc);
                LinksOwnerCarDepartureRep.AddLinkOwnerCarDeparture(oc);

                return 0;
            }
            catch (Exception)
            {
                throw new LinkOwnerCarDepartureAddException();
            }
        }

        public int DeleteLinkOwnerCarDeparture(int id)
        {
            ILinksOwnerCarDepartureRepository LinksOwnerCarDepartureRep = RepositoriesFactory.CreateLinksOwnerCarDepartureRepository();
            if (LinksOwnerCarDepartureRep == null)
            {
                throw new LinksOwnerCarDepartureRepositoryNullException("RepositoriesFactory.CreateLinksOwnerCarDepartureRepository() Error.");
            }

            try
            {
                LinksOwnerCarDepartureRep.DeleteLinkOwnerCarDeparture(id);

                return 0;
            }
            catch (Exception)
            {
                throw new LinkOwnerCarDepartureDeleteException();
            }
        }

        // !Models
        public List<Model> GetModels(int offset = 0, int limit = -1)
        {
            IModelsRepository modelsRep = RepositoriesFactory.CreateModelsRepository();
            if (modelsRep == null)
            {
                throw new ModelsRepositoryNullException("RepositoriesFactory.CreateModelsRepository() Error.");
            }

            try
            {
                List<Model> res = modelsRep.GetModels(offset, limit);

                foreach (var elem in  res)
                {
                    ModelsValidator.ValidateModel(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ModelsValidatorFailException)))
                {
                    throw new ModelsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ModelNotFoundException();
                }
            }
        }

        public List<Model> GetModelsByName(string name)
        {
            IModelsRepository modelsRep = RepositoriesFactory.CreateModelsRepository();
            if (modelsRep == null)
            {
                throw new ModelsRepositoryNullException("RepositoriesFactory.CreateModelsRepository() Error.");
            }

            try
            {
                List<Model> res = modelsRep.GetModelsByName(name);

                foreach (var elem in  res)
                {
                    ModelsValidator.ValidateModel(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ModelsValidatorFailException)))
                {
                    throw new ModelsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ModelNotFoundException();
                }
            }
        }

        public List<Model> GetModelsByBrandId(int bId)
        {
            IModelsRepository modelsRep = RepositoriesFactory.CreateModelsRepository();
            if (modelsRep == null)
            {
                throw new ModelsRepositoryNullException("RepositoriesFactory.CreateModelsRepository() Error.");
            }

            try
            {
                List<Model> res = modelsRep.GetModelsByBrandId(bId);

                foreach (var elem in  res)
                {
                    ModelsValidator.ValidateModel(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ModelsValidatorFailException)))
                {
                    throw new ModelsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ModelNotFoundException();
                }
            }
        }

        public Model GetModelById(int id)
        {
            IModelsRepository modelsRep = RepositoriesFactory.CreateModelsRepository();
            if (modelsRep == null)
            {
                throw new ModelsRepositoryNullException("RepositoriesFactory.CreateModelsRepository() Error.");
            }

            try
            {
                Model res = modelsRep.GetModelById(id);
                ModelsValidator.ValidateModel(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ModelsValidatorFailException)))
                {
                    throw new ModelsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ModelNotFoundException();
                }
            }
        }

        public int AddModel(Model m)
        {
            IModelsRepository modelsRep = RepositoriesFactory.CreateModelsRepository();
            if (modelsRep == null)
            {
                throw new ModelsRepositoryNullException("RepositoriesFactory.CreateModelsRepository() Error.");
            }

            try
            {
                ModelsValidator.ValidateModel(m);
                modelsRep.AddModel(m);

                return 0;
            }
            catch (Exception)
            {
                throw new ModelAddException();
            }
        }

        public int UpdateModel(int id, Model newModel)
        {
            IModelsRepository modelsRep = RepositoriesFactory.CreateModelsRepository();
            if (modelsRep == null)
            {
                throw new ModelsRepositoryNullException("RepositoriesFactory.CreateModelsRepository() Error.");
            }

            try
            {
                var model = modelsRep.GetModelById(id);
                var modelTmp = new Model(model.Id, model.BrandId, newModel.Name);
                ModelsValidator.ValidateModel(modelTmp);
                modelsRep.UpdateModel(id, modelTmp);

                return 0;
            }
            catch (Exception)
            {
                throw new ModelUpdateException();
            }
        }

        public int DeleteModel(int id)
        {
            IModelsRepository modelsRep = RepositoriesFactory.CreateModelsRepository();
            if (modelsRep == null)
            {
                throw new ModelsRepositoryNullException("RepositoriesFactory.CreateModelsRepository() Error.");
            }

            try
            {
                modelsRep.DeleteModel(id);

                return 0;
            }
            catch (Exception)
            {
                throw new ModelDeleteException();
            }
        }

        // !Brands
        public List<Brand> GetBrands(int offset = 0, int limit = -1)
        {
            IBrandsRepository brandsRep = RepositoriesFactory.CreateBrandsRepository();
            if (brandsRep == null)
            {
                throw new BrandsRepositoryNullException("RepositoriesFactory.CreateBrandsRepository() Error.");
            }

            try
            {
                List<Brand> res = brandsRep.GetBrands(offset, limit);

                foreach (var elem in  res)
                {
                    BrandsValidator.ValidateBrand(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }

        public List<Brand> GetBrandsByManufactCountry(string mc)
        {
            IBrandsRepository brandsRep = RepositoriesFactory.CreateBrandsRepository();
            if (brandsRep == null)
            {
                throw new BrandsRepositoryNullException("RepositoriesFactory.CreateBrandsRepository() Error.");
            }

            try
            {
                List<Brand> res = brandsRep.GetBrandsByManufactCountry(mc);

                foreach (var elem in  res)
                {
                    BrandsValidator.ValidateBrand(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }

        public List<Brand> GetBrandsByWheel(string wh)
        {
            IBrandsRepository brandsRep = RepositoriesFactory.CreateBrandsRepository();
            if (brandsRep == null)
            {
                throw new BrandsRepositoryNullException("RepositoriesFactory.CreateBrandsRepository() Error.");
            }

            try
            {
                List<Brand> res = brandsRep.GetBrandsByWheel(wh);

                foreach (var elem in  res)
                {
                    BrandsValidator.ValidateBrand(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }
        
        public Brand GetBrandByName(string name)
        {
            IBrandsRepository brandsRep = RepositoriesFactory.CreateBrandsRepository();
            if (brandsRep == null)
            {
                throw new BrandsRepositoryNullException("RepositoriesFactory.CreateBrandsRepository() Error.");
            }

            try
            {
                Brand res = brandsRep.GetBrandByName(name);
                BrandsValidator.ValidateBrand(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }

        public Brand GetBrandById(int id)
        {
            IBrandsRepository brandsRep = RepositoriesFactory.CreateBrandsRepository();
            if (brandsRep == null)
            {
                throw new BrandsRepositoryNullException("RepositoriesFactory.CreateBrandsRepository() Error.");
            }

            try
            {
                Brand res = brandsRep.GetBrandById(id);
                BrandsValidator.ValidateBrand(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }

        public int AddBrand(Brand b)
        {
            IBrandsRepository brandsRep = RepositoriesFactory.CreateBrandsRepository();
            if (brandsRep == null)
            {
                throw new BrandsRepositoryNullException("RepositoriesFactory.CreateBrandsRepository() Error.");
            }

            try
            {
                BrandsValidator.ValidateBrand(b);
                brandsRep.AddBrand(b);

                return 0;
            }
            catch (Exception)
            {
                throw new BrandAddException();
            }
        }

        public int UpdateBrand(int id, Brand newBrand)
        {
            IBrandsRepository brandsRep = RepositoriesFactory.CreateBrandsRepository();
            if (brandsRep == null)
            {
                throw new BrandsRepositoryNullException("RepositoriesFactory.CreateBrandsRepository() Error.");
            }

            try
            {
                var brand = brandsRep.GetBrandById(id);
                var brandTmp = new Brand(brand.Id, newBrand.Name, newBrand.ManufactCountry, newBrand.Wheel);
                BrandsValidator.ValidateBrand(brandTmp);
                brandsRep.UpdateBrand(id, brandTmp);

                return 0;
            }
            catch (Exception)
            {
                throw new BrandUpdateException();
            }
        }

        public int DeleteBrand(int id)
        {
            IBrandsRepository brandsRep = RepositoriesFactory.CreateBrandsRepository();
            if (brandsRep == null)
            {
                throw new BrandsRepositoryNullException("RepositoriesFactory.CreateBrandsRepository() Error.");
            }

            try
            {
                brandsRep.DeleteBrand(id);

                return 0;
            }
            catch (Exception)
            {
                throw new BrandDeleteException();
            }
        }

        // !Equipments
        public List<Equipment> GetEquipments(int offset = 0, int limit = -1)
        {
            IEquipmentsRepository eqsRep = RepositoriesFactory.CreateEquipmentsRepository();
            if (eqsRep == null)
            {
                throw new EquipmentsRepositoryNullException("RepositoriesFactory.CreateEquipmentsRepository() Error.");
            }

            try
            {
                List<Equipment> res = eqsRep.GetEquipments(offset, limit);

                foreach (var elem in  res)
                {
                    EquipmentsValidator.ValidateEquipment(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }

        public List<Equipment> GetEquipmentsByCategory(string category)
        {
            IEquipmentsRepository eqsRep = RepositoriesFactory.CreateEquipmentsRepository();
            if (eqsRep == null)
            {
                throw new EquipmentsRepositoryNullException("RepositoriesFactory.CreateEquipmentsRepository() Error.");
            }

            try
            {
                List<Equipment> res = eqsRep.GetEquipmentsByCategory(category);

                foreach (var elem in  res)
                {
                    EquipmentsValidator.ValidateEquipment(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }

        public List<Equipment> GetEquipmentsByGear(string gear)
        {
            IEquipmentsRepository eqsRep = RepositoriesFactory.CreateEquipmentsRepository();
            if (eqsRep == null)
            {
                throw new EquipmentsRepositoryNullException("RepositoriesFactory.CreateEquipmentsRepository() Error.");
            }

            try
            {
                List<Equipment> res = eqsRep.GetEquipmentsByGear(gear);

                foreach (var elem in  res)
                {
                    EquipmentsValidator.ValidateEquipment(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }

        public List<Equipment> GetEquipmentsByRoofType(string rooftype)
        {
            IEquipmentsRepository eqsRep = RepositoriesFactory.CreateEquipmentsRepository();
            if (eqsRep == null)
            {
                throw new EquipmentsRepositoryNullException("RepositoriesFactory.CreateEquipmentsRepository() Error.");
            }

            try
            {
                List<Equipment> res = eqsRep.GetEquipmentsByRoofType(rooftype);

                foreach (var elem in  res)
                {
                    EquipmentsValidator.ValidateEquipment(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }

        public Equipment GetEquipmentById(int id)
        {
            IEquipmentsRepository eqsRep = RepositoriesFactory.CreateEquipmentsRepository();
            if (eqsRep == null)
            {
                throw new EquipmentsRepositoryNullException("RepositoriesFactory.CreateEquipmentsRepository() Error.");
            }

            try
            {
                Equipment res = eqsRep.GetEquipmentById(id);
                EquipmentsValidator.ValidateEquipment(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(EquipmentsValidatorFailException)))
                {
                    throw new EquipmentsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new EquipmentNotFoundException();
                }
            }
        }

        public int AddEquipment(Equipment eq)
        {
            IEquipmentsRepository eqsRep = RepositoriesFactory.CreateEquipmentsRepository();
            if (eqsRep == null)
            {
                throw new EquipmentsRepositoryNullException("RepositoriesFactory.CreateEquipmentsRepository() Error.");
            }

            try
            {
                EquipmentsValidator.ValidateEquipment(eq);
                eqsRep.AddEquipment(eq);

                return 0;
            }
            catch (Exception)
            {
                throw new EquipmentAddException();
            }
        }

        public int UpdateEquipment(int id, Equipment newEquipment)
        {
            IEquipmentsRepository eqsRep = RepositoriesFactory.CreateEquipmentsRepository();
            if (eqsRep == null)
            {
                throw new EquipmentsRepositoryNullException("RepositoriesFactory.CreateEquipmentsRepository() Error.");
            }

            try
            {
                var eq = eqsRep.GetEquipmentById(id);
                var eqTmp = new Equipment(eq.Id, newEquipment.Category, newEquipment.Gear, newEquipment.RoofType);
                EquipmentsValidator.ValidateEquipment(eqTmp);
                eqsRep.UpdateEquipment(id, eqTmp);

                return 0;
            }
            catch (Exception)
            {
                throw new EquipmentUpdateException();
            }
        }

        public int DeleteEquipment(int id)
        {
            IEquipmentsRepository eqsRep = RepositoriesFactory.CreateEquipmentsRepository();
            if (eqsRep == null)
            {
                throw new EquipmentsRepositoryNullException("RepositoriesFactory.CreateEquipmentsRepository() Error.");
            }

            try
            {
                eqsRep.DeleteEquipment(id);

                return 0;
            }
            catch (Exception)
            {
                throw new EquipmentDeleteException();
            }
        }

        // !Colors
        public List<Color> GetColors(int offset = 0, int limit = -1)
        {
            IColorsRepository colsRep = RepositoriesFactory.CreateColorsRepository();
            if (colsRep == null)
            {
                throw new ColorsRepositoryNullException("RepositoriesFactory.CreateColorsRepository() Error.");
            }

            try
            {
                List<Color> res = colsRep.GetColors(offset, limit);

                foreach (var elem in  res)
                {
                    ColorsValidator.ValidateColor(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ColorsValidatorFailException)))
                {
                    throw new ColorsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ColorNotFoundException();
                }
            }
        }

        public Color GetColorByName(string name)
        {
            IColorsRepository colsRep = RepositoriesFactory.CreateColorsRepository();
            if (colsRep == null)
            {
                throw new ColorsRepositoryNullException("RepositoriesFactory.CreateColorsRepository() Error.");
            }

            try
            {
                Color res = colsRep.GetColorByName(name);
                ColorsValidator.ValidateColor(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ColorsValidatorFailException)))
                {
                    throw new ColorsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ColorNotFoundException();
                }
            }
        }

        public Color GetColorById(int id)
        {
            IColorsRepository colsRep = RepositoriesFactory.CreateColorsRepository();
            if (colsRep == null)
            {
                throw new ColorsRepositoryNullException("RepositoriesFactory.CreateColorsRepository() Error.");
            }

            try
            {
                Color res = colsRep.GetColorById(id);
                ColorsValidator.ValidateColor(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(ColorsValidatorFailException)))
                {
                    throw new ColorsValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new ColorNotFoundException();
                }              
            }
        }

        public int AddColor(Color col)
        {
            IColorsRepository colsRep = RepositoriesFactory.CreateColorsRepository();
            if (colsRep == null)
            {
                throw new ColorsRepositoryNullException("RepositoriesFactory.CreateColorsRepository() Error.");
            }

            try
            {
                ColorsValidator.ValidateColor(col);
                colsRep.AddColor(col);

                return 0;
            }
            catch (Exception)
            {
                throw new ColorAddException();
            }
        }

        public int UpdateColor(int id, Color newColor)
        {
            IColorsRepository colsRep = RepositoriesFactory.CreateColorsRepository();
            if (colsRep == null)
            {
                throw new ColorsRepositoryNullException("RepositoriesFactory.CreateColorsRepository() Error.");
            }

            try
            {
                var color = colsRep.GetColorById(id);
                var colorTmp = new Color(color.Id, newColor.Name);
                ColorsValidator.ValidateColor(colorTmp);
                colsRep.UpdateColor(id, colorTmp);

                return 0;
            }
            catch (Exception)
            {
                throw new ColorUpdateException();
            }
        }

        public int DeleteColor(int id)
        {
            IColorsRepository colsRep = RepositoriesFactory.CreateColorsRepository();
            if (colsRep == null)
            {
                throw new ColorsRepositoryNullException("RepositoriesFactory.CreateColorsRepository() Error.");
            }

            try
            {
                colsRep.DeleteColor(id);

                return 0;
            }
            catch (Exception)
            {
                throw new ColorDeleteException();
            }
        }


        // !VIPCarOwners
        public List<VIPCarOwner> GetVIPCarOwners(int offset = 0, int limit = -1)
        {
            IVIPCarOwnersRepository carOwnersRep = RepositoriesFactory.CreateVIPCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new VIPCarOwnersRepositoryNullException("RepositoriesFactory.CreateVIPCarOwnersRepository() Error.");
            }

            try
            {
                List<VIPCarOwner> res = carOwnersRep.GetVIPCarOwners(offset, limit);

                foreach (var elem in  res)
                {
                    VIPCarOwnersValidator.ValidateVIPCarOwner(elem);
                }

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(VIPCarOwnersValidatorFailException)))
                {
                    throw new VIPCarOwnersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new VIPCarOwnerNotFoundException();
                }
            }
        }

        public VIPCarOwner GetVIPCarOwnerById(int id)
        {
            IVIPCarOwnersRepository carOwnersRep = RepositoriesFactory.CreateVIPCarOwnersRepository();
            if (carOwnersRep == null)
            {
                throw new VIPCarOwnersRepositoryNullException("RepositoriesFactory.CreateCarOwnersRepository() Error.");
            }

            try
            {
                VIPCarOwner res = carOwnersRep.GetVIPCarOwnerById(id);
                VIPCarOwnersValidator.ValidateVIPCarOwner(res);

                return res;
            }
            catch (Exception exc)
            {
                if (exc.GetType().IsAssignableFrom(typeof(VIPCarOwnersValidatorFailException)))
                {
                    throw new VIPCarOwnersValidatorFailException("Invalid Format.", exc.InnerException);
                }
                else
                {
                    throw new VIPCarOwnerNotFoundException();
                }
            }
        }
    }
}