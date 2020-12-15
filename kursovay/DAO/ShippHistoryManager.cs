using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using kursovay.Models;

namespace kursovay.DAO
{
    public class ShippHistoryManager
    {
        private CargoDataBase db = new CargoDataBase();
        public Shipp_history AddShippingHistory(Orders order)
        {
            Shipp_history history = new Shipp_history()
            {
                id_order = order.id_order,
                id_partner = order.id_recipient,
                receiving_date = order.recevening_date,
                send_date = order.sending_date,
                send = false
            };
            int? wareHouseId = order.Products.ToArray()[0].id_warehouse;
            history.id_warehouse = wareHouseId;
            return history;
        }

        public List<Shipp_history> GetHistories(int orderId)
        {
            return db.Shipp_history.Where(h => h.id_order == orderId).Include(h => h.Warehouses).ToList();
        }
    }
}