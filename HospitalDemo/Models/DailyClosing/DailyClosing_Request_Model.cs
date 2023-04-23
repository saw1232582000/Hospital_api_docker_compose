namespace HospitalDemo.Models.DailyClosing
{
    public class DailyClosing_Request_Model
    {
        
        public int opening_balance { get; set; }
        public int deposit_total { get; set; }
        public int bill_total { get; set; }
        public int grand_total { get; set; }
        public int actual_amount { get; set; }
        public int adjusted_amount { get; set; }
        public string adjusted_reason { get; set; }
       
    }
}
