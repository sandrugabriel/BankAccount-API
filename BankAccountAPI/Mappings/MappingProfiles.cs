using AutoMapper;
using BankAccountAPI.Dto;
using BankAccountAPI.Models;

namespace BankAccountAPI.Mappings
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {


            CreateMap<CreateBankRequest,BankAccount>();
        }


    }
}
