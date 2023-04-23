namespace HospitalDemo.Models.DepositUsed
{
    public class Depositused_Bulk_Update_Model
    {
        public int id { get; set; }
        public int deposit_id { get; set; }
        public int payment_id { get; set; }
        public int unpaid_amount { get; set; }
        public int deposit_amount { get; set; }
    }
}
