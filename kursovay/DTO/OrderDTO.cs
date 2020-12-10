using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kursovay.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime? SendingDate { get; set; }
        public DateTime? ReceivingDate { get; set; }

        public string CurrentWarehouseTitle { get; set; }

        public List<ProductDto> Products { get; set; }

        public OrderDto()
        {
            Products = new List<ProductDto>();
        }
    }
}