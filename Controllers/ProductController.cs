using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ProjectOop.Controllers
{
    public class ProductController : Controller
    {
        ProductManager productManager = new ProductManager(new EFProductDAL());
        public IActionResult Index()
        {
            var values = productManager.TGetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            ProductValidator validationRules = new ProductValidator();
            ValidationResult validationResult = validationRules.Validate(product);
            if (validationResult.IsValid)
            {
                productManager.TInsert(product);
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
        public IActionResult RemoveProduct(int id)
        {
            var value = productManager.TGetByID(id);
            productManager.TDelete(value);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var value = productManager.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            ProductValidator validationRules = new ProductValidator();
            ValidationResult validationResult = validationRules.Validate(product);
            if (validationResult.IsValid)
            {
                productManager.TUpdate(product);
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
