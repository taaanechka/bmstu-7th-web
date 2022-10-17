using System.Threading.Tasks;

using BL;

#nullable disable

namespace TechnologicalUI
{
    class StartUp
    {
        private Presenter _presenter;

        private Permissions AccessPermissions = Permissions.UNAUTHORIZED;
        private BL.User _user;

        public StartUp(BL.Facade facade, BL.User user)
        {
            _presenter = new Presenter(facade, (int) user.UserType);
            AccessPermissions = user.UserType;
            _user = user;
        }

        public async Task Run()
        {
            switch ((int) AccessPermissions)
            {
                case 1:
                    await EmployeeFunctionality();
                    break;
                case 2:
                    AnalystFunctionality();
                    break;
                case 3:
                    await AdminFunctionality();
                    break;
                default:
                    Console.WriteLine("Выход из приложения выполнен\n");
                    break;
            }            
        }

        async Task EmployeeFunctionality()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\nВыберете действие:\n" + 
                    "0. Выход\n" +
                    "=========ПРИХОДЫ========\n" +
                    "10. Посмотреть приходы\n"+
                    "11. Получить приход по id\n"+
                    "12. Добавить приход\n" +
                    "==========УХОДЫ=========\n" +
                    "20. Посмотреть уходы\n" + 
                    "21. Получить уход по id\n"+
                    "22. Добавить уход\n" +
                    "=========ПРОЧЕЕ=========\n" +
                    "30. Посмотреть сотрудников\n" +
                    "31. Посмотреть автомобили\n" +
                    "32. Посмотреть автовладельцев\n" +
                    "33. Посмотреть связи владелец-автомобиль-уход\n" +
                    "34. Посмотреть модели автомобилей\n" +
                    "35. Посмотреть марки автомобилей\n" +
                    "36. Посмотреть комплектации автомобилей\n" +
                    "37. Посмотреть цвета автомобилей\n");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 10: // Посмотреть приходы
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetComings(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 11: // Получить приход по id
                        try
                        {
                            Console.WriteLine("Введите id прихода:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetComingById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректное значение id\n");
                        }
                        break;
                    case 12: // Добавить приход
                        try
                        {
                            Console.WriteLine("Введите номер автомобиля:");
                            string carId = Console.ReadLine();
                            Console.WriteLine("Введите Id модели:");
                            int modelId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите Id комплектации:");
                            int equipmentId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите Id цвета:");
                            int colorId = Convert.ToInt32(Console.ReadLine());

                            BL.Coming coming = new BL.Coming(0, _user.Id);
                            BL.Car car = new BL.Car(carId, modelId, equipmentId, colorId, 0);

                            await AddComingAsync(coming, car);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректные входные данные для добавления автомобиля\n");
                        }
                        break;
                    case 20: // Посмотреть уходы
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetDepartures(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 21: // Получить уход по id
                        try
                        {
                            Console.WriteLine("Введите id ухода:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetDepartureById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректное значение id\n");
                        }
                        break;
                    case 22: // Добавить уход
                        try
                        {
                            Console.WriteLine("Введите Id владельца автомобиля:");
                            int ownerId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите номер автомобиля:");
                            string carId = Console.ReadLine();

                            BL.Departure departure = new BL.Departure(0, _user.Id);
                            BL.LinkOwnerCarDeparture link = new BL.LinkOwnerCarDeparture(0, ownerId, carId, 0);

                            await AddDepartureAsync(departure, link);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректные входные данные для проведения ухода\n");
                        }
                        break;
                    case 30: // Посмотреть пользователей по типу пользователя
                        try
                        {
                            GetUsersByUserType((int) BL.Permissions.EMPLOYEE);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введено некорректное значение типа пользователя\n");
                        }
                        break;
                    case 31: // Посмотреть автомобили
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetCars(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 32: // Посмотреть автовладельцев
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetCarOwners(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 33: // Посмотреть связи владелец-автомобиль-уход
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetLinksOwnerCarDeparture(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 34: // Посмотреть модели автомобилей
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetModels(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 35: // Посмотреть марки автомобилей
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetBrands(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 36: // Посмотреть комплектации автомобилей
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetEquipments(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 37: // Посмотреть цвета автомобилей
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetColors(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 0: // Выход
                        LogOut();
                        Console.WriteLine("Выход из учетной записи\n");
                        break;
                    default:
                        Console.WriteLine("Введен неверный пункт меню\n");
                        break;
                }
            }
        }

        void AnalystFunctionality()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\nВыберете действие:\n" + 
                    "0. Выход\n" +
                    "1. Посмотреть пользователей по типу пользователя\n" +
                    "===============ПРИХОДЫ==================\n" +
                    "10. Посмотреть приходы\n" +
                    "11. Получить приход по id\n" +
                    "12. Получить приходы по дате\n" +
                    "13. Получить приходы за период\n" +
                    "14. Получить приход по id пользователя\n" +
                    "==================УХОДЫ==================\n" +
                    "20. Посмотреть уходы\n" +
                    "21. Получить уход по id\n" +
                    "22. Получить уходы по дате\n" +
                    "23. Получить уходы за период\n" +
                    "24. Получить уход по id пользователя\n" +
                    "==========НЕПРОДАННЫЕ АВТОМОБИЛИ==========\n" +
                    "30. Получить непроданные автомобили\n" +
                    "31. Получить непроданный автомобиль по его номеру\n");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: // Посмотреть пользователей по типу пользователя
                        try
                        {
                            Console.WriteLine("Введите тип пользователей (1 - сотрудник, 2 - аналитик):");
                            int type = Convert.ToInt32(Console.ReadLine());
                            GetUsersByUserType(type);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введено некорректное значение типа пользователя\n");
                        }
                        break;
                    case 10: // Посмотреть приходы
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetComings(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 11: // Получить приход по id
                        try
                        {
                            Console.WriteLine("Введите id прихода:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetComingById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректное значение id\n");
                        }
                        break;
                    case 12: // Получить приходы по дате
                        try
                        {
                            Console.WriteLine("Введите дату (yyyy-MM-dd):");
                            string datep = Console.ReadLine();
                            GetComingsByDate(datep);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Дата введена неверно\n");
                        }
                        break;
                    case 13: // Получить приходы за период
                        try
                        {
                            Console.WriteLine("Введите начальную дату (yyyy-MM-dd):");
                            string date1 = Console.ReadLine();
                            Console.WriteLine("Введите конечную дату (yyyy-MM-dd):");
                            string date2 = Console.ReadLine();
                            GetComingsBetweenDates(date1, date2);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Даты введены неверно\n");
                        }
                        break;
                    case 14: // Получить приходы по id пользователя
                        try
                        {
                            Console.WriteLine("Введите id пользователя:");
                            int userId = Convert.ToInt32(Console.ReadLine());
                            GetComingsByUserId(userId);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("id пользователя введен неверно\n");
                        }
                        break;
                    case 20: // Посмотреть уходы
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetDepartures(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 21: // Получить уход по id
                        try
                        {
                            Console.WriteLine("Введите id прихода:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetDepartureById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректное значение id\n");
                        }
                        break;
                    case 22: // Получить уходы по дате
                        try
                        {
                            Console.WriteLine("Введите дату (yyyy-MM-dd):");
                            string datep = Console.ReadLine();
                            GetDeparturesByDate(datep);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Дата введена неверно\n");
                        }
                        break;
                    case 23: // Получить уходы за период
                        try
                        {
                            Console.WriteLine("Введите начальную дату (yyyy-MM-dd):");
                            string date1 = Console.ReadLine();
                            Console.WriteLine("Введите конечную дату (yyyy-MM-dd):");
                            string date2 = Console.ReadLine();
                            GetDeparturesBetweenDates(date1, date2);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Даты введены неверно\n");
                        }
                        break;
                    case 24: // Получить уходы по id пользователя
                        try
                        {
                            Console.WriteLine("Введите id пользователя:");
                            int userId = Convert.ToInt32(Console.ReadLine());
                            GetDeparturesByUserId(userId);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("id пользователя введен неверно\n");
                        }
                        break;
                    case 30:
                        GetCarsNotInLinksOwnerCarDeparture();
                        break;
                    case 31:
                        try
                        {
                            Console.WriteLine("Введите номер автомобиля:");
                            string id = Console.ReadLine();
                            GetCarIfNotInLinksOwnerCarDeparture(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Неверный номер автомобиля\n");
                        }                        
                        break;
                    case 0:
                        LogOut();
                        Console.WriteLine("Выход из учетной записи\n");
                        break;
                    default:
                        Console.WriteLine("Введен неверный пункт меню\n");
                        break;
                }
            }
        }

        async Task AdminFunctionality()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\nВыберете действие / секцию:\n" + 
                    "0. Выход\n" +
                    "================================\n" +
                    "1. Пользователи\n" +
                    "2. Приходы / уходы\n" +
                    "3. Автомобили и информация о них\n" +
                    "4. Владельцы автомобилей\n");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: // Пользователи
                        await AdminUsersMenu();
                        break;
                    case 2: // Приходы / уходы
                        await AdminComingsDeparturesMenu();
                        break;
                    case 3: // Автомобили и информация о них
                        await AdminCarsMenu();
                        break;
                    case 4: // Владельцы автомобилей
                        AdminCarOwnersMenu();
                        break;
                    case 0: // Exit
                        LogOut();
                        Console.WriteLine("Выход из учетной записи\n");
                        break;
                    default:
                        Console.WriteLine("Введен неверный пункт меню\n");
                        break;
                }
            }
        }

        async Task AdminUsersMenu()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\nВыберете действие:\n" + 
                    "0. Выход\n" +
                    "==========ПОЛЬЗОВАТЕЛИ==========\n" +
                    "1. Посмотреть пользователей\n" +
                    "2. Посмотреть пользователей по типу пользователя\n" +
                    "3. Получить пользователя по id\n" +
                    "4. Добавить пользователя\n" +
                    "5. Обновить пользователя\n" +
                    "6. Заблокировать пользователя по id\n");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        break;
                    case 1: // Посмотреть пользователей
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetUsers(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 2: // Посмотреть пользователей по типу пользователя
                        try
                        {
                            Console.WriteLine("Введите тип пользователей (0 - заблокированные, 1 - сотрудник, 2 - аналитик, 3 - админ):");
                            int type = Convert.ToInt32(Console.ReadLine());
                            GetUsersByUserType(type);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введено некорректное значение типа пользователя\n");
                        }
                        break;
                    case 3: // Получить пользователя по id
                        try
                        {
                            Console.WriteLine("Введите id пользователя:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetUserById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 4: // Добавить пользователя
                        try
                        {
                            Console.WriteLine("Введите имя пользователя:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Введите фамилию пользователя:");
                            string surname = Console.ReadLine();
                            Console.WriteLine("Введите логин пользователя:");
                            string login = Console.ReadLine();
                            Console.WriteLine("Введите пароль пользователя:");
                            string password = Console.ReadLine();
                            Console.WriteLine("Введите тип пользователя (1 - сотрудник; 2 - аналитик; 3 - админ):");
                            int type = Convert.ToInt32(Console.ReadLine());

                            BL.User user = new BL.User(0, name, surname, login, password, (BL.Permissions) type);
                            await AddUserAsync(user);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 5: // Обновить пользователя
                        try
                        {
                            Console.WriteLine("Введите id пользователя:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите новое имя пользователя:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Введите новую фамилию пользователя:");
                            string surname = Console.ReadLine();
                            Console.WriteLine("Введите новый логин пользователя:");
                            string login = Console.ReadLine();
                            Console.WriteLine("Введите новый пароль пользователя:");
                            string password = Console.ReadLine();

                            BL.User updUser = new BL.User(id, name, surname, login, password, (Permissions) 1);
                            await UpdateUserAsync(id, updUser);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введенные данные некорректны\n");
                        }
                        break;
                    case 6: // Заблокировать пользователя по id
                        try
                        {
                            Console.WriteLine("Введите id пользователя:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await BlockUserAsync(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    default:
                        Console.WriteLine("Введен неверный пункт меню\n");
                        break;
                }
            }
        }

        async Task AdminComingsDeparturesMenu()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\nВыберете действие:\n" + 
                    "0. Выход\n" +
                    "============ПРИХОДЫ=============\n" +
                    "10. Посмотреть приходы\n" +
                    "11. Получить приход по id\n" +
                    "12. Удалить приход по id\n" + 
                    "==============УХОДЫ=============\n" +
                    "20. Посмотреть уходы\n" + 
                    "21. Получить уход по id\n" + 
                    "22. Удалить уход по id\n");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        break;
                    case 10: // Посмотреть приходы
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetComings(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 11: // Получить приход по id
                        try
                        {
                            Console.WriteLine("Введите id прихода:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetComingById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректное значение id\n");
                        }
                        break;
                    case 12: // Удалить приход по id
                        try
                        {
                            Console.WriteLine("Введите id прихода:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await DeleteComingAsync(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректное значение id\n");
                        }
                        break;
                    case 20: // Посмотреть уходы
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetDepartures(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 21: // Получить уход по id
                        try
                        {
                            Console.WriteLine("Введите id ухода:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetDepartureById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректное значение id\n");
                        }
                        break;
                    case 22: // Удалить уход по id
                        try
                        {
                            Console.WriteLine("Введите id ухода:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await DeleteDepartureAsync(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректное значение id\n");
                        }
                        break;
                    default:
                        Console.WriteLine("Введен неверный пункт меню\n");
                        break;
                }
            }
        }

        async Task AdminCarsMenu()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\nВыберете действие:\n" + 
                    "0. Выход\n" +
                    "===========АВТОМОБИЛИ============\n" +
                    "10. Посмотреть автомобили\n" + 
                    "11. Получить автомобиль по номеру\n" +
                    "12. Обновить автомобиль\n" +
                    "=============МОДЕЛИ==============\n" +
                    "20. Посмотреть модели автомобилей\n" +
                    "21. Получить модель по id\n" +
                    "22. Обновить модель автомобиля\n" +
                    "23. Добавить модель автомобиля\n" +
                    "=============МАРКИ===============\n" +
                    "30. Посмотреть марки автомобилей\n" +
                    "31. Получить марку по id\n" +
                    "32. Обновить марку автомобиля\n" +
                    "33. Добавить марку автомобиля\n" +
                    "==========КОМПЛЕКТАЦИИ===========\n" +
                    "40. Посмотреть комплектации автомобилей\n" +
                    "41. Получить комплектацию по id\n" +
                    "42. Обновить комплектацию автомобиля\n" +
                    "43. Добавить комплектацию автомобиля\n" +
                    "=============ЦВЕТА===============\n" +
                    "50. Посмотреть цвета\n" + 
                    "51. Получить цвет по id\n" +
                    "52. Обновить цвет\n" +
                    "53. Добавить цвет\n");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        break;
                    case 10: // Посмотреть автомобили
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetCars(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 11: // Получить автомобиль по номеру
                        try
                        {
                            Console.WriteLine("Введите номер автомобиля:");
                            string id = Console.ReadLine();
                            GetCarById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректный номер автомобиля\n");
                        }
                        break;
                    case 12: // Обновить автомобиль
                        try
                        {
                            Console.WriteLine("Введите номер автомобиля:");
                            string carId = Console.ReadLine();
                            Console.WriteLine("Введите Id комплектации:");
                            int equipmentId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите Id цвета:");
                            int colorId = Convert.ToInt32(Console.ReadLine());

                            BL.Car car = new BL.Car(carId, 1, equipmentId, colorId, 1);

                            await UpdateCarAsync(carId, car);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректные входные данные для добавления автомобиля\n");
                        }
                        break;
                    case 20: // Посмотреть модели автомобилей
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetModels(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 21: // Получить модель по id
                        try
                        {
                            Console.WriteLine("Введите id модели:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetModelById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректный id модели\n");
                        }
                        break;
                    case 22: // Обновить модель
                        try
                        {
                            Console.WriteLine("Введите id модели:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите новое имя модели:");
                            string name = Console.ReadLine();

                            BL.Model model = new BL.Model(id, 1, name);
                            UpdateModel(id, model);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введены некорректные данные для обновления модели\n");
                        }
                        break;
                    case 23: // Добавить модель
                        try
                        {
                            Console.WriteLine("Введите id марки:");
                            int brandId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите имя модели:");
                            string name = Console.ReadLine();

                            BL.Model model = new BL.Model(0, brandId, name);
                            AddModel(model);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введены некорректные данные для новой модели\n");
                        }
                        break;
                    case 30: // Посмотреть марки автомобилей
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetBrands(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 31: // Получить марку по id
                        try
                        {
                            Console.WriteLine("Введите id марки:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetBrandById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректный id марки\n");
                        }
                        break;
                    case 32: // Обновить марку
                        try
                        {
                            Console.WriteLine("Введите id марки:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите новое имя марки:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Введите новый код страны-производителя (XX):");
                            string manufactCountry = Console.ReadLine();
                            Console.WriteLine("Введите новое положение руля (right / left):");
                            string wheel = Console.ReadLine();

                            BL.Brand brand = new BL.Brand(id, name, manufactCountry, wheel);
                            UpdateBrand(id, brand);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введены некорректные данные для обновления марки\n");
                        }
                        break;
                    case 33: // Добавить марку
                        try
                        {
                            Console.WriteLine("Введите имя марки:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Введите код страны-производителя (XX):");
                            string manufactCountry = Console.ReadLine();
                            Console.WriteLine("Введите положение руля (right / left):");
                            string wheel = Console.ReadLine();

                            BL.Brand brand = new BL.Brand(0, name, manufactCountry, wheel);
                            AddBrand(brand);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введены некорректные данные для новой марки\n");
                        }
                        break;
                    case 40: // Посмотреть комплектации автомобилей
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetEquipments(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 41: // Получить комплектацию по id
                        try
                        {
                            Console.WriteLine("Введите id комплектации:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetEquipmentById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректный id комплектации\n");
                        }
                        break;
                    case 42: // Обновить комплектацию
                        try
                        {
                            Console.WriteLine("Введите id комплектации:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите новую категорию:");
                            string category = Console.ReadLine();
                            Console.WriteLine("Введите новый тип привода:");
                            string gear = Console.ReadLine();
                            Console.WriteLine("Введите новый тип крыши:");
                            string roofType = Console.ReadLine();

                            BL.Equipment equipment = new BL.Equipment(id, category, gear, roofType);
                            UpdateEquipment(id, equipment);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введены некорректные данные для новой комплектации\n");
                        }
                        break;
                    case 43: // Добавить комплектацию
                        try
                        {
                            Console.WriteLine("Введите категорию:");
                            string category = Console.ReadLine();
                            Console.WriteLine("Введите тип привода:");
                            string gear = Console.ReadLine();
                            Console.WriteLine("Введите тип крыши:");
                            string roofType = Console.ReadLine();

                            BL.Equipment equipment = new BL.Equipment(0, category, gear, roofType);
                            AddEquipment(equipment);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введены некорректные данные для новой комплектации\n");
                        }
                        break;
                    case 50: // Посмотреть цвета
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetColors(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 51: // Получить цвет по id
                        try
                        {
                            Console.WriteLine("Введите id цвета:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetColorById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректный id цвета\n");
                        }
                        break;
                    case 52: // Обновить цвет
                        try
                        {
                            Console.WriteLine("Введите id цвета:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите новое название цвета:");
                            string name = Console.ReadLine();

                            BL.Color color = new BL.Color(id, name);
                            UpdateColor(id, color);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введены некорректные данные для нового цвета\n");
                        }
                        break;
                    case 53: // Добавить цвет
                        try
                        {
                            Console.WriteLine("Введите название цвета:");
                            string name = Console.ReadLine();

                            BL.Color color = new BL.Color(0, name);
                            AddColor(color);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введены некорректные данные для нового цвета\n");
                        }
                        break;
                    default:
                        Console.WriteLine("Введен неверный пункт меню\n");
                        break;
                }
            }
        }

        void AdminCarOwnersMenu()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\nВыберете действие:\n" + 
                    "0. Выход\n" +
                    "===========АВТОВЛАДЕЛЬЦЫ============\n" +
                    "1. Посмотреть владельцев автомобилей\n" +
                    "2. Получить автовладельца по id\n" +
                    "3. Обновить автовладельца\n" +
                    "4. Добавить владельца автомобиля\n");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        break;
                    case 1: // Посмотреть владельцев автомобилей
                        try
                        {
                            Console.WriteLine("Введите offset:");
                            int offset = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите limit:");
                            int limit = Convert.ToInt32(Console.ReadLine());
                            GetCarOwners(offset, limit);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 2: // Получить автовладельца по id
                        try
                        {
                            Console.WriteLine("Введите id автовладельца:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            GetCarOwnerById(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректный id автовладельца\n");
                        }
                        break;
                    case 3: // Обновить владельца автомобиля
                        try
                        {
                            Console.WriteLine("Введите id автовладельца:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите новое имя автовладельца:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Введите новую фамилию автовладельца:");
                            string surname = Console.ReadLine();
                            Console.WriteLine("Введите новую почту автовладельца:");
                            string email = Console.ReadLine();

                            BL.CarOwner owner = new BL.CarOwner(0, name, surname, email);
                            UpdateCarOwner(id, owner);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    case 4: // Добавить владельца автомобиля
                        try
                        {
                            Console.WriteLine("Введите имя автовладельца:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Введите фамилию автовладельца:");
                            string surname = Console.ReadLine();
                            Console.WriteLine("Введите почту автовладельца:");
                            string email = Console.ReadLine();

                            BL.CarOwner owner = new BL.CarOwner(0, name, surname, email);
                            AddCarOwner(owner);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Пределы введены неверно\n");
                        }
                        break;
                    default:
                        Console.WriteLine("Введен неверный пункт меню\n");
                        break;
                }
            }
        }

