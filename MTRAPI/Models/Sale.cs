using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MTR.Models
{
    public class Sale
    {
        [Key]
        public string SaleID { get; set; }

        [Required]
        public string SaleDate { get; set; }
        [Required]
        public double SaleAmount { get; set; }

        public double VATAmount { get; set; }

        [Required]
        public string ETRID { get; set; }

        [JsonIgnore]
        public ETR ETR { get; set; }

        [Required]
        //public string ETRReceiptID { get; set; }
        public ETRReceipt ETRReceipt{ get; set; }
        
    }

}
