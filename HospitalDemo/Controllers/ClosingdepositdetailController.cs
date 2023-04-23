using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.ClosingBillDetail;
using HospitalDemo.Models.ClosingDepositdetail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClosingdepositdetailController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public ClosingdepositdetailController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get_all()
        {
            return Ok(await dbContext.closingdepositdetail.ToListAsync());
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute] int id)
        {
            var data = await dbContext.closingdepositdetail.FirstOrDefaultAsync(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add_dailyclosing([FromBody] Closingdepositdetail_Request_Model dc)
        {
            var cd_to_add = new Closingdepositdetail();

            cd_to_add.id = rng.Next(1, 1001);
            cd_to_add.created_time = DateTime.UtcNow;
            cd_to_add.updated_time = DateTime.UtcNow;
            cd_to_add.daily_closing_id = dc.daily_closing_id;
            cd_to_add.deposit_id = dc.deposit_id;
            cd_to_add.amount = dc.amount;
            cd_to_add.created_user_id = 0;
            cd_to_add.updated_user_id = 0;

            await dbContext.closingdepositdetail.AddAsync(cd_to_add);
            await dbContext.SaveChangesAsync();
            return Ok(cd_to_add);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update_dailyclosing([FromRoute] int id, [FromBody] Closingdepositdetail_Request_Model dc)
        {
            var dc_to_udpate = await dbContext.closingdepositdetail.FirstOrDefaultAsync(d => d.id == id);
            if (dc_to_udpate == null)
            {
                return NotFound();
            }


            dc_to_udpate.updated_time = DateTime.UtcNow;
            dc_to_udpate.daily_closing_id = dc.daily_closing_id;
            dc_to_udpate.deposit_id = dc.deposit_id;
            dc_to_udpate.amount = dc.amount;

            dbContext.closingdepositdetail.Update(dc_to_udpate);
            await dbContext.SaveChangesAsync();

            return Ok(dc_to_udpate);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<Closingdepositdetail_Bulk_Update_Model> bulk_data)
        {
            foreach (var bu in bulk_data)
            {
                var dc_to_udpate = await dbContext.closingdepositdetail.FirstOrDefaultAsync(d => d.id == bu.id);
                if (dc_to_udpate == null)
                {
                    return NotFound();
                }


                dc_to_udpate.updated_time = DateTime.UtcNow;
                dc_to_udpate.daily_closing_id = bu.daily_closing_id;
                dc_to_udpate.deposit_id = bu.deposit_id;
                dc_to_udpate.amount = bu.amount;

                dbContext.closingdepositdetail.Update(dc_to_udpate);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete_dailyclosing([FromRoute] int id)
        {
            var dc_to_delete = await dbContext.closingdepositdetail.FirstOrDefaultAsync(d => d.id == id);
            if (dc_to_delete == null)
            {
                return NotFound();
            }
            dbContext.closingdepositdetail.Remove(dc_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok(dc_to_delete);
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var dc_to_delete = await dbContext.closingdepositdetail.FirstOrDefaultAsync(d => d.id == id);
                if (dc_to_delete == null)
                {
                    return NotFound();
                }
                dbContext.closingdepositdetail.Remove(dc_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete complete");
        }
    }
}