        // !!EmployeeFunctionality
        void GetComings(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.Coming> res = _presenter.GetComings(offset, limit);

                Console.WriteLine("Результат:");
                foreach(var coming in res)
                {
                    Console.WriteLine(
                        Convert.ToString(coming.Id) + " " +
                        Convert.ToString(coming.UserId) + " " +
                        Convert.ToString(coming.ComingDate));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Приходы не найдены");
            }
        }

        void GetComingById(int id)
        {
            try
            {
                BL.Coming res = _presenter.GetComingById(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.UserId) + " " +
                    Convert.ToString(res.ComingDate));
            }
            catch (Exception)
            {
                Console.WriteLine("Приход не найден");
            }
        }

        void GetDepartures(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.Departure> res = _presenter.GetDepartures(offset, limit);

                Console.WriteLine("Результат:");
                foreach(var departure in res)
                {
                    Console.WriteLine(
                        Convert.ToString(departure.Id) + " " +
                        Convert.ToString(departure.UserId) + " " +
                        Convert.ToString(departure.DepartureDate));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Уходы не найдены");
            }
        }

        void GetDepartureById(int id)
        {
            try
            {
                BL.Departure res = _presenter.GetDepartureById(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.UserId) + " " +
                    Convert.ToString(res.DepartureDate));
            }
            catch (Exception)
            {
                Console.WriteLine("Уход не найден");
            }
        }

        void GetLinksOwnerCarDeparture(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.LinkOwnerCarDeparture> res = _presenter.GetLinksOwnerCarDeparture(offset, limit);

                Console.WriteLine("Результат:");
                foreach(var link in res)
                {
                    Console.WriteLine(
                        Convert.ToString(link.Id) + " " +
                        Convert.ToString(link.OwnerId) + " " +
                        Convert.ToString(link.CarId) + " " +
                        Convert.ToString(link.DepartureId));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Связи владелец-автомобиль-уход не найдены");
            }
        }

        void GetLinkOwnerCarDepartureById(int id)
        {
            try
            {
                BL.LinkOwnerCarDeparture res = _presenter.GetLinkOwnerCarDepartureById(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.OwnerId) + " " +
                    Convert.ToString(res.CarId) + " " +
                    Convert.ToString(res.DepartureId));
            }
            catch (Exception)
            {
                Console.WriteLine("Связь владелец-автомобиль-уход не найдена");
            }
        }


        // !!AnalystFunctionality

        void GetCarsNotInLinksOwnerCarDeparture()
        {
            try
            {
                List<BL.Car> res = _presenter.GetCarsNotInLinksOwnerCarDeparture();

                Console.WriteLine("Результат:");
                foreach(var car in res)
                {
                    Console.WriteLine(
                        Convert.ToString(car.Id) + " " +
                        Convert.ToString(car.ModelId) + " " +
                        Convert.ToString(car.EquipmentId) + " " +
                        Convert.ToString(car.ColorId) + " " +
                        Convert.ToString(car.ComingId));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Непроданных автомобилей в автосалоне не осталось");
            }
        }

        void GetCarIfNotInLinksOwnerCarDeparture(string id)
        {
            try
            {
                BL.Car car = _presenter.GetCarIfNotInLinksOwnerCarDeparture(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(car.Id) + " " +
                    Convert.ToString(car.ModelId) + " " +
                    Convert.ToString(car.EquipmentId) + " " +
                    Convert.ToString(car.ColorId) + " " +
                    Convert.ToString(car.ComingId));
            }
            catch (Exception)
            {
                Console.WriteLine("Такого автомобиля нет среди непроданных в автосалоне");
            }
        }

        void GetComingsByDate(string datep)
        {
            try
            {
                DateTime Date1 = DateTime.ParseExact(datep, "yyyy-MM-dd",
                                    System.Globalization.CultureInfo.InvariantCulture);
                Date1 = DateTime.SpecifyKind(Date1, DateTimeKind.Utc); // Перевод даты в тип Utc 

                List<BL.Coming> res = _presenter.GetComingsByDate(Date1);
                
                Console.WriteLine("Результат:");
                foreach(var coming in res)
                {
                    Console.WriteLine(
                        Convert.ToString(coming.Id) + " " +
                        Convert.ToString(coming.UserId) + " " +
                        Convert.ToString(coming.ComingDate));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Приходы не найдены");
            }
        }

        void GetComingsBetweenDates(string date1, string date2)
        {
            try
            {
                DateTime Date1 = DateTime.ParseExact(date1, "yyyy-MM-dd",
                                    System.Globalization.CultureInfo.InvariantCulture);
                Date1 = DateTime.SpecifyKind(Date1, DateTimeKind.Utc); // Перевод даты в тип Utc 
                DateTime Date2 = DateTime.ParseExact(date2, "yyyy-MM-dd",
                                    System.Globalization.CultureInfo.InvariantCulture);
                Date2 = DateTime.SpecifyKind(Date2, DateTimeKind.Utc); // Перевод даты в тип Utc 

                List<BL.Coming> res = _presenter.GetComingsBetweenDates(Date1, Date2);
                
                Console.WriteLine("Результат:");
                foreach(var coming in res)
                {
                    Console.WriteLine(
                        Convert.ToString(coming.Id) + " " +
                        Convert.ToString(coming.UserId) + " " +
                        Convert.ToString(coming.ComingDate));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Приходы не найдены");
            }
        }

        void GetComingsByUserId(int id)
        {
            try
            {
                List<BL.Coming> res = _presenter.GetComingsByUserId(id);
                
                Console.WriteLine("Результат:");
                foreach(var coming in res)
                {
                    Console.WriteLine(
                        Convert.ToString(coming.Id) + " " +
                        Convert.ToString(coming.UserId) + " " +
                        Convert.ToString(coming.ComingDate));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Приходы не найдены");
            }
        }

        void GetDeparturesByDate(string datep)
        {
            try
            {
                DateTime Date1 = DateTime.ParseExact(datep, "yyyy-MM-dd",
                                    System.Globalization.CultureInfo.InvariantCulture);
                Date1 = DateTime.SpecifyKind(Date1, DateTimeKind.Utc); // Перевод даты в тип Utc 

                List<BL.Departure> res = _presenter.GetDeparturesByDate(Date1);

                Console.WriteLine("Результат:");
                foreach(var departure in res)
                {
                    Console.WriteLine(
                        Convert.ToString(departure.Id) + " " +
                        Convert.ToString(departure.UserId) + " " +
                        Convert.ToString(departure.DepartureDate));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Уходы не найдены");
            }
        }

        void GetDeparturesBetweenDates(string date1, string date2)
        {
            try
            {
                DateTime Date1 = DateTime.ParseExact(date1, "yyyy-MM-dd",
                                    System.Globalization.CultureInfo.InvariantCulture);
                Date1 = DateTime.SpecifyKind(Date1, DateTimeKind.Utc); // Перевод даты в тип Utc
                DateTime Date2 = DateTime.ParseExact(date2, "yyyy-MM-dd",
                                    System.Globalization.CultureInfo.InvariantCulture);
                Date2 = DateTime.SpecifyKind(Date2, DateTimeKind.Utc); // Перевод даты в тип Utc

                List<BL.Departure> res = _presenter.GetDeparturesBetweenDates(Date1, Date2);

                Console.WriteLine("Результат:");
                foreach(var departure in res)
                {
                    Console.WriteLine(
                        Convert.ToString(departure.Id) + " " +
                        Convert.ToString(departure.UserId) + " " +
                        Convert.ToString(departure.DepartureDate));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Уходы не найдены");
            }
        }

        void GetDeparturesByUserId(int id)
        {
            try
            {
                List<BL.Departure> res = _presenter.GetDeparturesByUserId(id);
                
                Console.WriteLine("Результат:");
                foreach(var departure in res)
                {
                    Console.WriteLine(
                        Convert.ToString(departure.Id) + " " +
                        Convert.ToString(departure.UserId) + " " +
                        Convert.ToString(departure.DepartureDate));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Уходы не найдены");
            }
        }

        async Task AddComingAsync(BL.Coming coming, BL.Car car)
        {
            try
            {
                await _presenter.AddComingAsync(coming, car);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка проведения прихода");
            }
        }

        async Task DeleteComingAsync(int id)
        {
            try
            {
                await _presenter.DeleteComingAsync(id);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка удаления прихода");
            }
        }

        async Task AddDepartureAsync(BL.Departure departure, BL.LinkOwnerCarDeparture link)
        {
            try
            {
                await _presenter.AddDepartureAsync(departure, link);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка проведения ухода");
            }
        }

        async Task DeleteDepartureAsync(int id)
        {
            try
            {
                await _presenter.DeleteDepartureAsync(id);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка удаления ухода");
            }
        }

        // !!AdminFunctionality
        void GetUsers(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.User> res = _presenter.GetUsers(offset, limit);
            
                Console.WriteLine("Результат:");
                foreach(var user in res)
                {
                    Console.WriteLine(
                        Convert.ToString(user.Id) + " " +
                        Convert.ToString(user.Name) + " " +
                        Convert.ToString(user.Surname) + " " +
                        Convert.ToString(user.Login) + " " +
                        Convert.ToString(user.UserType));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Пользователи не найдены");
            }
        }

        void GetUsersByUserType(int type)
        {
            try
            {
                List<BL.User> res = _presenter.GetUsersByUserType(type);
            
                Console.WriteLine("Результат:");
                foreach(var user in res)
                {
                    Console.WriteLine(
                        Convert.ToString(user.Id) + " " +
                        Convert.ToString(user.Name) + " " +
                        Convert.ToString(user.Surname) + " " +
                        Convert.ToString(user.Login) + " " +
                        Convert.ToString(user.UserType));
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Пользователи типа {(BL.Permissions) type} не найдены");
            }
        }

        void GetUserById(int id)
        {
            try
            {
                BL.User res = _presenter.GetUserById(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.Name) + " " +
                    Convert.ToString(res.Surname) + " " +
                    Convert.ToString(res.Login) + " " +
                    Convert.ToString(res.UserType));
            }
            catch (Exception)
            {
                Console.WriteLine("Пользователь не найден");
            }
            
        }

        async Task AddUserAsync(BL.User user)
        {
            try
            {
                await _presenter.AddUserAsync(user);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка добавления пользователя");
            }
        }

        async Task UpdateUserAsync(int id, BL.User updUser)
        {
            try
            {
                await _presenter.UpdateUserAsync(id, updUser);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка обновления пользователя");
            }
        }

        async Task BlockUserAsync(int id)
        {
            try
            {
                await _presenter.BlockUserAsync(id);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка блокировки пользователя");
            }
        }

        void GetCars(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.Car> res = _presenter.GetCars(offset, limit);
            
                Console.WriteLine("Результат:");
                foreach(var car in res)
                {
                    Console.WriteLine(
                        Convert.ToString(car.Id) + " " +
                        Convert.ToString(car.ModelId) + " " +
                        Convert.ToString(car.EquipmentId) + " " +
                        Convert.ToString(car.ColorId) + " " +
                        Convert.ToString(car.ComingId));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Автомобили не найдены");
            }
        }

        void GetCarById(string id)
        {
            try
            {
                BL.Car res = _presenter.GetCarById(id);

                Console.WriteLine("Результат:");                
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.ModelId) + " " +
                    Convert.ToString(res.EquipmentId) + " " +
                    Convert.ToString(res.ColorId) + " " +
                    Convert.ToString(res.ComingId));
            }
            catch (Exception)
            {
                Console.WriteLine("Автомобиль не найден");
            }
            
        }

        async Task UpdateCarAsync(string id, BL.Car newCar)
        {
            try
            {
                await _presenter.UpdateCarAsync(id, newCar);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка обновления автомобиля");
            }
        }

        void GetColors(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.Color> res = _presenter.GetColors(offset, limit);
                
                Console.WriteLine("Результат:");
                foreach(var color in res)
                {
                    Console.WriteLine(
                        Convert.ToString(color.Id) + " " +
                        Convert.ToString(color.Name));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Цвета не найдены");
            }
        }

        void GetColorById(int id)
        {
            try
            {
                BL.Color res = _presenter.GetColorById(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.Name));
            }
            catch (Exception)
            {
                Console.WriteLine("Цвет не найден");
            }
            
        }

        void AddColor(BL.Color col)
        {
            try
            {
                _presenter.AddColor(col);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка добавления цвета");
            }
        }

        void UpdateColor(int id, BL.Color newColor)
        {
            try
            {
                _presenter.UpdateColor(id, newColor);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка обновления цвета");
            }
        }

        void GetModels(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.Model> res = _presenter.GetModels(offset, limit);
                
                Console.WriteLine("Результат:");
                foreach(var model in res)
                {
                    Console.WriteLine(
                        Convert.ToString(model.Id) + " " +
                        Convert.ToString(model.BrandId) + " " +
                        Convert.ToString(model.Name));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Модели автомобилей не найдены");
            }
        }

        void GetModelById(int id)
        {
            try
            {
                BL.Model res = _presenter.GetModelById(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.BrandId) + " " +
                    Convert.ToString(res.Name));
            }
            catch (Exception)
            {
                Console.WriteLine("Модель не найдена");
            }
            
        }

        void AddModel(BL.Model model)
        {
            try
            {
                _presenter.AddModel(model);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка добавления модели");
            }
        }

        void UpdateModel(int id, BL.Model newModel)
        {
            try
            {
                _presenter.UpdateModel(id, newModel);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка обновления модели");
            }
        }

        void GetBrands(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.Brand> res = _presenter.GetBrands(offset, limit);
                
                Console.WriteLine("Результат:");
                foreach(var brand in res)
                {
                    Console.WriteLine(
                        Convert.ToString(brand.Id) + " " +
                        Convert.ToString(brand.Name) + " " +
                        Convert.ToString(brand.ManufactCountry) + " " +
                        Convert.ToString(brand.Wheel));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Марки не найдены");
            }
        }

        void GetBrandById(int id)
        {
            try
            {
                BL.Brand res = _presenter.GetBrandById(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.Name) + " " +
                    Convert.ToString(res.ManufactCountry) + " " +
                    Convert.ToString(res.Wheel));
            }
            catch (Exception)
            {
                Console.WriteLine("Марка не найдена");
            }
            
        }

        void AddBrand(BL.Brand brand)
        {
            try
            {
                _presenter.AddBrand(brand);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка добавления марки");
            }
        }

        void UpdateBrand(int id, BL.Brand newBrand)
        {
            try
            {
                _presenter.UpdateBrand(id, newBrand);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка обновления марки");
            }
        }

        void GetEquipments(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.Equipment> res = _presenter.GetEquipments(offset, limit);
                
                Console.WriteLine("Результат:");
                foreach(var equipment in res)
                {
                    Console.WriteLine(
                        Convert.ToString(equipment.Id) + " " +
                        Convert.ToString(equipment.Category) + " " +
                        Convert.ToString(equipment.Gear) + " " +
                        Convert.ToString(equipment.RoofType));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Комплектации не найдены");
            }
        }

        void GetEquipmentById(int id)
        {
            try
            {
                BL.Equipment res = _presenter.GetEquipmentById(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.Category) + " " +
                    Convert.ToString(res.Gear) + " " +
                    Convert.ToString(res.RoofType));
            }
            catch (Exception)
            {
                Console.WriteLine("Комплектация не найдена");
            }
            
        }

        void AddEquipment(BL.Equipment equipment)
        {
            try
            {
                _presenter.AddEquipment(equipment);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка добавления комплектации");
            }
        }

        void UpdateEquipment(int id, BL.Equipment newEquipment)
        {
            try
            {
                _presenter.UpdateEquipment(id, newEquipment);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка обновления комплектации");
            }
        }

        void GetCarOwners(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.CarOwner> res = _presenter.GetCarOwners(offset, limit);
                
                Console.WriteLine("Результат:");
                foreach(var owner in res)
                {
                    Console.WriteLine(
                        Convert.ToString(owner.Id) + " " +
                        Convert.ToString(owner.Name) + " " +
                        Convert.ToString(owner.Surname) + " " +
                        Convert.ToString(owner.Email));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Владельцы автомобилей не найдены");
            }
        }

        void GetCarOwnerById(int id)
        {
            try
            {
                BL.CarOwner res = _presenter.GetCarOwnerById(id);

                Console.WriteLine("Результат:");
                Console.WriteLine(
                    Convert.ToString(res.Id) + " " +
                    Convert.ToString(res.Name) + " " +
                    Convert.ToString(res.Surname) + " " +
                    Convert.ToString(res.Email));
            }
            catch (Exception)
            {
                Console.WriteLine("Автовладелец не найдена");
            }
            
        }

        void AddCarOwner(BL.CarOwner owner)
        {
            try
            {
                _presenter.AddCarOwner(owner);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка добавления марки");
            }
        }

        void UpdateCarOwner(int id, BL.CarOwner newCarOwner)
        {
            try
            {
                _presenter.UpdateCarOwner(id, newCarOwner);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка обновления автовладельца");
            }
        }

        void GetVIPCarOwners(int offset = 0, int limit = -1)
        {
            try
            {
                List<BL.VIPCarOwner> res = _presenter.GetVIPCarOwners(offset, limit);
                
                Console.WriteLine("Результат:");
                foreach(var owner in res)
                {
                    Console.WriteLine(
                        Convert.ToString(owner.Id) + " " +
                        Convert.ToString(owner.CarOwnerId));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("VIP Владельцы автомобилей не найдены");
            }
        }

        void LogOut()
        {
            _presenter.LogOut();
        }
    }
}