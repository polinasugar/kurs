using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.Models;
using kursovay.DTO;

namespace kursovay.Utils
{
    //Для регистрации
    public class Mapper
    {
        private CargoDataBase db = new CargoDataBase();
        public Partners RegistrationDTOToPartner(RegistrationDTO newPartner)
        {
            Roles role = db.Roles.Where(r => r.title == newPartner.Type.ToString()).First();
            Partners partner = new Partners();
            partner.first_name = newPartner.FirstName;
            partner.second_name = newPartner.SecondName;
            partner.middle_name = newPartner.MiddleName;
            partner.address = newPartner.Address;
            partner.id_role = role.id_role;
            partner.login = newPartner.LoginAndPassword.Login;
            partner.password = newPartner.LoginAndPassword.Password;
            partner.itn = newPartner.ITN;
            partner.telephone = newPartner.Telephone;
            return partner;
        }

        public OrderDto GetOrderDto(Orders order)
        {
            OrderDto orderDto = new OrderDto()
            {
                OrderId = order.id_order,
                ReceivingDate = order.recevening_date,
                SendingDate = order.sending_date
            };
            Shipp_history history = db.Shipp_history.Where(sh => sh.id_order == order.id_order && sh.send == false).First();
            Warehouses warehouse = db.Warehouses.Where(w => w.id_warehouse == history.id_warehouse).First();
            orderDto.CurrentWarehouseTitle = warehouse.title;
            foreach(Products product in order.Products)
            {
                ProductDto productDto = ProductToProductDto(product);
                orderDto.Products.Add(productDto);
            }
            return orderDto;
        }

        public List<OrderDto> GetOrdersDto(List<Orders> orders)
        {
            List<OrderDto> ordersDto = new List<OrderDto>();
            orders.ForEach(o => ordersDto.Add(GetOrderDto(o)));
            return ordersDto;
        }

        public ProductDto ProductToProductDto(Products product)
        {
            ProductDto productDto = new ProductDto()
            {
                Id = product.id_product,
                Title = product.title,
                Weight = product.weight,
                ProductType = product.Product_type.title,
                WarehouseTitle = product.Warehouses.title
            };
            return productDto;
        }

        public List<Products> MapProductDtoToList(IEnumerable<ProductDto> productDtos)
        {
            List<Products> products = new List<Products>();
            foreach(ProductDto productDto in productDtos)
            {
                if(productDto.Buy)
                {
                    Products product = ProductsDtoToProduct(productDto);
                    products.Add(product);
                }
            }
            return products;
        }

        public Products ProductsDtoToProduct(ProductDto productDto)
        {
            Products product = new Products()
            {
                title = productDto.Title,
                weight = productDto.Weight
            };
            int typeId = db.Product_type.Where(pt => pt.title == productDto.ProductType).First().id_product_type;
            int warehouseId = db.Warehouses.Where(w => w.title == productDto.WarehouseTitle).First().id_warehouse;
            product.id_warehouse = warehouseId;
            product.id_warehouse = typeId;
            return product;
        }
    }
}