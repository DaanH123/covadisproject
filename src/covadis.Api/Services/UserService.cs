using covadis.Api.Context;
using covadis.Api.Models;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace covadis.Api.Services
{
    public class UserService(DbContextCovadis dbContext)
    {
        public IEnumerable<UserResponse> GetUsers()
        {
            return dbContext.Users.Select(x => new UserResponse
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            });
        }

        public UserResponse? GetUserById(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public UserResponse CreateUser(CreateUserRequest request)
        {
            var existingUser = dbContext.Users
                .SingleOrDefault(x => x.Email == request.Email);

            if (existingUser != null)
            {
                // This should be a custom exception but for now we'll just throw a regular one.
                // Best case scenario is to create a Result object that contains error messages or a success flag.
                throw new Exception("User already exists");
            }

            var roles = dbContext.Roles
                .Where(x => request.Roles.Contains(x.Name))
                .ToList();

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Roles = roles
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public UserResponse UpdateUser(int id, UpdateUserRequest request)
        {
            var user = dbContext.Users.Find(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Name = request.Name;
            user.Email = request.Email;

            dbContext.SaveChanges();

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public void DeleteUser(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}
