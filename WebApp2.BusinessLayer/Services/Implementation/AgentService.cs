using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using WebApp2.BusinessLayer.EntityModel;
using WebApp2.BusinessLayer.Services.Interface;
using WebApp2.DataAccessLayer.DTOModel;
using WebApp2.DataAccessLayer.HttpService;
using WebApp2.DataAccessLayer.Interface;
using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseModel;

namespace WebApp2.BusinessLayer.Services.Implementation
{
    public class AgentService : IAgentService
    {
       
        private readonly IApiClientService _apiClient;
        private IMapper _mapper; 
        public AgentService(IUnitOfWork repository, IApiClientService apiClient, IMapper mapper)
        {
           _mapper = mapper;
           _apiClient = apiClient;
        }
        public async Task<CommonStatusResponse<AgentDetailsResponseEntity>> AddPolicy(AddPolicyRequestEntity addPolicyRequestEntity , CancellationToken cancellationToken)
        {
            CommonStatusResponse<AgentDetailsResponseEntity> _responseModel = new CommonStatusResponse<AgentDetailsResponseEntity>();

            var requestDTO = _mapper.Map<AddPolicyRequestDTO>(addPolicyRequestEntity);
            var response = await _apiClient.AddPolicyAsync(requestDTO, cancellationToken);
            
            if (response != null)
            {
                var _responseEntity = _mapper.Map<AgentDetailsResponseEntity>(response);
                _responseModel.statusCode = 1;
                _responseModel.statusMsg = "Success";
                _responseModel.Data = _responseEntity;
            }
            else
            {
                _responseModel.statusCode = 0;
                _responseModel.statusMsg = "Failed";
                _responseModel.Data = null;
            }
            return _responseModel;
        }

        public async Task<CommonStatusResponse<AgentResponseEntity>> GetAllPolicies(CancellationToken cancellationToken)
        {
            CommonStatusResponse<AgentResponseEntity> _responseModel = new CommonStatusResponse<AgentResponseEntity>();
            var _policies = await _apiClient.GetAll(cancellationToken);
            if (_policies != null)
            {
                var _responseEntity = _mapper.Map<AgentResponseEntity>(_policies);
                _responseModel.statusCode = 1;
                _responseModel.statusMsg = "Success";
                _responseModel.Data = _responseEntity;
            }
            else
            {
                _responseModel.statusCode = 0;
                _responseModel.statusMsg = "Failed";
                _responseModel.Data = null;
            }
            return _responseModel;
        }
    }
}
