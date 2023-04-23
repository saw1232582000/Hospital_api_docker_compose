using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.Transactiontype;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactiontypeController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public TransactiontypeController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Get_allItem()
        {
            return Ok(await dbContext.transactiontype.ToListAsync());
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute] int id)
        {
            var data = await dbContext.transactiontype.FirstOrDefaultAsync(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("add_transaction")]
        public async Task<IActionResult> Add_transaction([FromBody]TransactiontypeRequestModel trans)
        {
            var trans_to_add = new Transactiontype()
            {
                id = rng.Next(1, 1001),
                created_time = DateTime.UtcNow,
                updated_time = DateTime.UtcNow,
                name = trans.name,
                type = trans.type,
                created_user_id = 0,
                updated_user_id = 0
            };
            await dbContext.transactiontype.AddAsync(trans_to_add);
            await dbContext.SaveChangesAsync();
            return Ok(trans_to_add);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update_transaction([FromRoute]int id, [FromBody] TransactiontypeRequestModel trans)
        {
            var transaction_to_update = await dbContext.transactiontype.FirstOrDefaultAsync(t => t.id == id);
            if(transaction_to_update==null)
            {
                return NotFound();
            }
            transaction_to_update.updated_time = DateTime.UtcNow;
            transaction_to_update.name = trans.name;
            transaction_to_update.type = trans.type;
            dbContext.transactiontype.Update(transaction_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(transaction_to_update);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<Transactiontype_Bulk_Update_Model> bulk_data)
        {
            foreach (var b in bulk_data)
            {
                var transaction_to_update = await dbContext.transactiontype.FirstOrDefaultAsync(t => t.id == b.id);
                if (transaction_to_update == null)
                {
                    return NotFound();
                }
                transaction_to_update.updated_time = DateTime.UtcNow;
                transaction_to_update.name = b.name;
                transaction_to_update.type = b.type;
                dbContext.transactiontype.Update(transaction_to_update);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> delete_transaction([FromRoute] int id)
        {
            var transaction_to_delete = await dbContext.transactiontype.FirstOrDefaultAsync(t => t.id == id);
            if( transaction_to_delete==null)
            {
                return NotFound();

            }
            dbContext.transactiontype.Remove(transaction_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok("transaction deleted");
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var transaction_to_delete = await dbContext.transactiontype.FirstOrDefaultAsync(t => t.id == id);
                if (transaction_to_delete == null)
                {
                    return NotFound();

                }
                dbContext.transactiontype.Remove(transaction_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete completed");
        }
    }
}
