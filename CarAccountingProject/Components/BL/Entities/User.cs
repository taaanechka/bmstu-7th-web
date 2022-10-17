namespace BL
{
    public enum Permissions: int
    {
        UNAUTHORIZED = 0,
        EMPLOYEE,
        ANALYST,
        ADMIN
    }
    public class User
    {
        public User (int id, string name, string surname, string login, string password, Permissions utype)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Login = login;
            Password = password;
            UserType = utype;
        }

        public int Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Login { get; }
        public string Password { get; }
        public Permissions UserType { get; }
    }
}
