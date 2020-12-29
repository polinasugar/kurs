using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kursovay.Models;
using kursovay.Services;

namespace kursovay.Controllers
{
    public class WarehouseController : Controller
    {
        private WarehouseService warehouseService = new WarehouseService();

        public ActionResult AddWarehouseForm()
        {
            return View("AddWarehouse");
        }

        public ActionResult AddWarehouse(Warehouses warehouse)
        {
            warehouseService.AddWarehouse(warehouse);
            return RedirectToAction("WarehouseList");
        }

        public ActionResult WarehouseList()
        {
            List<Warehouses> warehouses = warehouseService.GetList();
            return View(warehouses);
        }
    }
}