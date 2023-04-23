namespace HospitalDemo.Models.Deposit
{
    public class Deposit
    {
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public int patient_id { get; set; }
        public int amount { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
        public string remark { get; set; }
        public Boolean is_cancelled { get; set; }
    }
}
