using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kursovay.DTO;
using kursovay.Models;
using kursovay.Services;
using System.Data.Entity;

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
        /*
        [HttpPost]
        public ActionResult ProductList(IEnumerable<ProductDto> productDtos, int userId)
        {
            productService.MakeOrder(productDtos, userId);
            return RedirectToAction("ProductList");
        }*/

        public ActionResult AddItemInOrder(int itemId)
        {
            PartnerDto partner = (PartnerDto)Session["session"];
            partner.ItemsId.Add(itemId);
            return RedirectToAction("ProductList");
        }

    

        public ActionResult MakeOrder()
        {
            PartnerDto partnerDto = (PartnerDto)Session["session"];
            productService.MakeOrder(partnerDto);
            partnerDto.ItemsId.Clear();
            Session["session"] = partnerDto;
            return RedirectToAction("ProductList");
        }
    }
}
