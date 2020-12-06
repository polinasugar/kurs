using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.Models;
using kursovay.Utils;
using kursovay.DAO;
using kursovay.DTO;


namespace kursovay.Services
{
    public class ProductService
    {
        private ProductManager productManager = new ProductManager();
        private Mapper mapper = new Mapper();
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

        public void MakeOrder(IEnumerable<ProductDto> dtos, int userId)
        {
            List<Products> products = mapper.MapProductDtoToList(dtos);
            productManager.AddOrder(products, userId);
        }
    }
}