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

        public void DeleteOrder(int orderId)
        {
            using(DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Orders order = db.Orders.Where(o => o.id_order == orderId).First();
                    List<Shipp_history> histories = db.Shipp_history.Where(h => h.id_order == orderId).ToList();
                    histories.ForEach(h => db.Shipp_history.Remove(h));
                    order.Products.Clear();
                    db.SaveChanges();
                    db.Orders.Remove(order);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void MoveOrder(int orderId, int newWarehouseId)
        {
            using(DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Orders order = db.Orders.Where(o => o.id_order == orderId).First();
                    Shipp_history history = db.Shipp_history.Where(h => h.id_order == orderId && h.send == false).First();
                    history.send = true;
                    db.SaveChanges();
                    Shipp_history newHistory = new Shipp_history()
                    {
                        id_partner = order.id_recipient,
                        id_order = order.id_order,
                        id_warehouse = newWarehouseId,
                        send_date = order.sending_date,
                        receiving_date = order.recevening_date,
                        send = false
                    };
                    db.Shipp_history.Add(newHistory);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {

                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Warehouses> GetWarehousesOfOrder(int orderId)
        {
            List<Warehouses> list = new List<Warehouses>();
            Orders order = db.Orders.Where(o => o.id_order == orderId).Include(o => o.Products).First();
            foreach(Products product in order.Products)
            {
                Warehouses warehouses = db.Warehouses.Where(w => w.id_warehouse == product.id_warehouse).First();
                list.Add(warehouses);
            }
            return list;
        }
    }
}