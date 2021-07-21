using AutoMapper;
using MTR.DTOs;
using MTR.Models;
using System.Collections.Generic;

namespace MTR.Profiles
{
    public class ETRProfile:Profile
    {
        public ETRProfile()
        {
            CreateMap<ETR, ETRDTO>().ReverseMap();
        }
    }
}
