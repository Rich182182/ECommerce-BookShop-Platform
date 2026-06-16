using Microsoft.AspNetCore.Identity;
using Rich.DataAccess.Data;
using Rich.DataAccess.Repository.IReposetory;
using Rich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rich.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public string GetUserRole(string userId)
        {
            return _db.UserRoles.FirstOrDefault(ur => ur.UserId == userId)?.RoleId;
        }

        public List<IdentityRole> GetRoles()
        {
            return _db.Roles.ToList();
        }

        public string GetRoleNameById(string roleId)
        {
            return _db.Roles.FirstOrDefault(r => r.Id == roleId)?.Name;
        }

        public List<IdentityUserRole<string>> GetUserRoles()
        {
            return _db.UserRoles.ToList();
        }


    }
}
