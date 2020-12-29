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
        private WarehouseService warehouseService = new WarehouseService();
        public ActionResult ProductList()
        {
            List<ProductDto> productDtos = productService.GetAllProductsInDto();
            return View(productDtos);
        }

        public ActionResult ShowMyBag()
        {
            PartnerDto currentPartner = (PartnerDto)Session["session"];
            List<ProductDto> productsInBag = productService.GetProductsInBag(currentPartner.ItemsId);
            return View("ProductBag", productsInBag);
        }

        public ActionResult RemoveItemFromBag(int itemId)
        {
            PartnerDto currentPartner = (PartnerDto)Session["session"];
            currentPartner.ItemsId.Remove(itemId);
            return RedirectToAction("ShowMyBag");
        }

        public ActionResult AddItemInOrder(int itemId)
        {
            PartnerDto partner = (PartnerDto)Session["session"];
            partner.ItemsId.Add(itemId);
            return RedirectToAction("ProductList");
        }

        public ActionResult AddProductForm()
        {
            List<Warehouses> warehouses = warehouseService.GetList();
            List<Product_type> types = productService.GetTypes();
            ViewData["warehouses"] = new SelectList(warehouses, "id_warehouse", "title");
            ViewData["types"] = new SelectList(types, "id_product_type", "title");
            return View("AddProduct");
        }

        public ActionResult DeleteProduct(int productId)
        {
            productService.DeleteProduct(productId);
            return RedirectToAction("ProductList");
        }

        public ActionResult Addproduct(Products products)
        {
            productService.AddProduct(products);
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
