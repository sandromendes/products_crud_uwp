using ProductsCRUD.Common.Exceptions;
using ProductsCRUD.Infra.DbContext;
using ProductsCRUD.Domain.Models.Users;
using ProductsCRUD.Domain.Repositories.Users;
using SQLite;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductsCRUD.Infra.Repositories.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SQLiteAsyncConnection database;

        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            database = appDbContext.AsyncConnection;
            database.CreateTableAsync<User>();
        }

        public async new Task Add(User user)
        {
            var userDb = await GetUserByCompositeKey(user);
            if (userDb == null)
            {
                user.Id = Guid.NewGuid().ToString();
                await database.InsertAsync(user);
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

        public async Task<User> Get(Expression<Func<User, bool>> predicate)
        {
            return await database.Table<User>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await database.Table<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByCompositeKey(User user)
        {
            return await database.Table<User>().FirstOrDefaultAsync(u => u.Id == user.Id || u.Email == user.Email || u.CPF == user.CPF);
        }

        public async Task<bool> Exists(Expression<Func<User, bool>> predicate)
        {
            return await database.Table<User>().FirstOrDefaultAsync(predicate) != null;
        }
    }
}
