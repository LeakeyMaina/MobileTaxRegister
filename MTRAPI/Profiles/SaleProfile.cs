using AutoMapper;
using MTR.DTOs;
using MTR.Models;

namespace MTR.Profiles
{
    public class SaleProfile:Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleDTO>().ReverseMap();
        }
    }
}
