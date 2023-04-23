namespace HospitalDemo.Models.SalesServiceItem
{
    public class Salesserviceitem
    {
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int uom_id { get; set; }
        public int category_id { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
        public Boolean is_active { get; set; }  
    }
}
