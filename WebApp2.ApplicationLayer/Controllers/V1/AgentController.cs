using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp2.ApplicationLayer.Utility;
using WebApp2.BusinessLayer.EntityModel;
using WebApp2.BusinessLayer.Services.Implementation;
using WebApp2.BusinessLayer.Services.Interface;
using WebApp2.DataAccessLayer.DTOModel;
using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseFormat;
using WebApp2.Model.ResponseModel;

namespace WebApp2.ApplicationLayer.Controllers.V1
{
    [ApiController]
    [Route("/api/[Controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AgentController : Controller
    {
        private readonly ILogger<AgentController> _logger;
        private readonly IAgentService _service;
        private readonly IMapper _mapper;

        public AgentController(IAgentService service, ILogger<AgentController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("addPolicy")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPolicy([FromBody] AddPolicyRequestModel requestModel, CancellationToken cancellationToken)
        {
            try
            {
                if (requestModel.PolicyId < 1)
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.policyIdRequired, null));
                }
                else if (string.IsNullOrWhiteSpace(requestModel.PolicyName))
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.policyNameRequired, null));
                }
                else if (requestModel.PolicyTenure is 0)
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.policyTenureRequired, null));
                }
                else if (requestModel.StartDate == null || requestModel.EndDate == null || requestModel.StartDate >= requestModel.EndDate)
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.invalidDates, null));
                }
                else
                {
                    // Map AddPolicyRequestModel to the appropriate model
                    var _requestEntity = _mapper.Map<AddPolicyRequestEntity>(requestModel);
                    // Call the service method with the mapped model
                    var _responseData = await _service.AddPolicy(_requestEntity, cancellationToken);
                    //var _responseData = await _service.AddPolicy(requestModel, cancellationToken);
                    if (_responseData is not null && _responseData.statusCode == 1)
                    {
                        _logger.LogInformation("Policy Added Succesfully");
                        var _result = _mapper.Map<AgentDetailsResponseModel>(_responseData.Data);
                        return Ok(new Result<AgentDetailsResponseModel>(StatusCodes.Status200OK, ResponseMessages.policyAddingSucess, _result));
                    }
                    else
                    {
                        _logger.LogInformation("Some error occured while policy adding. Please try again.");
                        return StatusCode(StatusCodes.Status500InternalServerError, new Result<string>(StatusCodes.Status500InternalServerError, ResponseMessages.commonaTryAgainMsg, null));
                    }
                }
            }
            catch (OperationCanceledException cancelledEx)
            {
                _logger.LogError($"Request Cancelled in Adding Policy API : {cancelledEx.ToString()}");
                return StatusCode(499, new Result<string>(499, ResponseMessages.requestCancelledByUser, null));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Adding Policy API : {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, new Result<string>(StatusCodes.Status500InternalServerError, ResponseMessages.failedWithException, null));
            }
        }

        [HttpGet]
        [Route("getAllPolicies")]
        [ProducesResponseType(typeof(Result<AgentResponseEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPolicies(CancellationToken cancellationToken)
        {
            try
            {
                var _policies = await _service.GetAllPolicies(cancellationToken);
                if (_policies != null)
                {
                    _logger.LogInformation("All Policies Details Retrived Succesfully");
                    var _result = _mapper.Map<AgentResponseModel>(_policies.Data);
                    return Ok(new Result<AgentResponseModel>(StatusCodes.Status200OK, ResponseMessages.policyGettingSuccess, _result));
                }
                else
                {
                    _logger.LogInformation("Policies not found ");
                    return NotFound(new Result<IEnumerable<MemberDetailsEntity>>(StatusCodes.Status404NotFound, "Policies not found", null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Get All Policies API: {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, new Result<string>(StatusCodes.Status500InternalServerError, "Failed to retrieve Policies", null));
            }
        }
    }
}
