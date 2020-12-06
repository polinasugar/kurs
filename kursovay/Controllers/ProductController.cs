using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kursovay.DTO;
using kursovay.Services;

namespace kursovay.Controllers
{
    public class ProductController : Controller
    {
        private ProductService productService = new ProductService();
        public ActionResult ProductList()
        {
            List<ProductDto> productDtos = productService.GetAllProductsInDto();
            return View(productDtos);
        }

        [HttpPost]
        public ActionResult ProductList(IEnumerable<ProductDto> productDtos, int userId)
        {
            productService.MakeOrder(productDtos, userId);
            return RedirectToAction("ProductList");
        }
    }
}
