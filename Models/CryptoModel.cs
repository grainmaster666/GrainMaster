namespace GrainMaster.Models
{
    public class CryptoModel
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Image { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Volume_Change_15M_percent { get; set; }
        public decimal Volume_Change_30M_percent { get; set; }
        public decimal Volume_Change_1h_percent { get; set; }
    }
}