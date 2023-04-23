using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.DailyClosing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyclosingController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public DailyclosingController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;       
        }

        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get_all()
        {
            return Ok(await dbContext.dailyclosing.ToListAsync());
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute] int id)
        {
            var data = await dbContext.dailyclosing.FirstOrDefaultAsync(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("Add_dailclosing")]
        public async Task<IActionResult> Add_dailyclosing([FromBody]DailyClosing_Request_Model dc)
        {
            var dc_to_add = new Dailyclosing();

            dc_to_add.id = rng.Next(1, 1001);
            dc_to_add.created_time = DateTime.UtcNow;
            dc_to_add.updated_time = DateTime.UtcNow;
            dc_to_add.opening_balance = dc.opening_balance;
            dc_to_add.deposit_total = dc.deposit_total;
            dc_to_add.bill_total = dc.bill_total;
            dc_to_add.grand_total = dc.grand_total;
            dc_to_add.actual_amount = dc.actual_amount;
            dc_to_add.adjusted_amount = dc.adjusted_amount;
            dc_to_add.adjusted_reason = dc.adjusted_reason;
            dc_to_add.created_user_id = 0;
            dc_to_add.updated_user_id = 0;

            await dbContext.dailyclosing.AddAsync(dc_to_add);
            await dbContext.SaveChangesAsync();
            return Ok(dc_to_add);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update_dailyclosing([FromRoute]int id, [FromBody] DailyClosing_Request_Model dc)
        {
            var dc_to_udpate = await dbContext.dailyclosing.FirstOrDefaultAsync(d => d.id == id);
            if(dc_to_udpate==null)
            {
                return NotFound();
            }
            
           
            dc_to_udpate.updated_time = DateTime.UtcNow;
            dc_to_udpate.opening_balance = dc.opening_balance;
            dc_to_udpate.deposit_total = dc.deposit_total;
            dc_to_udpate.bill_total = dc.bill_total;
            dc_to_udpate.grand_total = dc.grand_total;
            dc_to_udpate.actual_amount = dc.actual_amount;
            dc_to_udpate.adjusted_amount = dc.adjusted_amount;
            dc_to_udpate.adjusted_reason = dc.adjusted_reason;

            dbContext.dailyclosing.Update(dc_to_udpate);
            await dbContext.SaveChangesAsync();

            return Ok(dc_to_udpate);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<DailyClosing_Bulk_Update_Model> bulk_data)
        {
            foreach (var bu in bulk_data)
            {
                var dc_to_udpate = await dbContext.dailyclosing.FirstOrDefaultAsync(d => d.id == bu.id);
                if (dc_to_udpate == null)
                {
                    return NotFound();
                }


                dc_to_udpate.updated_time = DateTime.UtcNow;
                dc_to_udpate.opening_balance = bu.opening_balance;
                dc_to_udpate.deposit_total = bu.deposit_total;
                dc_to_udpate.bill_total = bu.bill_total;
                dc_to_udpate.grand_total = bu.grand_total;
                dc_to_udpate.actual_amount = bu.actual_amount;
                dc_to_udpate.adjusted_amount = bu.adjusted_amount;
                dc_to_udpate.adjusted_reason = bu.adjusted_reason;

                dbContext.dailyclosing.Update(dc_to_udpate);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete_dailyclosing([FromRoute] int id)
        {
            var dc_to_delete= await dbContext.dailyclosing.FirstOrDefaultAsync(d => d.id == id);
            if(dc_to_delete==null)
            {
                return NotFound();
            }
            dbContext.dailyclosing.Remove(dc_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok(dc_to_delete);
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var dc_to_delete = await dbContext.dailyclosing.FirstOrDefaultAsync(d => d.id == id);
                if (dc_to_delete == null)
                {
                    return NotFound();
                }
                dbContext.dailyclosing.Remove(dc_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete complete");
        }

    }
}
