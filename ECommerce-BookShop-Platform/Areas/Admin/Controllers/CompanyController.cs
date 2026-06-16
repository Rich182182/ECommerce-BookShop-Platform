using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rich.DataAccess.Repository.IReposetory;
using Rich.Models;
using Rich.Models.ViewModels;
using Rich.Utility;

namespace ASPRich.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().OrderBy(x => x.Name).ToList();
            return View(companies);
        }
        public IActionResult Upsert(int? id)
        {
            Company comp = new Company();
            if (id == null)
            {
                return View(comp);
            }
            comp = _unitOfWork.Company.Get(x => x.Id == id);
            return View(comp);
        }
        [HttpPost]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                }
                _unitOfWork.Save();
                TempData["success"] = "product was created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> companyList = _unitOfWork.Company.GetAll().OrderBy(prod => prod.Name).ToList();
            return Json(new { data = companyList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToDelete = _unitOfWork.Company.Get(x=>x.Id == id);
            if(companyToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Company.Remove(companyToDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Company was deleted successfuly" });
        }
    }
}
