using AutoMapper;
using MTR.DTOs;
using MTR.Models;

namespace MTR.Profiles
{
    public class ETRReceiptProfile : Profile
    {
        public ETRReceiptProfile()
        {
            CreateMap<ETRReceipt, ETRReceiptDTO>().ReverseMap();
        }
    }
}
