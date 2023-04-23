using System.ComponentModel;

namespace HospitalDemo.Models.Patient
{
    public enum gender_enum
    {
        [Description("male")]
        male,
        [Description("female")]
        female
    }
    public class Patient
    {
        
        public int id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }

        public DateTime date_of_birth { get; set; }
        public int age { get; set; }
        public string address { get; set; }
        public string contact_details { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }


    }
}
