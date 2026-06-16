using Microsoft.AspNetCore.Identity;
using Rich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rich.DataAccess.Repository.IReposetory
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        string GetUserRole(string userId);
        List<IdentityRole> GetRoles();
        string GetRoleNameById(string roleId);
        List<IdentityUserRole<string>> GetUserRoles();
    }
}
