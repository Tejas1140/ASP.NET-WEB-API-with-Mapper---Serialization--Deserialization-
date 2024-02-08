using WebApp2.BusinessLayer.EntityModel;
using WebApp2.BusinessLayer.Services.Interface;
using WebApp2.DataAccessLayer.DTOModel;
using WebApp2.DataAccessLayer.Interface;
using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseModel;

namespace WebApp2.BusinessLayer.Services.Interface
{
    public interface IAgentService
    {
        Task<CommonStatusResponse<AgentDetailsResponseEntity>> AddPolicy(AddPolicyRequestEntity addPolicyRequestEntity , CancellationToken cancellationToken);
        Task<CommonStatusResponse<AgentResponseEntity>> GetAllPolicies(CancellationToken cancellationToken);
    }
}
