using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kursovay.DTO
{
    public class ShippHistoryDto
    {
        public string WarehouseTitle { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public DateTime? SendingDate { get; set; }

        public bool? Send { get; set; }
    }
}