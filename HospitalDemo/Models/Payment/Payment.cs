namespace HospitalDemo.Models.Payment
{
    public class Payment
    {
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public int bill_id { get; set; }
        public int total_amount { get; set; }
        public int total_deposit_amount { get; set; }
        public int collected_amount { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
        public int unpaid_amount { get; set; }
        public Boolean is_outstanding { get; set; }
    }
}
