#nullable disable

using BL;

namespace API
{
    public class UserToAddConverter
    {
        public static BL.User APIToBL(API.UserToAdd UserAPI)
        {
            return new BL.User(0, UserAPI.Name, UserAPI.Surname, UserAPI.Login, UserAPI.Password, (BL.Permissions) UserAPI.UserType);
        }

        public static API.UserToAdd BLToAPI(BL.User UserBL)
        {
            API.UserToAdd user = new API.UserToAdd {
                Name = UserBL.Name,
                Surname = UserBL.Surname,
                Login = UserBL.Login,
                Password = UserBL.Password,
                UserType = (int)UserBL.UserType
            };

            return user;
        }
    }
}