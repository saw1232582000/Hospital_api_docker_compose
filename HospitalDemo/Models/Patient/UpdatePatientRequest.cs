namespace HospitalDemo.Models.Patient
{
    public class UpdatePatientRequest
    {
        public enum gender
        {
            Male,
            Female
        }

        public string Name { get; set; }
        public gender Gender { get; set; }

        public DateTime DOB { get; set; }
        public int age { get; set; }
        public string Address { get; set; }
        public string Contact_Detail { get; set; }
        // public DateTime created_time { get; set; }
        // public DateTime updated_time { get; set; }
        public int Created_user_id { get; set; }
        public int Updated_user_id { get; set; }
    }
}
