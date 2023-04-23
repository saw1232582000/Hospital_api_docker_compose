using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.BillItem;
using HospitalDemo.Models.InventoryItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryItemController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public InventoryItemController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;  
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Get_allItem()
        {
            return Ok(await dbContext.inventoryitem.ToListAsync());
        }

        [HttpGet]
        [Route("all_with_ss")]
        public async Task<IActionResult> Get_allItem_with_ss()
        {
            var all_item = await dbContext.inventoryitem.ToListAsync();
            List<InventoryItem_Request_Model2> inventoryitem = new List<InventoryItem_Request_Model2>();
            foreach(var item in all_item)
            {
                var ssitem= await dbContext.salesserviceitem.FirstOrDefaultAsync(b => b.id == item.sales_service_item_id);
                if(ssitem == null)
                {
                    return NotFound();
                }
                InventoryItem_Request_Model2 iitem = new InventoryItem_Request_Model2();
                iitem.sales_service_item = new Models.SalesServiceItem.Salesserviceitem_request_mdoel();
                iitem.id = item.id;
                iitem.name = item.name;
                iitem.balance = item.balance;
                iitem.unit = item.unit;
                iitem.purchasing_price = item.purchasing_price;
                iitem.sales_service_item_id=item.sales_service_item_id;
                iitem.sales_service_item.price = ssitem.price;
                iitem.sales_service_item.name = ssitem.name;
                iitem.sales_service_item.uom_id = ssitem.uom_id;
                iitem.sales_service_item.category_id = ssitem.category_id;
                iitem.expiry_date = item.expiry_date;
                iitem.batch = item.batch;
                iitem.is_active = item.is_active;
                inventoryitem.Add(iitem);
            }
            return Ok(inventoryitem);
        }

        [HttpGet]
        [Route("one_item_with_ss/{id}")]
        public async Task<IActionResult> Get_oneItem_with_ss([FromRoute] int id)
        {
            var item = dbContext.inventoryitem.FirstOrDefault(i => i.id == id);
            if(item == null)
            {
                return NotFound();
            }
            var ssitem = await dbContext.salesserviceitem.FirstOrDefaultAsync(b => b.id == item.sales_service_item_id);
                if (ssitem == null)
                {
                    return NotFound();
                }
                InventoryItem_Request_Model2 iitem = new InventoryItem_Request_Model2();
                iitem.sales_service_item = new Models.SalesServiceItem.Salesserviceitem_request_mdoel();
                iitem.id = item.id;
                iitem.name = item.name;
                iitem.balance = item.balance;
                iitem.unit = item.unit;
                iitem.purchasing_price = item.purchasing_price;
                iitem.sales_service_item_id = item.sales_service_item_id;
                iitem.sales_service_item.price = ssitem.price;
                iitem.sales_service_item.name = ssitem.name;
                iitem.sales_service_item.uom_id = ssitem.uom_id;
                iitem.sales_service_item.category_id = ssitem.category_id;
                iitem.expiry_date = item.expiry_date;
                iitem.batch = item.batch;
                iitem.is_active = item.is_active;
               
           
            return Ok(iitem);
        }

        [HttpGet]
        [Route("get_one_item/{id}")]
        public IActionResult Get_item_by_id([FromRoute]int id)
        {
            var item = dbContext.inventoryitem.FirstOrDefault(i => i.id==id);
            if(item==null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("Add_item")]
        public async Task<IActionResult> Add_item([FromBody]Inventory_Request_Model item_info)
        {
            var item_to_add = new Inventoryitem()
            {
                id = rng.Next(1, 1001),
                name = item_info.name,
                created_time = DateTime.UtcNow,
                updated_time = DateTime.UtcNow,
                balance = item_info.balance,
                unit = item_info.unit,
                purchasing_price = item_info.purchasing_price,
                sales_service_item_id = item_info.sales_service_item_id,
                expiry_date = item_info.expiry_date,
                batch = item_info.batch,
                created_user_id = 0,
                updated_user_id =0,
                is_active = item_info.is_active
            };
            await dbContext.inventoryitem.AddAsync(item_to_add);
            await dbContext.SaveChangesAsync();
            return Ok(item_to_add);
        }

        //[HttpPost]
        //[Route("Get_all_inventory_by_billItems")]
        //public async Task<IActionResult> Get_all_inv_by_bill_item([FromBody] BillItem_Request_Model bill_items)
        //{
          
        //}

        [HttpPut]
        [Route("update_item/{id}")]
        public async Task<IActionResult> Update_item([FromRoute]int id, [FromBody] Inventory_Request_Model item_info)
        {
            var item_to_update = await dbContext.inventoryitem.FirstOrDefaultAsync(i => i.id == id);
            if(item_to_update == null)
            {
                return NotFound("item not found");
            }
            item_to_update.name = item_info.name;
            
            item_to_update.updated_time = DateTime.UtcNow;
            item_to_update.balance = item_info.balance;
            item_to_update.unit = item_info.unit;
            item_to_update.purchasing_price = item_info.purchasing_price;
            item_to_update.sales_service_item_id = item_info.sales_service_item_id;
            item_to_update.expiry_date = item_info.expiry_date;
            item_to_update.batch = item_info.batch;
            item_to_update.created_user_id = 0;
            item_to_update.updated_user_id = 0;
            item_to_update.is_active = item_info.is_active;
           
           dbContext.inventoryitem.Update(item_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(item_to_update);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<InventoryItem_Bulk_Update_Model> bulk_data)
        {
            foreach (var b in bulk_data)
            {
                var item_to_update = await dbContext.inventoryitem.FirstOrDefaultAsync(i => i.id == b.id);
                if (item_to_update == null)
                {
                    return NotFound("item not found");
                }
                item_to_update.name = b.name;

                item_to_update.updated_time = DateTime.UtcNow;
                item_to_update.balance = b.balance;
                item_to_update.unit = b.unit;
                item_to_update.purchasing_price = b.purchasing_price;
                item_to_update.sales_service_item_id = b.sales_service_item_id;
                item_to_update.expiry_date = b.expiry_date;
                item_to_update.batch = b.batch;
                item_to_update.created_user_id = 0;
                item_to_update.updated_user_id = 0;
                item_to_update.is_active = b.is_active;

                dbContext.inventoryitem.Update(item_to_update);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("Delete_one_item/{id}")]
        public async Task<IActionResult> Delete_item([FromRoute] int id)
        {
            var item_to_delete= await dbContext.inventoryitem.FirstOrDefaultAsync(i => i.id == id);
            if(item_to_delete == null)
            {
                return NotFound($"item with id {id} not found");
            }
            dbContext.inventoryitem.Remove(item_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok("item deleted");
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var item_to_delete = await dbContext.inventoryitem.FirstOrDefaultAsync(i => i.id == id);
                if (item_to_delete == null)
                {
                    return NotFound($"item with id {id} not found");
                }
                dbContext.inventoryitem.Remove(item_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete complete");
        }
    }
}
