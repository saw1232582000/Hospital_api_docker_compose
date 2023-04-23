namespace HospitalDemo.Models.Category
{
    public class Category
    {
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
    }
}
