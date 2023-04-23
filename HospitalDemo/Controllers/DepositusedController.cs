using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.Deposit;
using HospitalDemo.Models.DepositUsed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositusedController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public DepositusedController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        [Route("get_deposit")]
        public async Task<IActionResult> Get_deposit()
        {
            return Ok(await dbContext.depositused.ToListAsync());
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute] int id)
        {
            var data = await dbContext.depositused.FirstOrDefaultAsync(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("add_deposit")]
        public async Task<IActionResult> Add_deposit([FromBody] Depositused_Request_Model d)
        {
            var d_to_add = new Depositused();
            d_to_add.id = rng.Next(1, 1001);
            d_to_add.created_time = DateTime.UtcNow;
            d_to_add.updated_time = DateTime.UtcNow;
            d_to_add.deposit_id = d.deposit_id;
            d_to_add.payment_id = d.payment_id;
            d_to_add.unpaid_amount = d.unpaid_amount;
            d_to_add.deposit_amount = d.deposit_amount;
            d_to_add.created_user_id = 0;
            d_to_add.updated_user_id = 0;
            

            dbContext.depositused.Add(d_to_add);
            await dbContext.SaveChangesAsync();
            return Ok(d_to_add);
        }

        [HttpPut]
        [Route("update_deposit/{id}")]
        public async Task<IActionResult> Update_deposit([FromRoute] int id, [FromBody] Depositused_Request_Model d)
        {
            var d_to_update = await dbContext.depositused.FirstOrDefaultAsync(d => d.id == id);
            if (d_to_update == null)
            {
                return NotFound();
            }

            d_to_update.updated_time = DateTime.UtcNow;
            d_to_update.deposit_id = d.deposit_id;
            d_to_update.payment_id = d.payment_id;
            d_to_update.unpaid_amount = d.unpaid_amount;
            d_to_update.deposit_amount = d.deposit_amount;

            dbContext.depositused.Update(d_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(d_to_update);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<Depositused_Bulk_Update_Model> bulk_data)
        {
            foreach (var b in bulk_data)
            {
                var d_to_update = await dbContext.depositused.FirstOrDefaultAsync(d => d.id == b.id);
                if (d_to_update == null)
                {
                    return NotFound();
                }

                d_to_update.updated_time = DateTime.UtcNow;
                d_to_update.deposit_id = b.deposit_id;
                d_to_update.payment_id = b.payment_id;
                d_to_update.unpaid_amount = b.unpaid_amount;
                d_to_update.deposit_amount = b.deposit_amount;

                dbContext.depositused.Update(d_to_update);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("delete_deposit/{id}")]
        public async Task<IActionResult> Delete_deposit([FromRoute] int id)
        {
            var d_to_delete = await dbContext.depositused.FirstOrDefaultAsync(d => d.id == id);
            if (d_to_delete == null)
            {
                return NotFound();
            }
            dbContext.depositused.Remove(d_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok(d_to_delete);
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var d_to_delete = await dbContext.depositused.FirstOrDefaultAsync(d => d.id == id);
                if (d_to_delete == null)
                {
                    return NotFound();
                }
                dbContext.depositused.Remove(d_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete complete");
        }
    }
}
