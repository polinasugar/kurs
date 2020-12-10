
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kursovay.DTO;
using kursovay.Services;

namespace kursovay.Controllers
{
    public class OrderController : Controller
    {
        private ProductService productService = new ProductService();
        public ActionResult OrdersList(int userId)
        {
            List<OrderDto> orders = productService.GerOrders(userId);
            return View(orders);
        }
    }
}
