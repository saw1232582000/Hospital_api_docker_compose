using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.BillItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillItemController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public BillItemController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("get_all_billitem")]
        public async Task<IActionResult> Get_bill()
        {
            return Ok(await dbContext.billitem.ToListAsync());
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute] int id)
        {
            var data = await dbContext.billitem.FirstOrDefaultAsync(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("get_by_bill_id/{id}")]
        public async Task<IActionResult> Get_bill_by_billid([FromRoute] int id)
        {
            var data =await dbContext.billitem.Where(b => b.bill_id == id).ToListAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [Authorize]
        [HttpPost]
        [Route("Add_billitem")]
        public async Task<IActionResult> Add_billitem([FromBody] List< BillItem_Request_Model> bi)
        {
            foreach( var b in bi)
            {
                var bill_to_add = new Billitem();
                bill_to_add.id = rng.Next(1, 1001);
                bill_to_add.created_time = DateTime.UtcNow;
                bill_to_add.updated_time = DateTime.UtcNow;
                bill_to_add.sales_service_item_id = b.sales_service_item_id;
                bill_to_add.name = b.name;
                bill_to_add.quantity = b.quantity;
                bill_to_add.uom = b.uom;
                bill_to_add.price = b.price;
                bill_to_add.subtotal = b.subtotal;
                bill_to_add.remark = b.remark;
                bill_to_add.created_user_id = 0;
                bill_to_add.updated_user_id = 0;
                bill_to_add.bill_id = b.bill_id;

                 dbContext.billitem.Add(bill_to_add);
                 dbContext.SaveChanges();
            }
            await dbContext.SaveChangesAsync();
            return Ok(bi);
        }

        [HttpPut]
        [Route("update_billitem/{id}")]
        public async Task<IActionResult> Update_bill([FromRoute] int id, [FromBody] BillItem_Request_Model b)
        {
            var bill_to_update = await dbContext.billitem.FirstOrDefaultAsync(b => b.id == id);
            if (bill_to_update == null)
            {
                return NotFound();
            }
            bill_to_update.updated_time = DateTime.UtcNow;
            bill_to_update.sales_service_item_id = b.sales_service_item_id;
            bill_to_update.name = b.name;
            bill_to_update.quantity = b.quantity;
            bill_to_update.uom = b.uom;
            bill_to_update.price = b.price;
            bill_to_update.subtotal = b.subtotal;
            bill_to_update.remark = b.remark;
            bill_to_update.bill_id = b.bill_id;


            dbContext.billitem.Update(bill_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(bill_to_update);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<BillItem_Bulk_Update_Model> bulk_data)
        {
            foreach (var bu in bulk_data)
            {
                var bill_to_update = await dbContext.billitem.FirstOrDefaultAsync(b => b.id == bu.id);
                if (bill_to_update == null)
                {
                    return NotFound();
                }
                bill_to_update.updated_time = DateTime.UtcNow;
                bill_to_update.sales_service_item_id = bu.sales_service_item_id;
                bill_to_update.name = bu.name;
                bill_to_update.quantity = bu.quantity;
                bill_to_update.uom = bu.uom;
                bill_to_update.price = bu.price;
                bill_to_update.subtotal = bu.subtotal;
                bill_to_update.remark = bu.remark;
                bill_to_update.bill_id = bu.bill_id;


                dbContext.billitem.Update(bill_to_update);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("delete_billitem/{id}")]
        public async Task<IActionResult> Delete_bill([FromRoute] int id)
        {
            var bill_to_delete = await dbContext.billitem.FirstOrDefaultAsync(b => b.id == id);
            if (bill_to_delete == null)
            {
                return NotFound();
            }
            dbContext.billitem.Remove(bill_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok(bill_to_delete);
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var bill_to_delete = await dbContext.billitem.FirstOrDefaultAsync(b => b.id == id);
                if (bill_to_delete == null)
                {
                    return NotFound();
                }
                dbContext.billitem.Remove(bill_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete complete");
        }
    }
}
