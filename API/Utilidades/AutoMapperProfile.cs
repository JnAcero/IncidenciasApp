using API.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
namespace API.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TrainerDTO,Trainer>();
            CreateMap<EmailTrainerCreationDTO,EmailsTrainers>();
            CreateMap<TelefonoTrainerCreationDTO,TelefonosTrainers>();
        }
    }
}