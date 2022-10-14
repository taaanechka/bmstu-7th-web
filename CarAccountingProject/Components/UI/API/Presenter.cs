using System;
using BL;

using API.Exceptions;

namespace API
{
    public class Presenter
    {
        private BL.Facade Facade;
        private Permissions AccessPermissions;

        public Presenter(BL.Facade facade, int access=0)
        {
            Facade = facade;

            AccessPermissions = (BL.Permissions) access;
        }

        public BL.User LogIn(string login, string password)
        {
            if (AccessPermissions != Permissions.UNAUTHORIZED)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            BL.User user = Facade.LogIn(login, password);
            AccessPermissions = user.UserType;

            return user;
        }

        public void LogOut()
        {
            AccessPermissions = Permissions.UNAUTHORIZED;

            //return Facade.LogOut(login);
        }

        // !Users
        public List<BL.User> GetUsers(int offset = 0, int limit = -1)
        {
            Console.WriteLine($"PERM:{AccessPermissions.ToString()}\n");
            
            // if (AccessPermissions != Permissions.ADMIN)
            // {
            //     throw new AdminPermissionsException("Access permissions are missing.");
            // }

            return Facade.GetUsers(offset, limit);
        }

        public List<BL.User> GetUsersByName(string name)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetUsersByName(name);
        }

