using BL;

namespace DB
{
    public class UserConverter
    {
        public static BL.User DBToBL(DB.User UserDB)
        {
            return new BL.User(UserDB.Id, UserDB.Name, UserDB.Surname, UserDB.Login, UserDB.Password, (BL.Permissions) UserDB.UserType);
        }

        public static DB.User BLToDB(BL.User UserBL)
        {
            DB.User user = new DB.User();
            // user.Id = UserBL.Id;
            user.Name = UserBL.Name;
            user.Surname = UserBL.Surname;
            user.Login = UserBL.Login;
            user.Password = UserBL.Password;
            user.UserType = (int) UserBL.UserType;

            return user;
        }
    }
}