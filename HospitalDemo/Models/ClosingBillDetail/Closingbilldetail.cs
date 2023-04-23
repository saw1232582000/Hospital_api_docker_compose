namespace HospitalDemo.Models.ClosingBillDetail
{
    public class Closingbilldetail
    {
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public int daily_closing_id { get; set; }
        public int bill_id { get; set; }
        public int amount { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
    }
}
