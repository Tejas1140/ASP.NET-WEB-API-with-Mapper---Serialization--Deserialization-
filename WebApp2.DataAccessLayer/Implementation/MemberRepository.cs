using Microsoft.EntityFrameworkCore;
using WebApp2.DataAccessLayer.Interface;
using WebApp2.Model.Entity;

namespace WebApp2.DataAccessLayer.Implementation
{
    internal class MemberRepository : Repository<MemberDetailsEntity>, IMemberRepository
    {
        private readonly NpgSqlDBContext _db;
        public MemberRepository(NpgSqlDBContext db) : base(db)
        {
            _db = db;
        }
    }
}
