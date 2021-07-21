using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MTR.Models
{
    public class Address
    {
        [Key]
        public string AddressID { get; set; }
        public string LRNumber { get; set; }
        public string Building { get; set; }
        public string StreetRoad{ get; set; }
        [Required]
        public string CityTown { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        [Required]
        public string POBox { get; set; }
        [Required]
        public string PostalCode { get; set; }

        public string TaxPayerID { get; set; }
        public TaxPayer TaxPayer { get; set; }

        public Address()
        {
            
        }
    }
}
