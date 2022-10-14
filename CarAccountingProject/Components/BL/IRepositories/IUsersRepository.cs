using System;
using System.Collections.Generic;

namespace BL
{
    public interface IUsersRepository
    {
        List<User> GetUsers(int offset = 0, int limit = -1);
        List<User> GetUsersByName(string name);
        List<User> GetUsersBySurname(string surname);
        List<User> GetUsersByUserType(int type);
        User GetUserByLogin(string login);
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(int id, User newUser);
        void BlockUser(int id);
    }
}