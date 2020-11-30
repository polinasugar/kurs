using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kursovay.DTO
{
    public class ProductDto
    {
        public int Id { get; set;  }
        public string  Title { get; set; }
        public double? Weight { get; set; }
        public string ProductType { get; set; }
        public string WarehouseTitle { get; set; }
        public bool Buy { get; set; }
    }
}