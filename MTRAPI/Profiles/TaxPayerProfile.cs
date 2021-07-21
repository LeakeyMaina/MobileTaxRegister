using AutoMapper;
using MTR.DTOs;
using MTR.Models;

namespace MTR.Profiles
{
    public class TaxPayerProfile : Profile
    {
        public TaxPayerProfile()
        {
            CreateMap<TaxPayer, TaxPayerDTO>().ReverseMap();
        }
    }
}
