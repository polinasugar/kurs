
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
        private UserService userService = new UserService();
        public ActionResult OrdersList(int userId)
        {
            List<OrderDto> orders = productService.GerOrders(userId);
            return View(orders);
        }

        public ActionResult List(int orderId, int userId)
        {
            SelectList list = new SelectList(productService.GetWarehouses(orderId), "id_warehouse", "title");
            ViewData["list"] = list;
            ViewData["orderId"] = orderId;
            ViewData["userId"] = userId;
            return PartialView("List");
        }

        public ActionResult MoveOrder(int orderId, int newWarehouseId, int userId)
        {
            productService.MoveOrder(orderId, newWarehouseId);
            return RedirectToAction("OrdersList", new { userId = userId });
        }

        public ActionResult AcceptOrder(int orderId)
        {
            PartnerDto partnerDto = (PartnerDto)Session["session"];
            productService.AcceptOrder(orderId);
            return RedirectToAction("OrdersList", new { userId = partnerDto.Id });
        }
    }
}
