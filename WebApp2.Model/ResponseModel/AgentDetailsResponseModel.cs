using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp2.Model.ResponseModel
{
    public class AgentDetailsResponseModel
    {
        public int AgentId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public int PolicyTenure { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
