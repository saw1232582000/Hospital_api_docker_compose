using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.ClosingDepositdetail;
using HospitalDemo.Models.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public PaymentController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get_all()
        {
            return Ok(await dbContext.payment.ToListAsync());
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute] int id)
        {
            var data = await dbContext.payment.FirstOrDefaultAsync(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add_dailyclosing([FromBody] Payment_Request_Model p)
        {
            var p_to_add = new Payment();

            p_to_add.id = rng.Next(1, 1001);
            p_to_add.created_time = DateTime.UtcNow;
            p_to_add.updated_time = DateTime.UtcNow;
            p_to_add.bill_id = p.bill_id;
            p_to_add.total_amount = p.total_amount;
            p_to_add.total_deposit_amount = p.total_deposit_amount;
            p_to_add.collected_amount = p.collected_amount;
            p_to_add.unpaid_amount = p.unpaid_amount;
            p_to_add.is_outstanding = p.is_outstanding;
            p_to_add.created_user_id = 0;
            p_to_add.updated_user_id = 0;

            await dbContext.payment.AddAsync(p_to_add);
            await dbContext.SaveChangesAsync();
            return Ok(p_to_add);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update_dailyclosing([FromRoute] int id, [FromBody] Payment_Request_Model p)
        {
            var p_to_udpate = await dbContext.payment.FirstOrDefaultAsync(d => d.id == id);
            if (p_to_udpate == null)
            {
                return NotFound();
            }


            p_to_udpate.updated_time = DateTime.UtcNow;
            p_to_udpate.bill_id = p.bill_id;
            p_to_udpate.total_amount = p.total_amount;
            p_to_udpate.total_deposit_amount = p.total_deposit_amount;
            p_to_udpate.collected_amount = p.collected_amount;
            p_to_udpate.unpaid_amount = p.unpaid_amount;
            p_to_udpate.is_outstanding = p.is_outstanding;

            dbContext.payment.Update(p_to_udpate);
            await dbContext.SaveChangesAsync();

            return Ok(p_to_udpate);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<Payment_Bulk_Update_Model> bulk_data)
        {
            foreach (var b in bulk_data)
            {
                var p_to_udpate = await dbContext.payment.FirstOrDefaultAsync(d => d.id == b.id);
                if (p_to_udpate == null)
                {
                    return NotFound();
                }


                p_to_udpate.updated_time = DateTime.UtcNow;
                p_to_udpate.bill_id = b.bill_id;
                p_to_udpate.total_amount = b.total_amount;
                p_to_udpate.total_deposit_amount = b.total_deposit_amount;
                p_to_udpate.collected_amount = b.collected_amount;
                p_to_udpate.unpaid_amount = b.unpaid_amount;
                p_to_udpate.is_outstanding = b.is_outstanding;

                dbContext.payment.Update(p_to_udpate);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete_dailyclosing([FromRoute] int id)
        {
            var p_to_delete = await dbContext.payment.FirstOrDefaultAsync(d => d.id == id);
            if (p_to_delete == null)
            {
                return NotFound();
            }
            dbContext.payment.Remove(p_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok(p_to_delete);
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var p_to_delete = await dbContext.payment.FirstOrDefaultAsync(d => d.id == id);
                if (p_to_delete == null)
                {
                    return NotFound();
                }
                dbContext.payment.Remove(p_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete complete");
        }
    }
}
