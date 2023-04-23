using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.Deposit;
using HospitalDemo.Models.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public DepositController(HospitalDbContext dbContext)
        {
                this.dbContext = dbContext; 
        }

        [HttpGet]
        [Route("get_deposit")]
        public async Task<IActionResult> Get_deposit()
        {
            return Ok(await dbContext.deposit.ToListAsync());
        }

        [HttpGet]
        [Route("get_deposit/active/{id}")]
        public  IActionResult Get_deposit_by_patientId([FromRoute]int id)
        {
            return Ok( dbContext.deposit.Where(d => d.patient_id == id).ToList());
        }

        [HttpGet]
        [Route("get_deposit_with_patient")]
        public async Task<IActionResult> Get_deposit_with_patient()
        {
           var alldata= await dbContext.deposit.ToListAsync();
            Depoist_Request_Model2 d = new Depoist_Request_Model2();
            List<Depoist_Request_Model2> data_to_return = new List<Depoist_Request_Model2>();
            foreach(var depodata in alldata)
            {
                var get_patient=await dbContext.patient.FirstOrDefaultAsync(p=>p.id==depodata.patient_id);
                if (get_patient == null)
                {
                    return NotFound();
                }
                d.id = depodata.id;
                d.patient_id = depodata.patient_id;
                d.patient = get_patient;
                d.amount = depodata.amount;
                d.remark = depodata.remark;
                d.is_cancelled = depodata.is_cancelled;
                data_to_return.Add(d);
            }
            return Ok(data_to_return);
        }

        [HttpGet]
        [Route("get_with_patient_by_id/{id}")]
        public async Task<IActionResult> Get_bill_with_patient_by_id([FromRoute] int id)
        {
            var depodata = await dbContext.deposit.FirstOrDefaultAsync(d=>d.id==id);
            if(depodata == null)
            {
                return NotFound();
            }
            Depoist_Request_Model2 d = new Depoist_Request_Model2();
           
                var get_patient = await dbContext.patient.FirstOrDefaultAsync(p => p.id == depodata.patient_id);
                if (get_patient == null)
                {
                    return NotFound();
                }
                d.id = depodata.id;
                d.patient_id = depodata.patient_id;
                d.patient = get_patient;
                d.amount = depodata.amount;
                d.remark = depodata.remark;
                d.is_cancelled = depodata.is_cancelled;
                
           
            return Ok(d);
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute] int id)
        {
            var data = await dbContext.deposit.FirstOrDefaultAsync(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("get_by_patient_id/{id}")]
        public async Task<IActionResult> Get_bill_by_patient_id([FromRoute] int id)
        {
            var data = await dbContext.deposit.Where(b => b.patient_id == id).ToListAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("add_deposit")]
        public async Task<IActionResult> Add_deposit([FromBody]Depoist_Request_Model d)
        {
            var d_to_add=new Deposit();
            d_to_add.id = rng.Next(1, 1001);
            d_to_add.created_time = DateTime.UtcNow;
            d_to_add.updated_time = DateTime.UtcNow;
            d_to_add.patient_id = d.patient_id;
            d_to_add.amount = d.amount;
            d_to_add.created_user_id = 0;
            d_to_add.updated_user_id = 0;
            d_to_add.remark = d.remark;
            d_to_add.is_cancelled = d.is_cancelled;

            dbContext.deposit.Add(d_to_add);
            await dbContext.SaveChangesAsync();
            return Ok(d_to_add);
        }

        [HttpPut]
        [Route("update_deposit/{id}")]
        public async Task<IActionResult> Update_deposit([FromRoute]int id, [FromBody] Depoist_Request_Model d)
        {
            var d_to_update = await dbContext.deposit.FirstOrDefaultAsync(d => d.id == id);
            if(d_to_update == null)
            {
                return NotFound();
            }
            
            d_to_update.updated_time = DateTime.UtcNow;
            d_to_update.patient_id = d.patient_id;
            d_to_update.amount = d.amount;
            d_to_update.remark = d.remark;
            d_to_update.is_cancelled = d.is_cancelled;

            dbContext.deposit.Update(d_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(d_to_update);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<Deposit_Bulk_Update_Model> bulk_data)
        {
            foreach (var bu in bulk_data)
            {
                var d_to_update = await dbContext.deposit.FirstOrDefaultAsync(d => d.id == bu.id);
                if (d_to_update == null)
                {
                    return NotFound();
                }

                d_to_update.updated_time = DateTime.UtcNow;
                d_to_update.patient_id = bu.patient_id;
                d_to_update.amount = bu.amount;
                d_to_update.remark = bu.remark;
                d_to_update.is_cancelled = bu.is_cancelled;

                dbContext.deposit.Update(d_to_update);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("delete_deposit/{id}")]
        public async Task<IActionResult> Delete_deposit([FromRoute] int id)
        {
            var d_to_delete = await dbContext.deposit.FirstOrDefaultAsync(d => d.id == id);
            if(d_to_delete == null)
            {
                return NotFound();
            }
            dbContext.deposit.Remove(d_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok(d_to_delete);
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var d_to_delete = await dbContext.deposit.FirstOrDefaultAsync(d => d.id == id);
                if (d_to_delete == null)
                {
                    return NotFound();
                }
                dbContext.deposit.Remove(d_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete complete");
        }
    }
}
