using AutoMapper;
using Poliedro.Client.Application.Client.Commands.CreateClientPos;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Application.Client.Mappers
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientLegalPosCommand, ClientLegalPosEntity>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.DocumentType, opt => opt.Ignore());

            CreateMap<CreateClientNaturalPosCommand, ClientNaturalPosEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentType, opt => opt.Ignore());

            CreateMap<ClientEntity, ClientDto>();
        }
    }
}
