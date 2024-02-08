using WebApp2.DataAccessLayer.HttpService;

namespace WebApp2.DataAccessLayer.Interface
{
    public interface IUnitOfWork
    {
        IMemberRepository MemberRepository { get; set; }
        IApiClientService ApiClientService { get; set; }    
        Task save(CancellationToken cancellationToken);
    }
}
