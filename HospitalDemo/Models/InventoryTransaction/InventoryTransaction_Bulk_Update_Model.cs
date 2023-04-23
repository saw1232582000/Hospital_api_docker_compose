namespace HospitalDemo.Models.InventoryTransaction
{
    public class InventoryTransaction_Bulk_Update_Model
    {
        public int id { get; set; }
        public int inventory_item_id { get; set; }
        public string inventory_item_name { get; set; }
        public string transaction_type_name { get; set; }
        public int quantity { get; set; }
        public string unit { get; set; }
        public int purchasing_price { get; set; }
        public int selling_price { get; set; }
        public string note { get; set; }

        public int opening_balance { get; set; }
        public int closing_balance { get; set; }
        public string transaction_type { get; set; }
    }
}
