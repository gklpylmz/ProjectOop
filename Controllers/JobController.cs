using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ProjectOop.Controllers
{
    public class JobController : Controller
    {
        JobManager jobManager = new JobManager(new EFJobDAL());
        public IActionResult Index()
        {
            var values = jobManager.TGetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddJob()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddJob(Job job)
        {
            JobValidator validationRules = new JobValidator();
            ValidationResult validationResult = validationRules.Validate(job);
            if (validationResult.IsValid)
            {
                jobManager.TInsert(job);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
        public IActionResult RemoveJob(int id)
        {
            var value = jobManager.TGetByID(id);
            jobManager.TDelete(value);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditJob(int id)
        {
            var value = jobManager.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult EditJob(Job job)
        {
            JobValidator validationRules = new JobValidator();
            ValidationResult validationResult = validationRules.Validate(job);
            if (validationResult.IsValid)
            {
                jobManager.TUpdate(job);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
    }
}
