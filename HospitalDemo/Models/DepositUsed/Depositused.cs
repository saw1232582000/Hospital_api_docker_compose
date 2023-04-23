namespace HospitalDemo.Models.DepositUsed
{
    public class Depositused
    {
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public int deposit_id { get; set; }
        public int payment_id { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
        public int unpaid_amount { get; set; }
        public int deposit_amount { get; set; }
    }
}
