using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using hehe.DB;
using hehe.Models;
using hehe.Models.DBModels;
using hehe.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace hehe.Controllers
{
    public class CategoriesController : Controller
    {
        public ActionResult Index()
        {
            var model = new PCIndexViewModel();
            using (var db = new heheDB())
            {
                model.PCList.AddRange(db.ProductCategories.Select(r => new PCIndexViewModel.PCListViewModel
                {
                    ProductCategoryId = r.ProductCategoryId,
                    Name = r.Name
                }));
            }

            return View(model);
        }

        public ActionResult ViewProducts(int? id)
        {
            using (var db = new heheDB())
            {
                var model = new ProductIndexViewModel();
                model.ProductList.AddRange(db.Products.Select(p => new ProductIndexViewModel.ProductListViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ProductCategoryId = p.ProductCategoryId
                }).Where(p => p.ProductCategoryId == id));
                return View(model);
            }
        }

        public ActionResult ViewProductDetails(int? id)
        {
            using (var db = new heheDB())
            {
                var model = new ProductIndexViewModel();
                model.ProductList.AddRange(db.Products.Select(p => new ProductIndexViewModel.ProductListViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ProductCategoryId = p.ProductCategoryId
                }).Where(p => p.ProductId == id));
                return View(model);
            }
        }





        [HttpGet]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult Edit(int? id)
        {
            using (var db = new heheDB())
            {
                var item = db.ProductCategories.Find(id);
                var model = new PCEditViewModel
                {
                    ProductCategoryId = item.ProductCategoryId,
                    Name = item.Name
                };
                return View(model);
            }
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpPost]
        public ActionResult Edit(PCEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new heheDB())
            {
                var item = db.ProductCategories.FirstOrDefault(p => p.ProductCategoryId == model.ProductCategoryId);

                item.ProductCategoryId = model.ProductCategoryId;
                item.Name = model.Name;


                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }





        [Authorize(Roles = "Admin, ProductManager")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new PCEditViewModel());
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpPost]
        public ActionResult Create(PCEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new heheDB())
            {
                var item = new ProductCategory
                {

                    Name = model.Name

                };

                db.ProductCategories.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }





        [Authorize(Roles = "Admin, ProductManager")]
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View(new ProductEditViewModel());
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpPost]
        public ActionResult CreateProduct(ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new heheDB())
            {
                var item = new Product
                {

                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ProductCategoryId = model.ProductCategoryId

                };

                db.Products.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }





        [HttpGet]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult EditProduct(int? id)
        {
            using (var db = new heheDB())
            {
                var item = db.Products.Find(id);
                var model = new ProductEditViewModel
                {
                    ProductId = item.ProductId,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ProductCategoryId = item.ProductCategoryId
                };
                return View(model);
            }
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpPost]
        public ActionResult EditProduct(ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new heheDB())
            {
                var item = db.Products.FirstOrDefault(p => p.ProductId == model.ProductId);

                item.ProductId = model.ProductId;
                item.Name = model.Name;
                item.Description = model.Description;
                item.Price = model.Price;
                item.ProductCategoryId = model.ProductCategoryId;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }




        public ActionResult ViewProduct(int? id)
        {
            var model = new ProductEditViewModel();

            using (var db = new heheDB())
            {
                var product = db.Products.Find(id);
                model.Name = product.Name;
                model.Description = product.Description;
                model.Price = product.Price;
            }

            return View(model);
        }



        [HttpGet]
        public ActionResult Search(string search, string sort)
        {
            var model = new ProductIndexViewModel();
            model.Search = search;

            if (!string.IsNullOrWhiteSpace(search))
            {
                using (var db = new heheDB())
                {
                    model.ProductList.AddRange(db.Products
                        .Select(x => new ProductIndexViewModel.ProductListViewModel
                        {
                            ProductId = x.ProductId,
                            Name = x.Name,
                            Description = x.Description,
                            Price = x.Price,
                            ProductCategoryId = x.ProductCategoryId
                        }));

                    model.ProductList = model.ProductList.Where(x => x.Name.ToUpper().Contains(search.ToUpper()) ||
                                                                     x.Description.ToUpper().Contains(search.ToUpper())).ToList();

                    model = Sort(model, sort);

                    return View("Search", model);
                }
            }

            return View("Search", model);
        }

        public ProductIndexViewModel Sort(ProductIndexViewModel model, string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                sort = "nameAsc";
            }
            model.SortOrderName = sort == "nameDesc" ? "nameAsc" : "nameDesc";
            model.SortOrderPrice = sort == "priceDesc" ? "priceAsc" : "priceDesc";

            switch (sort)
            {
                case "nameAsc":
                    model.ProductList = model.ProductList.OrderBy(x => x.Name).ToList();
                    break;
                case "nameDesc":
                    model.ProductList = model.ProductList.OrderByDescending(x => x.Name).ToList();
                    break;
                case "priceAsc":
                    model.ProductList = model.ProductList.OrderBy(p => p.Price).ToList();
                    break;
                case "priceDesc":
                    model.ProductList = model.ProductList.OrderByDescending(p => p.Price).ToList();
                    break;
            }

            return model;
        }
    }

}