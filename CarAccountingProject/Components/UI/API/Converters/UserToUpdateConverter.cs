#nullable disable

using BL;

namespace API
{
    public class UserToUpdateConverter
    {
        public static BL.User APIToBL(API.UserToUpdate UserAPI)
        {
            return new BL.User(0, UserAPI.Name, UserAPI.Surname, UserAPI.Login, UserAPI.Password, (BL.Permissions) 0);
        }

        public static API.UserToUpdate BLToAPI(BL.User UserBL)
        {
            API.UserToUpdate user = new API.UserToUpdate {
                Name = UserBL.Name,
                Surname = UserBL.Surname,
                Login = UserBL.Login,
                Password = UserBL.Password
            };

            return user;
        }
    }
}