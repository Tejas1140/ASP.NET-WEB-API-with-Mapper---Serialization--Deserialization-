using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp2.Model.Entity;

namespace WebApp2.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNull] DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<AgentDetails> Agent { get; set; }
    }
}