        public List<BL.User> GetUsersBySurname(string surname)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetUsersBySurname(surname);
        }

        public List<BL.User> GetUsersByUserType(int userType)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED || (int) AccessPermissions < userType || 
                (userType == (int) Permissions.UNAUTHORIZED && AccessPermissions != Permissions.ADMIN))
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetUsersByUserType(userType);
        }

        public BL.User GetUserByLogin(string login)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetUserByLogin(login);
        }

        public BL.User GetUserById(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetUserById(id);
        }

        public int AddUser(BL.User user)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.AddUser(user);
        }

        public int UpdateUser(int id, BL.User newUser)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.UpdateUser(id, newUser);
        }

        public int BlockUser(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.BlockUser(id);
        }

        // !Comings
        public List<BL.Coming> GetComings(int offset = 0, int limit = -1)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }
            else
            {
                return Facade.GetComings(offset, limit);
            }
        }

        public List<BL.Coming> GetComingsByDate(DateTime date)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade.GetComingsByDate(date);
        }

        public List<BL.Coming> GetComingsBetweenDates(DateTime date1, DateTime date2)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade.GetComingsBetweenDates(date1, date2);
        }

        public List<BL.Coming> GetComingsByUserId(int uId)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade.GetComingsByUserId(uId);
        }

        public BL.Coming GetComingById(int id)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade.GetComingById(id);
        }

        public int AddComing(BL.Coming com, BL.Car car)
        {
            if (AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new EmployeePermissionsException("Access permissions are missing.");
            }

            return Facade.AddComing(com, car);
        }

        public int DeleteComing(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.DeleteComing(id);
        }


        // !Departures
        public List<BL.Departure> GetDepartures(int offset = 0, int limit = -1)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetDepartures(offset, limit);
        }

        public List<BL.Departure> GetDeparturesByDate(DateTime date)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade.GetDeparturesByDate(date);
        }

        public List<BL.Departure> GetDeparturesBetweenDates(DateTime date1, DateTime date2)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade.GetDeparturesBetweenDates(date1, date2);
        }

        public List<BL.Departure> GetDeparturesByUserId(int uId)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade.GetDeparturesByUserId(uId);
        }

        public BL.Departure GetDepartureById(int id)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade.GetDepartureById(id);
        }

        public int AddDeparture(BL.Departure dep, BL.LinkOwnerCarDeparture link)
        {
            if (AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new EmployeePermissionsException("Access permissions are missing.");
            }

            return Facade.AddDeparture(dep, link);
        }

        public int DeleteDeparture(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.DeleteDeparture(id);
        }
        

        // !Cars
        public List<BL.Car> GetCars(int offset = 0, int limit = -1)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCars(offset, limit);
        }

        public List<BL.Car> GetCarsByModelId(int mId)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarsByModelId(mId);
        }

        public List<BL.Car> GetCarsByEquipmentId(int eId)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarsByEquipmentId(eId);
        }

        public List<BL.Car> GetCarsByColorId(int cId)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarsByColorId(cId);
        }

        public BL.Car GetCarsByComingId(int comId)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarsByComingId(comId);
        }

        public BL.Car GetCarById(string id)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarById(id);
        }

        public int GetComingIdByCarId(string id)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetComingIdByCarId(id);
        }

        // public int AddCar(BL.Car car)
        // {
        //     if (AccessPermissions != Permissions.EMPLOYEE)
        //     {
        //         throw new EmployeePermissionsException("Access permissions are missing.");
        //     }

        //     return Facade.AddCar(car);
        // }

        public int UpdateCar(string id, BL.Car newCar)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.UpdateCar(id, newCar);
        }

        // public int DeleteCar(string id)
        // {
        //     if (AccessPermissions != Permissions.ADMIN)
        //     {
        //         throw new AdminPermissionsException("Access permissions are missing.");
        //     }

        //     return Facade.DeleteCar(id);
        // }

        // !CarOwners
        public List<BL.CarOwner> GetCarOwners(int offset = 0, int limit = -1)
        {
            if (AccessPermissions != Permissions.ADMIN && AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarOwners(offset, limit);
        }

        public List<BL.CarOwner> GetCarOwnersByName(string name)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarOwnersByName(name);
        }

        public List<BL.CarOwner> GetCarOwnersBySurname(string surname)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarOwnersBySurname(surname);
        }

        public BL.CarOwner GetCarOwnerByEmail(string email)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarOwnerByEmail(email);
        }

        public BL.CarOwner GetCarOwnerById(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarOwnerById(id);
        }

        public int AddCarOwner(BL.CarOwner owner)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new EmployeePermissionsException("Access permissions are missing.");
            }

            return Facade.AddCarOwner(owner);
        }

        public int UpdateCarOwner(int id, BL.CarOwner newCarOwner)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.UpdateCarOwner(id, newCarOwner);
        }

        public int DeleteCarOwner(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.DeleteCarOwner(id);
        }

        // !LinksOwnerCarDeparture
        public List<BL.LinkOwnerCarDeparture> GetLinksOwnerCarDeparture(int offset = 0, int limit = -1)
        {
            if (AccessPermissions != Permissions.ADMIN && AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new EmployeePermissionsException("Access permissions are missing.");
            }

            return Facade.GetLinksOwnerCarDeparture(offset, limit);
        }

        public List<BL.LinkOwnerCarDeparture> GetLinksOwnerCarDepartureByOwnerId(int oId)
        {
            if (AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new EmployeePermissionsException("Access permissions are missing.");
            }

            return Facade.GetLinksOwnerCarDepartureByOwnerId(oId);
        }

        public BL.LinkOwnerCarDeparture GetLinkOwnerCarDepartureById(int id)
        {
            if (AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new EmployeePermissionsException("Access permissions are missing.");
            }

            return Facade.GetLinkOwnerCarDepartureById(id);
        }

        public List<BL.Car> GetCarsNotInLinksOwnerCarDeparture()
        {
            if (AccessPermissions != Permissions.ANALYST)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarsNotInLinksOwnerCarDeparture();
        }

        public BL.Car GetCarIfNotInLinksOwnerCarDeparture(string id)
        {
            if (AccessPermissions != Permissions.ANALYST)
            {
                throw new AnalystPermissionsException("Access permissions are missing.");
            }

            return Facade. GetCarIfNotInLinksOwnerCarDeparture(id);
        }

        public BL.LinkOwnerCarDeparture GetLinkOwnerCarDepartureByCarId(string cId)
        {
            if (AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new EmployeePermissionsException("Access permissions are missing.");
            }

            return Facade.GetLinkOwnerCarDepartureByCarId(cId);
        }

        public BL.LinkOwnerCarDeparture GetLinkOwnerCarDepartureByDepartureId(int depId)
        {
            if (AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new EmployeePermissionsException("Access permissions are missing.");
            }

            return Facade.GetLinkOwnerCarDepartureByDepartureId(depId);
        }

        public string GetCarIdByDepartureId(int depId)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetCarIdByDepartureId(depId);
        }

        public int GetDepartureIdByCarId(string carId)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetDepartureIdByCarId(carId);
        }

        public int AddLinkOwnerCarDeparture(BL.LinkOwnerCarDeparture oc)
        {
            if (AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new EmployeePermissionsException("Access permissions are missing.");
            }

            return Facade.AddLinkOwnerCarDeparture(oc);
        }

        public int DeleteLinkOwnerCarDeparture(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.DeleteLinkOwnerCarDeparture(id);
        }

        // !Models
        public List<BL.Model> GetModels(int offset = 0, int limit = -1)
        {
            if (AccessPermissions != Permissions.ADMIN && AccessPermissions != Permissions.EMPLOYEE)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetModels(offset, limit);
        }

        public List<BL.Model> GetModelsByName(string name)
        {
            if (AccessPermissions != Permissions.ADMIN && AccessPermissions != Permissions.ANALYST)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetModelsByName(name);
        }

        public List<BL.Model> GetModelsByBrandId(int bId)
        {
            if (AccessPermissions != Permissions.ADMIN && AccessPermissions != Permissions.ANALYST)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetModelsByBrandId(bId);
        }

        public BL.Model GetModelById(int id)
        {
            if (AccessPermissions != Permissions.ADMIN && AccessPermissions != Permissions.ANALYST)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetModelById(id);
        }

        public int AddModel(BL.Model m)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.AddModel(m);
        }

        public int UpdateModel(int id, BL.Model newModel)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.UpdateModel(id, newModel);
        }

        public int DeleteModel(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.DeleteModel(id);
        }

        // !Brands
        public List<BL.Brand> GetBrands(int offset = 0, int limit = -1)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetBrands(offset, limit);
        }

        public List<BL.Brand> GetBrandsByManufactCountry(string mc)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetBrandsByManufactCountry(mc);
        }

        public List<BL.Brand> GetBrandsByWheel(string wh)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetBrandsByWheel(wh);
        }
        
        public BL.Brand GetBrandByName(string name)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetBrandByName(name);
        }

        public BL.Brand GetBrandById(int id)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetBrandById(id);
        }

        public int AddBrand(BL.Brand b)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.AddBrand(b);
        }

        public int UpdateBrand(int id, BL.Brand newBrand)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.UpdateBrand(id, newBrand);
        }

        public int DeleteBrand(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.DeleteBrand(id);
        }

        // !Equipments
        public List<BL.Equipment> GetEquipments(int offset = 0, int limit = -1)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetEquipments(offset, limit);
        }

        public List<BL.Equipment> GetEquipmentsByCategory(string category)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetEquipmentsByCategory(category);
        }

        public List<BL.Equipment> GetEquipmentsByGear(string gear)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetEquipmentsByGear(gear);
        }

        public List<BL.Equipment> GetEquipmentsByRoofType(string rooftype)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetEquipmentsByRoofType(rooftype);
        }

        public BL.Equipment GetEquipmentById(int id)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetEquipmentById(id);
        }

        public int AddEquipment(BL.Equipment eq)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.AddEquipment(eq);
        }

        public int UpdateEquipment(int id, BL.Equipment newEquipment)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.UpdateEquipment(id, newEquipment);
        }

        public int DeleteEquipment(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.DeleteEquipment(id);
        }

        // !Colors
        public List<BL.Color> GetColors(int offset = 0, int limit = -1)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetColors(offset, limit);
        }

        public BL.Color GetColorByName(string name)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetColorByName(name);
        }

        public BL.Color GetColorById(int id)
        {
            if (AccessPermissions == Permissions.UNAUTHORIZED) 
            {
                throw new UnauthorizedPermissionsException("Access permissions are missing.");
            }

            return Facade.GetColorById(id);
        }

        public int AddColor(BL.Color col)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.AddColor(col);
        }

        public int UpdateColor(int id, BL.Color newColor)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.UpdateColor(id, newColor);
        }

        public int DeleteColor(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.DeleteColor(id);
        }

        // !VIPCarOwners
        public List<BL.VIPCarOwner> GetVIPCarOwners(int offset = 0, int limit = -1)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetVIPCarOwners(offset, limit);
        }

        public VIPCarOwner GetVIPCarOwnerById(int id)
        {
            if (AccessPermissions != Permissions.ADMIN)
            {
                throw new AdminPermissionsException("Access permissions are missing.");
            }

            return Facade.GetVIPCarOwnerById(id);
        }
 
    }
}