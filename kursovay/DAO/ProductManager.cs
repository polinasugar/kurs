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


        public Products GetProductById(int id)
        {
            return db.Products.Where(p => p.id_product == id).First();
        }

        public void DeleteProduct(int productId)
        {
            Products product = db.Products.Where(p => p.id_product == productId).First();
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public Orders AddOrder(int userId, int itemsCount)
        {
            using(DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Orders order = new Orders();
                    order.id_recipient = 24;
                    order.sending_date = DateTime.Now;
                    DateTime receivDay = DateTime.Now.AddDays(10);
                    order.recevening_date = receivDay;
                    order.price = itemsCount * 100;
                    order = db.Orders.Add(order);
                    db.SaveChanges();
                    transaction.Commit();
                    return order;
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