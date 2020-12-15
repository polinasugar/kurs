using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.DTO;
using kursovay.Models;

namespace kursovay.Utils
{
    public class HistoryMapper
    {
        private ShippHistoryDto GetHistoryDto(Shipp_history history)
        {
            ShippHistoryDto historyDto = new ShippHistoryDto()
            {
                WarehouseTitle = history.Warehouses.title,
                ReceivingDate = history.receiving_date,
                SendingDate = history.send_date,
                Send = history.send
            };
            return historyDto;
        }

        public List<ShippHistoryDto> GetHistoryList(List<Shipp_history> histories)
        {
            List<ShippHistoryDto> shippHistories = new List<ShippHistoryDto>();
            histories.ForEach(h => shippHistories.Add(GetHistoryDto(h)));
            return shippHistories;
        }
    }
}