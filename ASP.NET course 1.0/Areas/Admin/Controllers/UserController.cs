using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rich.DataAccess.Data;
using Rich.DataAccess.Repository;
using Rich.DataAccess.Repository.IReposetory;
using Rich.Models;
using Rich.Models.ViewModels;
using Rich.Utility;
namespace ASPRich.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            obj.LockoutEnd = (obj.LockoutEnd != null && obj.LockoutEnd > DateTime.Now)
                ? DateTime.Now
                : DateTime.Now.AddYears(1000);

            _unitOfWork.Save();
            return Json(new { success = true, message = "Operation successful" });
        }

        [HttpGet]
        public IActionResult Permission(string id)
        {
            string roleId = _unitOfWork.ApplicationUser.GetUserRole(id);

            UserVM userVM = new UserVM()
            {
                CompanyList = _unitOfWork.Company.GetAll()
                    .Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }).ToList(),
                RoleList = _unitOfWork.ApplicationUser.GetRoles()
                    .Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString(),
                    }).ToList(),
                AppUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id, includeProperties: "Company")
            };
            userVM.AppUser.Role = roleId;
            return View(userVM);
        }

        [HttpPost]
        public IActionResult Permission(UserVM userVM)
        {
            var newRole = _unitOfWork.ApplicationUser.GetRoleNameById(userVM.AppUser.Role);
            var roleId = _unitOfWork.ApplicationUser.GetUserRole(userVM.AppUser.Id);
            string oldRole = _unitOfWork.ApplicationUser.GetRoleNameById(roleId);

            if (newRole != oldRole)
            {
                var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userVM.AppUser.Id);

                if (newRole == SD.Role_Company)
                {
                    user.CompanyId = userVM.AppUser.CompanyId;
                }
                else if (oldRole == SD.Role_Company)
                {
                    user.CompanyId = null;
                }

                _unitOfWork.Save();
                _userManager.RemoveFromRoleAsync(user, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, newRole).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> userList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").OrderBy(user => user.Name).ToList();
            var userRoles = _unitOfWork.ApplicationUser.GetUserRoles();
            var roles = _unitOfWork.ApplicationUser.GetRoles();

            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id)?.RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId)?.Name;
                user.Company ??= new() { Name = " " };
            }

            return Json(new { data = userList });
        }
    }
}