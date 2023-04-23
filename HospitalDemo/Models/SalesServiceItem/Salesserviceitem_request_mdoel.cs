namespace HospitalDemo.Models.SalesServiceItem
{
    public class Salesserviceitem_request_mdoel
    {
       
        public string name { get; set; }
        public int price { get; set; }
        public int uom_id { get; set; }
        public int category_id { get; set; }
        
        public Boolean is_active { get; set; }
    }
}
