using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.Models;
using System.Data.Entity;

namespace kursovay.DAO
{
    public class OrderManager
    {
        private CargoDataBase db = new CargoDataBase();
        public List<Orders> GetOrdersOfUser(int userId)
        {
            List<Orders> orders = db.Orders.Where(o => o.id_recipient == userId).Include(o => o.Products).ToList();
            return orders; 
        }
    }
}