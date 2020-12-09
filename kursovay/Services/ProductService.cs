using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.Models;
using kursovay.Utils;
using kursovay.DAO;
using kursovay.DTO;
using System.Data.Entity;


namespace kursovay.Services
{
    public class ProductService
    {
        private ProductManager productManager = new ProductManager();
        private Mapper mapper = new Mapper();
        private CargoDataBase db = new CargoDataBase();
        public List<ProductDto> GetAllProductsInDto()
        {
            List<ProductDto> productDtos = new List<ProductDto>();
            List<Products> products = productManager.GetAllProducts();
            foreach(Products product in products)
            {
                ProductDto productDto = mapper.ProductToProductDto(product);
                productDtos.Add(productDto);
            }
            return productDtos;
        } 

        public void MakeOrder(PartnerDto partnerDto)
        {
            CargoDataBase db = new CargoDataBase();
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Orders order = new Orders();
                    order.id_recipient = partnerDto.Id;
                    order.sending_date = DateTime.Now;
                    DateTime receivDay = DateTime.Now.AddDays(10);
                    order.recevening_date = receivDay;
                    order.price = partnerDto.ItemsId.Count * 100;
                    order = db.Orders.Add(order);
                    db.SaveChanges();
                    foreach (int id in partnerDto.ItemsId)
                    {
                        Products product = db.Products.Where(p => p.id_product == id).First();
                        order.Products.Add(product);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    partnerDto.ItemsId.Clear();
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