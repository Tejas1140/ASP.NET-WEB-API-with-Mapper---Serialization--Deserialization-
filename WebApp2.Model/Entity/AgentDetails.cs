using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp2.Model.Entity
{
    public class AgentDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgentId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public int PolicyTenure { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
