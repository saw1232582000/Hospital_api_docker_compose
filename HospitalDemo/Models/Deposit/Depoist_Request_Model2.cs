using HospitalDemo.Models.Patient;

namespace HospitalDemo.Models.Deposit
{
    public class Depoist_Request_Model2
    {
        public int id { get; set; }
        public int patient_id { get; set; }
        public Patient.Patient patient { get; set; }
        public int amount { get; set; }
        public string remark { get; set; }
        public Boolean is_cancelled { get; set; }
    }
}
