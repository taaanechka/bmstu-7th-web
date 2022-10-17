#nullable disable

using BL;

namespace API
{
    public class UserConverter
    {
        public static BL.User APIToBL(API.User UserAPI)
        {
            return new BL.User(UserAPI.Id, UserAPI.Name, UserAPI.Surname, UserAPI.Login, null, (BL.Permissions) UserAPI.UserType);
        }

        public static API.User BLToAPI(BL.User UserBL)
        {
            API.User user = new API.User {
                Id = UserBL.Id,
                Name = UserBL.Name,
                Surname = UserBL.Surname,
                Login = UserBL.Login,
                UserType = (int) UserBL.UserType
            };

            return user;
        }
    }
}