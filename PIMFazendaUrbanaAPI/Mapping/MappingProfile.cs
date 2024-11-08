using AutoMapper;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;

namespace PIMFazendaUrbanaAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Telefone, TelefoneDTO>().ReverseMap();
            CreateMap<Funcionario, FuncionarioDTO>().ReverseMap();
            CreateMap<EnderecoViaCepDTO, EnderecoDTO>()
            .ForMember(dest => dest.CEP, opt => opt.MapFrom(src => src.cep))
            .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.logradouro))
            .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.complemento))
            .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.bairro))
            .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.localidade))
            .ForMember(dest => dest.UF, opt => opt.MapFrom(src => src.uf));
            // adicionar outros mapeamentos conforme necessário
        }
    }
}
