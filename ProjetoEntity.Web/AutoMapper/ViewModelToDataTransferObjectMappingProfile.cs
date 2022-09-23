using AutoMapper;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Models;

namespace ProvaCandidato.AutoMapper
{
    public class ViewModelToDataTransferObjectMappingProfile : Profile
    {
        public ViewModelToDataTransferObjectMappingProfile()
        {
            CreateMap<Cliente, ClienteModel>()
                .ForMember(dest => dest.CidadeNome , op => op.MapFrom( x => x.Cidade.Nome));

            CreateMap<ClienteModel, Cliente>();
            CreateMap<CidadeModel, Cidade>();
            CreateMap<Cidade, CidadeModel>();
        }
    }
}