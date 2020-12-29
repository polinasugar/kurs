using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.Models;

namespace kursovay.Services
{
    public class WarehouseService
    {
        private CargoDataBase db = new CargoDataBase();

        public List<Warehouses> GetList()
        {
            return db.Warehouses.ToList();
        }

        public void AddWarehouse(Warehouses warehouses)
        {
            db.Warehouses.Add(warehouses);
            db.SaveChanges();
        }
    }
}