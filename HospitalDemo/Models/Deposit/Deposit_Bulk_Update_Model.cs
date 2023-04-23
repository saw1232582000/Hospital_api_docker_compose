namespace HospitalDemo.Models.Deposit
{
    public class Deposit_Bulk_Update_Model
    {
        public int id { get; set; }
        public int patient_id { get; set; }
        public int amount { get; set; }
        public string remark { get; set; }
        public Boolean is_cancelled { get; set; }
    }
}
