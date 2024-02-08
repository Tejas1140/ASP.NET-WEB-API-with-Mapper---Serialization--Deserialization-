using WebApp2.DataAccessLayer.HttpService;
using WebApp2.DataAccessLayer.Interface;

namespace WebApp2.DataAccessLayer.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NpgSqlDBContext _db;
        public IMemberRepository MemberRepository { get; set; }
        public IApiClientService ApiClientService { get; set; }
        
        public UnitOfWork(NpgSqlDBContext db)
        {
            _db = db;
            MemberRepository = new MemberRepository(_db);
        }

        public async Task save(CancellationToken cancellationToken)
        {
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(true);
        }
    }
}
