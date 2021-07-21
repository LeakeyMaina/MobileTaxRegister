using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTR.Models
{
    public class ETRReceipt
    {
        [Key]
        public string ETRReceiptID { get; set; }


        [Required]
        public string SaleID { get; set; }
        public Sale Sale { get; set; }


        [Required]
        public string ETRID { get; set; }

        [JsonIgnore]
        public ETR ETR { get; set; }

        [Required]
        public string SaleDate { get; set; }

        [Required]
        public double SaleAmount { get; set; }

        [Required]
        public double VATAmount { get; set; }

    }
}
