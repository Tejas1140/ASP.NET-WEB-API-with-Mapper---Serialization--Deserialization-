using Microsoft.AspNetCore.Mvc;
using WebApp2.ApplicationLayer.Utility;
using WebApp2.BusinessLayer.Services.Interface;
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
    public class MemberController : Controller
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService _service;
        public MemberController(IMemberService service, ILogger<MemberController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMember([FromBody] AddMemberRequestModel requestModel, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(requestModel.FirstName))
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.memberFirstNameRequired, null));
                }
                else if (string.IsNullOrWhiteSpace(requestModel.LastName))
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.memberLastNameRequired, null));
                }
                else if (string.IsNullOrWhiteSpace(requestModel.Phone) || !AppUtility.CheckMobileNumber(requestModel.Phone))
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.phoneNumberInvalid, null));
                }
                else if (string.IsNullOrWhiteSpace(requestModel.Address))
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.addressRequired, null));
                }
                else if (string.IsNullOrWhiteSpace(requestModel.Gender))
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.genderRequired, null));
                }
                else if (requestModel.Age <1 )
                {
                    return BadRequest(new Result<string>(StatusCodes.Status400BadRequest, ResponseMessages.ageRequired, null));
                }
                else
                {
                    var _responseData = await _service.AddMember(requestModel, cancellationToken);
                    if (_responseData is not null && _responseData.statusCode == 1)
                    {
                        _logger.LogInformation("Member Added Succesfully");
                        return Ok(new Result<MemberDetailsResponseModel>(StatusCodes.Status200OK, ResponseMessages.memberAddingSuccess, _responseData.Data));
                    }
                    else
                    {
                        _logger.LogInformation("Some error occured while member adding. Please try again.");
                        return StatusCode(StatusCodes.Status500InternalServerError, new Result<string>(StatusCodes.Status500InternalServerError, ResponseMessages.commonaTryAgainMsg, null));
                    }
                }
            }
            catch (OperationCanceledException cancelledEx)
            {
                _logger.LogError($"Request Cancelled in Adding Member API : {cancelledEx.ToString()}");
                return StatusCode(499, new Result<string>(499, ResponseMessages.requestCancelledByUser, null));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Adding Member API : {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, new Result<string>(StatusCodes.Status500InternalServerError, ResponseMessages.failedWithException, null));
            }
        }

        [HttpPut]
        [Route("updateMemberAddress/{memberId}/{newAddress}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMemberAddress(int memberId, string newAddress, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.UpdateMemberAddress(memberId, newAddress, cancellationToken);
                if (result is not null && result.statusCode == 1)
                {
                    _logger.LogInformation("Member Updated Succesfully");
                    return Ok(new Result<MemberDetailsEntity>(StatusCodes.Status200OK, ResponseMessages.memberUpdatingSucess, result.Data));
                }
                else
                {
                    _logger.LogInformation("Member Not Found");
                    return StatusCode(StatusCodes.Status404NotFound, new Result<string>(StatusCodes.Status404NotFound, ResponseMessages.memberNotFound, null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Updating Member API : {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, new Result<string>(StatusCodes.Status500InternalServerError, "Failed to update member address", null));
            }
        }

        [HttpGet]
        [Route("getAllMembers")]
        [ProducesResponseType(typeof(IEnumerable<MemberDetailsEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMembers(CancellationToken cancellationToken)
        {
            try
            {
                var _members = await _service.GetAllMembers(cancellationToken);
                if(_members != null)
                {
                    _logger.LogInformation("All Members Details Retrived Succesfully");
                    return Ok(new Result<IEnumerable<MemberDetailsEntity>>(StatusCodes.Status200OK, ResponseMessages.memberGettingSuccess, _members.Data));
                }
                else
                {
                    _logger.LogInformation("Member not found ");
                    return NotFound(new Result<IEnumerable<MemberDetailsEntity>>(StatusCodes.Status404NotFound, "Member not found", null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Get All Members API: {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, new Result<string>(StatusCodes.Status500InternalServerError, "Failed to retrieve members", null));
            }
        }

        [HttpGet]
        [Route("memberAbove60")]
        [ProducesResponseType(typeof(IEnumerable<MemberDetailsEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMembersAbove60(CancellationToken cancellationToken)
        {
            try
            {
                var _members60 = await _service.GetAllMembersAbove60(cancellationToken);
                if (_members60 != null)
                {
                    _logger.LogInformation("All Members Details Whose Age Above 60 Retrived Succesfully");
                    return Ok(new Result<IEnumerable<MemberDetailsEntity>>(StatusCodes.Status200OK, ResponseMessages.memberGettingSuccess60, _members60.Data));
                }
                else
                {
                    _logger.LogInformation("Members not found ");
                    return NotFound(new Result<IEnumerable<MemberDetailsEntity>>(StatusCodes.Status404NotFound, "Members not found", null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Get All Members whose age above 60 API: {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, new Result<string>(StatusCodes.Status500InternalServerError, "Failed to retrieve members", null));
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMemberById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.DeleteMemberById(id, cancellationToken);

                if (result.statusCode == 1)
                {
                    _logger.LogInformation("Member deleted successfully");
                    return Ok(new Result<string>(StatusCodes.Status200OK, "Member deleted successfully", null));

                }
                else
                {
                    _logger.LogInformation("Member not found for deletion");
                    return NotFound(new Result<string>(StatusCodes.Status404NotFound, "Member not found", null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Delete Member API: {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, new Result<string>(StatusCodes.Status500InternalServerError, "Failed to delete member", null));
            }
        }
    }
}
