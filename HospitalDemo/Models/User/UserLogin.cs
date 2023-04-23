using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalDemo.Models.User
{
    [Table("User")]
    public class UserLogin
    {

        public int id { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }

    }
}
