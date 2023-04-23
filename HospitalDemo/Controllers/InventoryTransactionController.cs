using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.InventoryTransaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryTransactionController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public InventoryTransactionController(HospitalDbContext dbContext)
        {
                this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get_all()
        {
            return Ok(await dbContext.inventorytransaction.ToListAsync());
        }

        [HttpGet]
        [Route("get_all_with_inventory_item")]
        public async Task<IActionResult> Get_all_itransaction()
        {
           var alldata= await dbContext.inventorytransaction.ToListAsync();
            List<InventoryTransaction_Request_Model2>inventorytransaction=new List<InventoryTransaction_Request_Model2>();

            foreach(var transaction in alldata)
            {
                var inventory_item = dbContext.inventoryitem.FirstOrDefault(i => i.id == transaction.inventory_item_id);
                if(inventory_item==null)
                {
                    return NotFound();
                }

                InventoryTransaction_Request_Model2 itran=new InventoryTransaction_Request_Model2();
                itran.id = transaction.id;
                itran.created_time = transaction.created_time;
                itran.updated_time = transaction.updated_time;
                itran.inventory_item_id = transaction.inventory_item_id;
                itran.inventory_item_name = inventory_item.name;
                itran.batch = inventory_item.batch;
                itran.transaction_type_name=transaction.transaction_type_name;
                itran.quantity=transaction.quantity; 
                itran.unit=transaction.unit;
                itran.purchasing_price=transaction.purchasing_price;
                itran.selling_price = transaction.selling_price;
                itran.note = transaction.note;
                itran.created_user_id = transaction.created_user_id;
                itran.updated_user_id = transaction.updated_user_id;
                itran.opening_balance = transaction.opening_balance;
                itran.closing_balance = transaction.closing_balance;
                itran.transaction_type = transaction.transaction_type;
                inventorytransaction.Add(itran);
             }
            return Ok(inventorytransaction);
        }

        [HttpGet]
        [Route("get_one_with_inventory_item/{id}")]
        public async Task<IActionResult> Get_one_itransaction(int id)
        {
            var transaction = await dbContext.inventorytransaction.FirstOrDefaultAsync(t => t.id == id);
           
            if(transaction == null)
            {
                return NotFound();
            }
            
                var inventory_item = dbContext.inventoryitem.FirstOrDefault(i => i.id == transaction.inventory_item_id);
                if (inventory_item == null)
                {
                    return NotFound();
                }

                InventoryTransaction_Request_Model2 itran = new InventoryTransaction_Request_Model2();
                itran.id = transaction.id;
                itran.created_time = transaction.created_time;
                itran.updated_time = transaction.updated_time;
                itran.inventory_item_id = transaction.inventory_item_id;
                itran.inventory_item_name = inventory_item.name;
                itran.batch = inventory_item.batch;
                itran.transaction_type_name = transaction.transaction_type_name;
                itran.quantity = transaction.quantity;
                itran.unit = transaction.unit;
                itran.purchasing_price = transaction.purchasing_price;
                itran.selling_price = transaction.selling_price;
                itran.note = transaction.note;
                itran.created_user_id = transaction.created_user_id;
                itran.updated_user_id = transaction.updated_user_id;
                itran.opening_balance = transaction.opening_balance;
                itran.closing_balance = transaction.closing_balance;
                itran.transaction_type = transaction.transaction_type;
               
           
            return Ok(itran);
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute] int id)
        {
            var data = await dbContext.inventorytransaction.FirstOrDefaultAsync(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("add_it")]
        public async Task<IActionResult> Add_it([FromBody]InventoryTransaction_Request_Model it)
        {
            var it_to_add = new Inventorytransactions();
            it_to_add.id = rng.Next(1, 1001);
            it_to_add.created_time = DateTime.UtcNow;
            it_to_add.updated_time = DateTime.UtcNow;
            it_to_add.inventory_item_id = it.inventory_item_id;
            it_to_add.inventory_item_name = it.inventory_item_name;
            it_to_add.transaction_type_name = it.transaction_type_name;
            it_to_add.quantity = it.quantity;
            it_to_add.unit = it.unit;
            it_to_add.purchasing_price = it.purchasing_price;
            it_to_add.selling_price = it.selling_price;
            it_to_add.note = it.note;
            it_to_add.created_user_id = 0;
            it_to_add.updated_user_id = 0;
            it_to_add.opening_balance = it.opening_balance;
            it_to_add.closing_balance = it.closing_balance;
            it_to_add.transaction_type = it.transaction_type;

            await dbContext.inventorytransaction.AddAsync(it_to_add);
            await dbContext.SaveChangesAsync();
            return Ok(it_to_add);
        }

        [HttpPut]
        [Route("update_it/{id}")]
        public async Task<IActionResult> Update_it([FromRoute]int id, [FromBody] InventoryTransaction_Request_Model it)
        {
            var it_to_update = await dbContext.inventorytransaction.FirstOrDefaultAsync(i => i.id == id);

            if(it_to_update==null)
            {
                return NotFound();
            }
           
            
            it_to_update.updated_time = DateTime.UtcNow;
            it_to_update.inventory_item_id = it.inventory_item_id;
            it_to_update.inventory_item_name = it.inventory_item_name;
            it_to_update.transaction_type_name = it.transaction_type_name;
            it_to_update.quantity = it.quantity;
            it_to_update.unit = it.unit;
            it_to_update.purchasing_price = it.purchasing_price;
            it_to_update.selling_price = it.selling_price;
            it_to_update.note = it.note;
            
            it_to_update.opening_balance = it.opening_balance;
            it_to_update.closing_balance = it.closing_balance;
            it_to_update.transaction_type = it.transaction_type;

            dbContext.inventorytransaction.Update(it_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(it_to_update);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<InventoryTransaction_Bulk_Update_Model> bulk_data)
        {
            foreach (var b in bulk_data)
            {
                var it_to_update = await dbContext.inventorytransaction.FirstOrDefaultAsync(i => i.id == b.id);

                if (it_to_update == null)
                {
                    return NotFound();
                }


                it_to_update.updated_time = DateTime.UtcNow;
                it_to_update.inventory_item_id = b.inventory_item_id;
                it_to_update.inventory_item_name = b.inventory_item_name;
                it_to_update.transaction_type_name = b.transaction_type_name;
                it_to_update.quantity = b.quantity;
                it_to_update.unit = b.unit;
                it_to_update.purchasing_price = b.purchasing_price;
                it_to_update.selling_price = b.selling_price;
                it_to_update.note = b.note;

                it_to_update.opening_balance = b.opening_balance;
                it_to_update.closing_balance = b.closing_balance;
                it_to_update.transaction_type = b.transaction_type;

                dbContext.inventorytransaction.Update(it_to_update);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("Delete_it/{id}")]
        public async Task<IActionResult> Update_it([FromRoute] int id)
        {
            var it_to_delete = await dbContext.inventorytransaction.FirstOrDefaultAsync(i => i.id == id);
            if(it_to_delete == null)
            {
                return NotFound();
            }
            dbContext.inventorytransaction.Remove(it_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok("item deleted");
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var it_to_delete = await dbContext.inventorytransaction.FirstOrDefaultAsync(i => i.id == id);
                if (it_to_delete == null)
                {
                    return NotFound();
                }
                dbContext.inventorytransaction.Remove(it_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete complete");
        }

    }
}
