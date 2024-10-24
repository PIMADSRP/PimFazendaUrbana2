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
            // Adicione outros mapeamentos conforme necessário
        }
    }
}
