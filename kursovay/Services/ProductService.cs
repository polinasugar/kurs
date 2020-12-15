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
        private OrderManager orderManager = new OrderManager();
        private CargoDataBase db = new CargoDataBase();
        private HistoryMapper historyMapper = new HistoryMapper();
        private ShippHistoryManager shipHistoryManager = new ShippHistoryManager();
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

        public List<OrderDto> GetAllOrders()
        {
            List<Orders> orders = orderManager.GetAllOrder();
            List<OrderDto> ordersDto = mapper.GetOrdersDto(orders);
            return ordersDto;
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
                    Shipp_history history = shipHistoryManager.AddShippingHistory(order);
                    db.Shipp_history.Add(history);
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

        public void MoveOrder(int orderId, int newWarehouseId)
        {
            orderManager.MoveOrder(orderId, newWarehouseId);
        }

        public List<OrderDto> GerOrders(int userId)
        {
            List<Orders> orders = orderManager.GetOrdersOfUser(userId);
            List<OrderDto> ordersDto = mapper.GetOrdersDto(orders);
            return ordersDto;
        }

        public void AcceptOrder(int itemId)
        {
            orderManager.DeleteOrder(itemId);
        }

        public List<Warehouses> GetWarehouses(int orderId)
        {
            return orderManager.GetWarehousesOfOrder(orderId);
        }

        public List<ShippHistoryDto> GetHistoriesDto(int orderId)
        {
            List<Shipp_history> histories = shipHistoryManager.GetHistories(orderId);
            List<ShippHistoryDto> historiesDto = historyMapper.GetHistoryList(histories);
            return historiesDto;
        }
    }
}