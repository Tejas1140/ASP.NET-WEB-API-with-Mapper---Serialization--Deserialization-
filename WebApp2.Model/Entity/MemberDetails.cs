using System.ComponentModel.DataAnnotations;

namespace WebApp2.Model.Entity
{
    public class MemberDetailsEntity
    {
        [Key]
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}
