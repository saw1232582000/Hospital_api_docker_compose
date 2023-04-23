using HospitalDemo.Models.BillItem;
using HospitalDemo.Models.Patient;
using HospitalDemo.Models.Payment;

namespace HospitalDemo.Models.Bill
{
    public class Bill_Request_Model2
    {
        
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public int patient_id { get; set; }
        public string patient_name { get; set; }
        public string patient_phone { get; set; }
        public Patient.Patient patient { get; set; }
        public List< Payment.Payment> payment { get; set; }
        public List<Billitem> billitems { get; set; }
        public int total_amount { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
        public string printed_or_drafted { get; set; }
        public string patient_address { get; set; }
        public Boolean is_cancelled { get; set; }
    }
}
