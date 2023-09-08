using ProductsCRUD.Common.Exceptions;
using ProductsCRUD.Infra.DbContext;
using ProductsCRUD.Domain.Models.Users;
using ProductsCRUD.Domain.Repositories.Users;
using SQLite;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace ProductsCRUD.Infra.Repositories.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SQLiteAsyncConnection databaseAsync;
        private readonly SQLiteConnection database;

        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            databaseAsync = appDbContext.AsyncConnection;
            database = appDbContext.Connection;
            Task.Run(() => databaseAsync.CreateTableAsync<User>()).Wait();
        }

        public IQueryable<User> GetQueryable()
        {
            return database.Table<User>().AsQueryable();
        }

        public async new Task Add(User user)
        {
            var userDb = await GetUserByCompositeKey(user);
            if (userDb == null)
            {
                user.Id = Guid.NewGuid().ToString();
                await databaseAsync.InsertAsync(user);
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

        public async Task<User> Get(User user)
        {
            var query = BuildQuery(user);

            return await Task.Run(() => query.FirstOrDefault());
        }

        private IQueryable<User> BuildQuery(User user)
        {
            var query = GetQueryable();

            if (user.Id != null && user.Id != string.Empty)
                query = query.Where(u => u.Id == user.Id);

            if (user.FirstName != null && user.FirstName != string.Empty)
                query = query.Where(u => u.FirstName == user.FirstName);

            if (user.LastName != null && user.LastName != string.Empty)
                query = query.Where(u => u.LastName == user.LastName);

            if (user.CPF != null && user.CPF != string.Empty)
                query = query.Where(u => u.CPF == user.CPF);

            if (user.Email != null && user.Email != string.Empty)
                query = query.Where(u => u.Email == user.Email);
            return query;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await databaseAsync.Table<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByCompositeKey(User user)
        {
            return await databaseAsync.Table<User>().FirstOrDefaultAsync(u => u.Id == user.Id || u.Email == user.Email || u.CPF == user.CPF);
        }

        public async Task<bool> Exists(User user)
        {
            return await Get(user) != null;
        }
    }
}
