namespace HospitalDemo.Models.DailyClosing
{
    public class Dailyclosing
    {
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public int opening_balance { get; set; }
        public int deposit_total { get; set; }
        public int bill_total { get; set; }
        public int grand_total { get; set; }
        public int actual_amount { get; set; }
        public int adjusted_amount { get; set; }
        public string adjusted_reason { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
    }
}
