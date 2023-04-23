namespace HospitalDemo.Models.BillItem
{
    public class BillItem_Request_Model
    {
        public int id { get; set; }
        public int bill_id { get; set; }
        public int sales_service_item_id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string uom { get; set; }
        public int price { get; set; }
        public int subtotal { get; set; }
        public string remark { get; set; }
        
    }
}
