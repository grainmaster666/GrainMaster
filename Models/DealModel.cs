using System.Collections.Generic;

namespace GrainMaster.Models
{
    public class DealModel
    {
        public string Company { get; set; }
        public string Quantity { get; set; }
        public decimal Price { get; set; }
        public string TranDate { get; set; }
    }
    public class DealData
    {
        public string Company { get; set; }

        public string TotalBuyQuantity { get; set; }

        public string TotalSellQuantity { get; set; }

        public decimal TotalBuyPrice { get; set; }

        public decimal TotalSellPrice { get; set; }
        public string TransactionDate { get; set; }
    }
    public class DealRoot
    {
        public List<DealModel> OnlyBuy { get; set; }
        public List<DealModel> OnlySell { get; set; }
        public List<DealData> BuyGTSell { get; set; }
        public List<DealData> SellGTBuy { get; set; }
    }
}