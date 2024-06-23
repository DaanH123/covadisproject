using covadis.Api.Context;
using covadis.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace covadis.Api.Services
{
    public class RoleService
    {
        private readonly DbContextCovadis _dbContext;

        public RoleService(DbContextCovadis dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _dbContext.Roles.FindAsync(id);
        }

        public async Task<Role> AddRoleAsync(Role role)
        {
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }

        public async Task<bool> UpdateRoleAsync(int id, Role role)
        {
            if (id != role.Id)
                return false;

            _dbContext.Entry(role).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);
            if (role == null)
                return false;

            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private bool RoleExists(int id)
        {
            return _dbContext.Roles.Any(e => e.Id == id);
        }
    }
}
