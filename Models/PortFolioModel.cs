using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrainMaster.Models
{
    public class PortFolioModel : NewsModel
    {
        public int ID { get; set; }
        [Required]
        public string StockName { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int PurchaseType { get; set; }
        public string TPSID { get; set; }


    }

    public class VolDeliveryModel : PortFolioModel
    {
        public decimal SMA5 { get; set; }
        public decimal SMA10 { get; set; }
        public decimal SMA20 { get; set; }
        public decimal SMA50 { get; set; }
        public decimal SMA100 { get; set; }
        public decimal SMA200 { get; set; }
        public decimal R1 { get; set; }
        public decimal R2 { get; set; }
        public decimal R3 { get; set; }
        public decimal S1 { get; set; }
        public decimal S2 { get; set; }
        public decimal S3 { get; set; }
        public string DeliveryAverageMonth { get; set; }
        public string DeliveryAverageWeek { get; set; }
        public string DeliveryYesterday { get; set; }
        public DealType ISDealType { get; set; }

    }

    public class CombinedModel
    {
        public PortFolioModel PortFolio { get; set; }
        public List<VolDeliveryModel> PortFolioList { get; set; }
        public List<NewsRepository> NewsRepositoriesModel { get; set; }
    }

    public class DealType
    {
        public bool IsPositiveDeal { get; set; }
        public bool IsNegativeDeal { get; set; }
    }

    public class StockDetail
    {
        public int ID;
        public string Name;
    }

    public class TPDetail
    {
        public string TPSID;
        public string URL;
    }

}