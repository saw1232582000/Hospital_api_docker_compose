namespace HospitalDemo.Models.Bill
{
    public class Bill_Add_Model
    {
        public int patient_id { get; set; }
        public string patient_name { get; set; }
        public string patient_phone { get; set; }
        public int total_amount { get; set; }
        public string printed_or_drafted { get; set; }
        public string patient_address { get; set; }
        public Boolean is_cancelled { get; set; }
        public List<BillItem.BillItem_Request_Model> bill_items { get; set; }
    }
}
