using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp2.DataAccessLayer.DTOModel;
using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseModel;

namespace WebApp2.DataAccessLayer.HttpService
{
    public interface IApiClientService
    {
        Task<AgentDetailsResponseDTO> AddPolicyAsync(AddPolicyRequestDTO addPolicyRequestDTO, CancellationToken cancellationToken);
        Task<AgentResponseDTO> GetAll(CancellationToken cancellationToken);
    }
}
