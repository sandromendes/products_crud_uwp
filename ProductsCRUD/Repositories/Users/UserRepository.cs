using ProductsCRUD.DbContext;
using ProductsCRUD.Exceptions;
using ProductsCRUD.Models.Users;
using SQLite;
using System;
using System.Linq.Expressions;

namespace ProductsCRUD.Repositories.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SQLiteConnection database;

        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            database = appDbContext.Connection;
            database.CreateTable<User>();
        }

        public new void Add(User user)
        {
            var userDb = GetUserByCompositeKey(user);
            if (userDb == null)
            {
                user.Id = Guid.NewGuid().ToString();
                database.Insert(user);
            }
            else
            {
                if (user.Id == userDb.Id)
                    throw new UserAlreadyExistsException("Operação inválida! Existe um usuário com o mesmo id.");

                if (user.Email == userDb.Email)
                    throw new UserAlreadyExistsException("Operação inválida! Existe um usuário com o mesmo Email");

                if (user.CPF == userDb.CPF)
                    throw new UserAlreadyExistsException("Operação inválida! Existe um usuário com o mesmo CPF.");
            }
        }

        public User GetUserByEmail(string email)
        {
            return database.Table<User>().FirstOrDefault(u => u.Email == email);
        }

        public User GetUserByCompositeKey(User user)
        {
            return database.Table<User>().FirstOrDefault(u => u.Id == user.Id || u.Email == user.Email || u.CPF == user.CPF);
        }

        public bool Exists(Expression<Func<User, bool>> predicate)
        {
            return database.Table<User>().FirstOrDefault(predicate) != null;
        }
    }
}
