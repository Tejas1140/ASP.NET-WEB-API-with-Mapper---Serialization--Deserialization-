using AutoMapper;
using WebApp2.BusinessLayer.EntityModel;
using WebApp2.DataAccessLayer.DTOModel;
using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseModel;

namespace WebApp2.ApplicationLayer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddPolicyRequestModel, AddPolicyRequestEntity>().ReverseMap();
            CreateMap<AddPolicyRequestEntity, AddPolicyRequestDTO>().ReverseMap();
            CreateMap<AgentDetailsResponseEntity, AgentDetailsResponseDTO>().ReverseMap();
            CreateMap<AgentDetailsResponseModel, AgentDetailsResponseEntity>().ReverseMap();

            CreateMap<AgentResponseEntity, AgentResponseDTO>().ReverseMap();
            CreateMap<AgentResponseModel, AgentResponseEntity>().ReverseMap();
        }
    }
}
