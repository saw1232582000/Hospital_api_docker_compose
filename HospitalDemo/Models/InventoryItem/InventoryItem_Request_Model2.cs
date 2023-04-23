using HospitalDemo.Models.SalesServiceItem;

namespace HospitalDemo.Models.InventoryItem
{
    public class InventoryItem_Request_Model2
    {
        public int id { get; set; }
        public string name { get; set; }
        public int balance { get; set; }
        public string unit { get; set; }
        public int purchasing_price { get; set; }
        public int sales_service_item_id { get; set; }
        public Salesserviceitem_request_mdoel sales_service_item { get; set; }
        public DateTime expiry_date { get; set; }
        public string batch { get; set; }

        public Boolean is_active { get; set; }
    }
}
