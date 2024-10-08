using Employee.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employees employee)
        {


            db.Employees.Add(employee);
            db.SaveChanges();
            return Json(employee);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var employees = db.Employees.ToList();
            
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(Employee.Models.Employees employee)
        {
            var existingEmployee =db.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.Department = employee.Department;
            }
            db.SaveChanges();
            return Json(employee);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}