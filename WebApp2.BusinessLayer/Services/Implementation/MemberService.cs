using Microsoft.EntityFrameworkCore;
using WebApp2.BusinessLayer.Services.Interface;
using WebApp2.DataAccessLayer.Interface;
using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseFormat;
using WebApp2.Model.ResponseModel;


namespace WebApp2.BusinessLayer.Services.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _repository;
        public MemberService(IUnitOfWork repository)
        {
            _repository = repository;
        }
        public async Task<CommonStatusResponse<MemberDetailsResponseModel>> AddMember(AddMemberRequestModel requestModel, CancellationToken cancellationToken)
        {
            CommonStatusResponse<MemberDetailsResponseModel> _responseModel = new CommonStatusResponse<MemberDetailsResponseModel>();
            var _otpMasterObj = new MemberDetailsEntity()
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Phone = requestModel.Phone,
                Address = requestModel.Address,
                Gender = requestModel.Gender,
                Age = requestModel.Age,
            };

            var response = await _repository.MemberRepository.AddAsync(_otpMasterObj, cancellationToken);
            await _repository.save(cancellationToken);
            if (response != null)
            {
                MemberDetailsResponseModel _memberResponse = new MemberDetailsResponseModel()
                {
                    Address = response.Address,
                    Gender = response.Gender,
                    Age = response.Age,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    Phone = response.Phone,
                    MemberId = response.MemberId
                };
                _responseModel.statusCode = 1;
                _responseModel.statusMsg = "Success";
                _responseModel.Data = _memberResponse;
            }
            else
            {
                _responseModel.statusCode = 0;
                _responseModel.statusMsg = "Failed";
                _responseModel.Data = null;
            }
            return _responseModel;
        }
        public async Task<CommonStatusResponse<IEnumerable<MemberDetailsEntity>>> GetAllMembers(CancellationToken cancellationToken)
        {
            CommonStatusResponse<IEnumerable<MemberDetailsEntity>> _responseModel = new CommonStatusResponse<IEnumerable<MemberDetailsEntity>>();
            var _members = await _repository.MemberRepository.GetAll(cancellationToken);
            if(_members != null) 
            {
                _responseModel.statusCode = 1;
                _responseModel.statusMsg = "Success";
                _responseModel.Data = _members;
            }
            else
            {
                _responseModel.statusCode = 0;
                _responseModel.statusMsg = "Failed";
                _responseModel.Data = null;
            }
            return _responseModel;
        }

        public async Task<CommonStatusResponse<IEnumerable<MemberDetailsEntity>>> GetAllMembersAbove60(CancellationToken cancellationToken)
        {
            CommonStatusResponse<IEnumerable<MemberDetailsEntity>> _responseModel = new CommonStatusResponse<IEnumerable<MemberDetailsEntity>>();
            var _membersAbove60 = await (await _repository.MemberRepository.GetAllAsQueryable())
                .Where(member => member.Age > 60)
                .ToListAsync(cancellationToken);
            if(_membersAbove60 != null ) 
            {
                _responseModel.statusCode = 1;
                _responseModel.statusMsg = "Success";
                _responseModel.Data = _membersAbove60;
            }
            else
            {
                _responseModel.statusCode = 0;
                _responseModel.statusMsg = "Failed";
                _responseModel.Data = null;
            }
            return _responseModel;
        }
        public async Task<CommonStatusResponse<MemberDetailsEntity>> DeleteMemberById(int memberId, CancellationToken cancellationToken)
        {
            var isDeleted = await _repository.MemberRepository.DeleteByIdAsync(memberId, cancellationToken);
            CommonStatusResponse<MemberDetailsEntity> _responseModel = new CommonStatusResponse<MemberDetailsEntity>();
            await _repository.save(cancellationToken);
            if (isDeleted)
            {
                _responseModel.statusCode = 1;
                _responseModel.statusMsg = "Success";
                _responseModel.Data = null;
            }
            else
            {
                _responseModel.statusCode = 0;
                _responseModel.statusMsg = "Failed";
                _responseModel.Data = null;
            }
            return _responseModel;
        }

        public async Task<CommonStatusResponse<MemberDetailsEntity>> UpdateMemberAddress(int memberId, string newAddress, CancellationToken cancellationToken)
        {
            CommonStatusResponse<MemberDetailsEntity> _responseModel = new CommonStatusResponse<MemberDetailsEntity>();

            MemberDetailsEntity memberToUpdate = new();
            memberToUpdate = await _repository.MemberRepository.FindAsync(member => member.MemberId == memberId, cancellationToken);
            memberToUpdate.Address = newAddress;
            var _entityResponse = _repository.MemberRepository.Update(memberToUpdate);
            await _repository.save(cancellationToken);
            if (_entityResponse is not null)
            {
                _responseModel.statusCode = 1;
                _responseModel.statusMsg = "Success";
                _responseModel.Data = memberToUpdate;
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
