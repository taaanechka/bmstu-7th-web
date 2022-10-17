namespace BL
{
    public class UsersValidator
    {
        public static void ValidateUser(User user)
        {
            if (user == null)
            // if (user == null ||
            //     user.Name.Length == 0 ||
            //     user.Surname.Length == 0 ||
            //     user.Login.Length == 0 ||
            //     user.Password.Length == 0 ||
            //     user.UserType < 0)
            {
                throw new UsersValidatorFailException();
            }
        }
    }
}