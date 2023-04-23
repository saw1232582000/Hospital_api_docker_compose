namespace HospitalDemo.Models.Payment
{
    public class Payment_Request_Model
    {
       
        public int bill_id { get; set; }
        public int total_amount { get; set; }
        public int total_deposit_amount { get; set; }
        public int collected_amount { get; set; }
        public int unpaid_amount { get; set; }
        public Boolean is_outstanding { get; set; }
    }
}

