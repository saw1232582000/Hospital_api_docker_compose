namespace HospitalDemo.Models.InventoryItem
{
    public class Inventory_Request_Model
    {
        
       
        public string name { get; set; }
        public int balance { get; set; }
        public string unit { get; set; }
        public int purchasing_price { get; set; }
        public int sales_service_item_id { get; set; }
        public DateTime expiry_date { get; set; }
        public string batch { get; set; }
        
        public Boolean is_active { get; set; }
    }
}
