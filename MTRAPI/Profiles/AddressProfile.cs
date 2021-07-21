using AutoMapper;
using MTR.DTOs;
using MTR.Models;
using System.Collections.Generic;

namespace MTR.Profiles
{
    public class AddressProfile:Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
