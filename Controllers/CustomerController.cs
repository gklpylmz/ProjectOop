using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectOop.Controllers
{
    public class CustomerController : Controller
    {
        CustomerManager customerManager = new CustomerManager(new EFCustomerDAL());
        JobManager jobManager = new JobManager(new EFJobDAL());
        public IActionResult Index()
        {
            var values = customerManager.TGetCustomerWithJob();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            
            List<SelectListItem> values = (from x in jobManager.TGetList()
                                               select new SelectListItem
                                               {
                                                   Text=x.JobName,
                                                   Value=x.JobID.ToString(),
                                               }).ToList();
            ViewBag.SelectList = values;
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            CustomerValidator validationRules = new CustomerValidator();
            ValidationResult result = validationRules.Validate(customer);

            if (result.IsValid)
            {
                customerManager.TInsert(customer);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
            
        }

        public IActionResult RemoveCustomer(int id)
        {
            var value = customerManager.TGetByID(id);
            customerManager.TDelete(value);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditCustomer(int id)
        {
            List<SelectListItem> values = (from x in jobManager.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.JobName,
                                               Value = x.JobID.ToString(),
                                           }).ToList();
            ViewBag.SelectList = values;
            var value = customerManager.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult EditCustomer(Customer customer)
        {
            CustomerValidator validationRules = new CustomerValidator();
            ValidationResult result = validationRules.Validate(customer);
            if (result.IsValid)
            {
                customerManager.TUpdate(customer);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
