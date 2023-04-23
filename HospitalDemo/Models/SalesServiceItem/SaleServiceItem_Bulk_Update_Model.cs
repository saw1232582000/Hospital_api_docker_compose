namespace HospitalDemo.Models.SalesServiceItem
{
    public class SaleServiceItem_Bulk_Update_Model
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int uom_id { get; set; }
        public int category_id { get; set; }

        public Boolean is_active { get; set; }
    }
}
