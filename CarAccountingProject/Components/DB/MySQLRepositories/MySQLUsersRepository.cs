using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class MySQLUsersRepository: BL.IUsersRepository
    {
        private MySQLApplicationContext db;

        public MySQLUsersRepository(MySQLApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.User> GetUsers(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var users = db.Users.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.Users.Count())
            {
                users = users.Take(limit);
            }
            
            var usersDB = users.AsNoTracking().ToList();

            List<BL.User> res = new List<BL.User>();

            foreach (var elem in usersDB)
            {
                res.Add(UserConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.User> GetUsersByName(string name)
        {
            List<User> usersDB = (from p in db.Users.AsNoTracking()
                        where p.Name == name
                        select p).ToList();

            List<BL.User> res = new List<BL.User>();

            foreach (var elem in usersDB)
            {
                res.Add(UserConverter.DBToBL(elem));
            }

            return res;     
        }

        public List<BL.User> GetUsersBySurname(string surname)
        {
            List<User> usersDB = (from p in db.Users.AsNoTracking()
                        where p.Surname == surname
                        select p).ToList();

            List<BL.User> res = new List<BL.User>();

            foreach (var elem in usersDB)
            {
                res.Add(UserConverter.DBToBL(elem));
            }

            return res;        
        }

        public List<BL.User> GetUsersByUserType(int type)
        {
            List<User> usersDB = (from p in db.Users.AsNoTracking()
                        where p.UserType == type
                        select p).ToList();

            List<BL.User> res = new List<BL.User>();

            foreach (var elem in usersDB)
            {
                res.Add(UserConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.User GetUserByLogin(string login)
        {
            try
            {
                User userDB = (from p in db.Users.AsNoTracking()
                            where p.Login == login
                            select p).First();

                return UserConverter.DBToBL(userDB);
            }
            catch (Exception) // ArgumentNullException
            {
                throw new UserNotFoundException();
            }            
        }

        public BL.User GetUserById(int id)
        {
            try
            {
                return UserConverter.DBToBL(db.Users.Find(id));
            }
            catch (Exception) // ArgumentNullException
            {
                throw new UserNotFoundException();
            }  
        }

        public void AddUser(BL.User user)
        {
            // Validation
            try
            {
                User userDB = UserConverter.BLToDB(user);
                UsersValidator.ValidateUser(userDB);
            }
            catch (Exception)
            {
                throw new UsersValidatorFailException();
            }

            // Exists
            try
            {
                var userDB = UserConverter.BLToDB(user);

                BL.User userDupl = GetUserById(userDB.Id);
                if (userDupl != null)
                {
                    throw new UserExistsException();
                }

                userDupl = GetUserByLogin(userDB.Login);
                if (userDupl != null)
                {
                    throw new UserExistsException();
                }
            }
            catch(UserNotFoundException)
            {
                db.Users.Add(UserConverter.BLToDB(user));
                db.SaveChanges();
            }
        }

        public void UpdateUser(int id, BL.User newUser)
        {
            User user = db.Users.Find(id);

            // NotFound
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            // Validation
            try
            {
                User userDB = UserConverter.BLToDB(newUser);
                UsersValidator.ValidateUser(userDB);
            }
            catch (Exception)
            {
                throw new UsersValidatorFailException();
            }

            // Exists
            try
            {
                var userDB = UserConverter.BLToDB(newUser);

                BL.User userDupl = GetUserByLogin(userDB.Login);
                if (userDupl != null && userDupl.Id != user.Id)
                {
                    throw new UserExistsException();
                }
            }
            catch(UserNotFoundException)
            {
                user.Name = newUser.Name;
                user.Surname = newUser.Surname;
                user.Login = newUser.Login;
                user.Password = newUser.Password;
                db.SaveChanges();
            }
        }

        public void BlockUser(int id)
        {
            User user = db.Users.Find(id);

            // NotFound
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            user.UserType = (int) BL.Permissions.UNAUTHORIZED;
            db.SaveChanges();
        }
    }
}