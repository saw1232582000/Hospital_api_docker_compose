
using HospitalDemo.Models.Category;
using HospitalDemo.Models.UOM;
namespace HospitalDemo.Models.SalesServiceItem
{
    public class SaleServiceItem_request_model2
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int uom_id { get; set; }
        public int category_id { get; set; }
        public UOM_Request_model uom { get; set; }
        public Category_Request_model category{ get; set; }
        public DateTime created_time { get; set; }
        public Boolean is_active { get; set; }
    }
}
