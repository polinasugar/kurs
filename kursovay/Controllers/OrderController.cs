
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

        public ActionResult GetAllOrders()
        {
            List<OrderDto> orders = productService.GetAllOrders();
            return View("OrdersList", orders);
        }

        public ActionResult List(int orderId, int userId)
        {
            SelectList list = new SelectList(productService.GetWarehouses(orderId), "id_warehouse", "title");
            ViewData["list"] = list;
            ViewData["orderId"] = orderId;
            ViewData["userId"] = userId;
            return PartialView("List");
        }

        public ActionResult MoveOrder(int orderId, int newWarehouseId, int userId, RoleTypes role)
        {
            productService.MoveOrder(orderId, newWarehouseId);
            switch (role)
            {
                case RoleTypes.Supplier:
                    return RedirectToAction("OrdersList", new { userId = userId });
                default:
                    return RedirectToAction("GetAllOrders");
            }
        }

        public ActionResult HistoryList(int orderId)
        {
            List<ShippHistoryDto> histories = productService.GetHistoriesDto(orderId);
            return View(histories);
        }

        public ActionResult AcceptOrder(int orderId)
        {
            PartnerDto partnerDto = (PartnerDto)Session["session"];
            productService.AcceptOrder(orderId);
            return RedirectToAction("OrdersList", new { userId = partnerDto.Id });
        }
    }
}
