namespace HospitalDemo.Models.Transactiontype
{
    public enum type_enum
    {

    }
    public class Transactiontype
    {
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
    }
}
