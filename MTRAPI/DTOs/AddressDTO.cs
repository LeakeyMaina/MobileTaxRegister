using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTR.DTOs
{
    public class AddressDTO
    {
        public string AddressID { get; set; }
        public string LRNumber { get; set; }
        public string Building { get; set; }
        public string StreetRoad { get; set; }
        public string CityTown { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string POBox { get; set; }
        public string PostalCode { get; set; }

        public string TaxPayerID { get; set; }
        public TaxPayerDTO TaxPayer { get; set; }
    }
}
