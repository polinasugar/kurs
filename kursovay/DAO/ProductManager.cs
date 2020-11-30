using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using kursovay.Models;
using kursovay.DTO;

namespace kursovay.DAO
{
    public class ProductManager
    {
        private CargoDataBase db = new CargoDataBase();

        public List<Products> GetAllProducts()
        {
            List<Products> products = db.Products.Include(p => p.Product_type).Include(p => p.Warehouses).ToList();
            return products;
        }

        public void AddOrder(List<Products> products, int partnerId)
        {
            using(DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Partners partner = db.Partners.Where(p => p.id_user == partnerId).First();
                    Orders order = new Orders();
                    order.id_recipient = partnerId;
                    order = db.Orders.Add(order);
                    db.SaveChanges();
                    foreach(Products product in products)
                    {
                        order.Products.Add(product);
                    }
                    db.SaveChanges();
                    partner.Orders.Add(order);
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
    }
}