using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseFormat;
using WebApp2.Model.ResponseModel;

namespace WebApp2.BusinessLayer.Services.Interface
{
    public interface IMemberService
    {
        Task<CommonStatusResponse<MemberDetailsResponseModel>> AddMember(AddMemberRequestModel requestModel, CancellationToken cancellationToken);
        Task<CommonStatusResponse<IEnumerable<MemberDetailsEntity>>> GetAllMembers(CancellationToken cancellationToken);
        Task<CommonStatusResponse<IEnumerable<MemberDetailsEntity>>> GetAllMembersAbove60(CancellationToken cancellationToken);
        Task<CommonStatusResponse<MemberDetailsEntity>> DeleteMemberById(int memberId, CancellationToken cancellationToken);
        Task<CommonStatusResponse<MemberDetailsEntity>> UpdateMemberAddress(int memberId, string newAddress, CancellationToken cancellationToken);
    }
}
