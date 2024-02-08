using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp2.DataAccessLayer.DTOModel;

namespace WebApp2.BusinessLayer.EntityModel
{
    public class AgentResponseEntity
    {
        public List<AgentDetailsResponseEntity> AgentDetails { get; set; }
    }
}
