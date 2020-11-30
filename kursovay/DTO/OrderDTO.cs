using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kursovay.DTO
{
    public class OrderDTO
    {
        public int ClientId { get; set; }

        public IEnumerable<ProductDto> ProductDtos { get; set; }
    }
}