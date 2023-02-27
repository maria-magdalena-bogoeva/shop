using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopDemo.Abstraction;
using WebShopDemo.Domain;
using WebShopDemo.Models.Brand;
using WebShopDemo.Models.Categroy;
using WebShopDemo.Models.Product;

namespace WebShopDemo.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ProductController : Controller
    {

        private readonly IProductService _productServices;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;


        public ProductController(IProductService productServices, ICategoryService categoryService, IBrandService brandService)
        {
            this._productServices = productServices;
            this._brandService = brandService;
            this._categoryService = categoryService;
        }
        [AllowAnonymous]
        public IActionResult Index(string searchStringCategoryName, string searchStringBrandName)
        {
            List<ProductIndexVM> products = _productServices.GetProducts(searchStringCategoryName, searchStringBrandName).Select(product => new ProductIndexVM
            {
                Id=product.Id,
                ProductName= product.ProductName,
                BrandId= product.BrandId,
                BrandName= product.Brand.BrandName,
                CategoryId= product.CategoryId,
                CategoryName= product.Category.CategoryName,
                Picture= product.Picture,
                Quantity= product.Quantity,
                Price= product.Price,
                Discount= product.Discount
            }).ToList();
            return this.View(products);
        }


        public IActionResult Create()
        {
            var product = new ProductCreateVM();
            product.Brands = _brandService.GetBrands().Select(x => new BrandPairVM()
            {
                Id = x.Id,
                Name = x.BrandName
            }).ToList();
           
            product.Categories = _categoryService.GetCategories().Select(x => new CategoryPairVM()
            {
                Id = x.Id,
                Name = x.CategoryName
            }).ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                var createId = _productServices.Create(product.ProductName, product.BrandId,
                    product.CategoryId, product.Picture,
                    product.Quantity, product.Price, product.Discount
                    );

                if (createId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }
        public IActionResult Edit(int id)
        {

            Product product = _productServices.GetProductById(id);
            if (product==null)
            {
                return NotFound();
            }
            ProductEditVM updateProduct = new ProductEditVM()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount
            };
            updateProduct.Brands = _brandService.GetBrands().Select(b => new BrandPairVM()
            {
                Id = b.Id,
                Name = b.BrandName
            }).ToList();

            updateProduct.Categories = _categoryService.GetCategories().Select(c => new CategoryPairVM()
            {
                Id = c.Id,
                Name = c.CategoryName
            }).ToList();

            return View(updateProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,ProductEditVM product)
        {
            if (ModelState.IsValid)
            {
                var updated = _productServices.Update(id,product.ProductName, product.BrandId,
                    product.CategoryId, product.Picture,
                    product.Quantity, product.Price, product.Discount
                    );

                if (updated)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product item = _productServices.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                BrandId = item.BrandId,
                CategoryId = item.CategoryId,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
           
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            Product item = _productServices.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDeleteVM product = new ProductDeleteVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                BrandId = item.BrandId,
                CategoryId = item.CategoryId,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            
                var deleted = _productServices.RemoveById(id);

                if (deleted)
                {
                    return RedirectToAction("Success");
                }
                else
                {
                   return View();
                }
            
        }
        public IActionResult Success()
        {
            return View();
        }

    }
}
