using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.BillItem;
using HospitalDemo.Models.Patient;
using HospitalDemo.Models.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public BillController(HospitalDbContext dbContext)
        {
            this.dbContext=dbContext;
        }

        [Authorize]
        [HttpGet]
        [Route("get_all_bill")]
        public async Task<IActionResult> Get_bill()
        {
            return Ok(await dbContext.bill.ToListAsync());
        }

        [Authorize]
        [HttpGet]
        [Route("get_all_bill/Drafted")]
        public IActionResult Get_Drafted_bill()
        {
         var bill=dbContext.bill.Where(b=>b.is_cancelled==false && b.printed_or_drafted=="drafted").ToList();
         var draftedbill = new List<Bill_Request_Model2>();       
            foreach(var billitem in bill)
            {
                var dbill = new Bill_Request_Model2();
                dbill.id = billitem.id;
                dbill.created_time = billitem.created_time;
                dbill.updated_time = billitem.updated_time;
                dbill.patient_id = billitem.patient_id;
                var p = dbContext.patient.FirstOrDefault(i => i.id == dbill.patient_id);
                if(p==null)
                {
                    return NotFound();
                }
                dbill.patient = p;
                dbill.patient_name = p.name;
                dbill.patient_phone = p.contact_details;
                dbill.patient_address = p.address;
                dbill.total_amount = billitem.total_amount;
                var py = dbContext.payment.Where(p => p.bill_id == dbill.id).ToList();
                dbill.payment = py;
                var bitem = dbContext.billitem.Where(b => b.bill_id == dbill.id).ToList();
                dbill.billitems = bitem;
                dbill.created_user_id = billitem.created_user_id;
                dbill.updated_user_id = billitem.updated_user_id;
                dbill.printed_or_drafted = billitem.printed_or_drafted;
                dbill.is_cancelled = billitem.is_cancelled;
                draftedbill.Add(dbill);
            }
            return Ok(draftedbill);
        }

        [Authorize]
        [HttpGet]
        [Route("get_all_bill/Outstanding")]
        public async Task<IActionResult> Get_Outstanding_bill()
        {
            var bill = await dbContext.bill.Where(b => b.is_cancelled == false && b.printed_or_drafted == "printed").ToListAsync();
            var outstandingbill = new List<Bill_Request_Model2>();
            foreach (var billitem in bill)
            {
                var dbill = new Bill_Request_Model2();
                dbill.id = billitem.id;
                dbill.created_time = billitem.created_time;
                dbill.updated_time = billitem.updated_time;
                dbill.patient_id = billitem.patient_id;
                var p = dbContext.patient.FirstOrDefault(i => i.id == dbill.patient_id);
                if (p == null)
                {
                    return NotFound();
                }
                dbill.patient = p;
                dbill.patient_name = p.name;
                dbill.patient_phone = p.contact_details;
                dbill.patient_address = p.address;
                dbill.total_amount = billitem.total_amount;
                var py = dbContext.payment.Where(p => p.bill_id == dbill.id).ToList();
                dbill.payment = py;
                var bitem = dbContext.billitem.Where(b => b.bill_id == dbill.id).ToList();
                dbill.billitems = bitem;
                dbill.created_user_id = billitem.created_user_id;
                dbill.updated_user_id = billitem.updated_user_id;
                dbill.printed_or_drafted = billitem.printed_or_drafted;
                dbill.is_cancelled = billitem.is_cancelled;
                outstandingbill.Add(dbill);
            }
            return Ok(outstandingbill);
        }


        [Authorize]
        [HttpGet]
        [Route("get_all_bill/Completed")]
        public async Task<IActionResult> Get_Completed_bill()
        {
            var bill = await dbContext.bill.ToListAsync();
            var completedbill = new List<Bill_Request_Model2>();
            foreach (var billitem in bill)
            {
                var dbill = new Bill_Request_Model2();
                dbill.id = billitem.id;
                dbill.created_time = billitem.created_time;
                dbill.updated_time = billitem.updated_time;
                dbill.patient_id = billitem.patient_id;
                var p = dbContext.patient.FirstOrDefault(i => i.id == dbill.patient_id);
                if (p == null)
                {
                    return NotFound();
                }
                dbill.patient = p;
                dbill.patient_name = p.name;
                dbill.patient_phone = p.contact_details;
                dbill.patient_address = p.address;
                dbill.total_amount = billitem.total_amount;
                var py = dbContext.payment.Where(p => p.bill_id == dbill.id).ToList();
                dbill.payment = py;
                var bitem = dbContext.billitem.Where(b => b.bill_id == dbill.id).ToList();
                dbill.billitems = bitem;
                dbill.created_user_id = billitem.created_user_id;
                dbill.updated_user_id = billitem.updated_user_id;
                dbill.printed_or_drafted = billitem.printed_or_drafted;
                dbill.is_cancelled = billitem.is_cancelled;
                completedbill.Add(dbill);
            }
            return Ok(completedbill);
        }

        [Authorize]
        [HttpGet]
        [Route("get_all_bill/Cancelled")]
        public async Task<IActionResult> Get_Cancelled_bill()
        {
            var bill = await dbContext.bill.Where(b => b.is_cancelled == true).ToListAsync();
            var cancelledbill = new List<Bill_Request_Model2>();
            foreach (var billitem in bill)
            {
                var dbill = new Bill_Request_Model2();
                dbill.id = billitem.id;
                dbill.created_time = billitem.created_time;
                dbill.updated_time = billitem.updated_time;
                dbill.patient_id = billitem.patient_id;
                var p = dbContext.patient.FirstOrDefault(i => i.id == dbill.patient_id);
                if (p == null)
                {
                    return NotFound();
                }
                dbill.patient = p;
                dbill.patient_name = p.name;
                dbill.patient_phone = p.contact_details;
                dbill.patient_address = p.address;
                dbill.total_amount = billitem.total_amount;
                var py = dbContext.payment.Where(p => p.bill_id == dbill.id).ToList();
                dbill.payment = py;
                var bitem = dbContext.billitem.Where(b => b.bill_id == dbill.id).ToList();
                dbill.billitems = bitem;
                dbill.created_user_id = billitem.created_user_id;
                dbill.updated_user_id = billitem.updated_user_id;
                dbill.printed_or_drafted = billitem.printed_or_drafted;
                dbill.is_cancelled = billitem.is_cancelled;
                cancelledbill.Add(dbill);
            }
            return Ok(cancelledbill);
        }

        [Authorize]
        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute]int id)
        {
            var data = await dbContext.bill.FirstOrDefaultAsync(b => b.id == id);
            
            if(data == null)
            {
                return NotFound();
            }
            var bill = new Bill_Request_Model2();
            bill.id = data.id;
            bill.created_time = data.created_time;
            bill.updated_time = data.updated_time;
            bill.patient_id = data.patient_id;
            var p = dbContext.patient.FirstOrDefault(i => i.id == bill.patient_id);
            if (p == null)
            {
                return NotFound();
            }
            bill.patient = p;
            bill.patient_name = p.name;
            bill.patient_phone = p.contact_details;
            bill.patient_address = p.address;
            bill.total_amount = data.total_amount;
            var py = dbContext.payment.Where(p => p.bill_id == bill.id).ToList();
            bill.payment = py;
            var bitem = dbContext.billitem.Where(b => b.bill_id == bill.id).ToList();
            bill.billitems = bitem;
            bill.created_user_id = data.created_user_id;
            bill.updated_user_id = data.updated_user_id;
            bill.printed_or_drafted = data.printed_or_drafted;
            bill.is_cancelled = data.is_cancelled;


            return Ok(bill);
        }

        [Authorize]
        [HttpPost]
        [Route("Add_bill_")]
        public async Task<IActionResult> Add_bill([FromBody]Bill_Request_Model b)
        {
            try
            {
                var bill_to_add = new Bill();
                bill_to_add.id = rng.Next(1, 1001);
                bill_to_add.created_time = DateTime.UtcNow;
                bill_to_add.updated_time = DateTime.UtcNow;
                bill_to_add.patient_id = b.patient_id;
                bill_to_add.patient_name = b.patient_name;
                bill_to_add.patient_phone = b.patient_phone;
                bill_to_add.total_amount = b.total_amount;
                bill_to_add.created_user_id = 0;
                bill_to_add.updated_user_id = 0;
                bill_to_add.printed_or_drafted = b.printed_or_drafted;
                bill_to_add.patient_address = b.patient_address;
                bill_to_add.is_cancelled = b.is_cancelled;

                await dbContext.bill.AddAsync(bill_to_add);
                await dbContext.SaveChangesAsync();
                return Ok(bill_to_add.id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
        [HttpPost]
        [Route("Add_bill_with_billitem")]
        public async Task<IActionResult> Add_bill_with_billitem([FromBody]  Bill_Add_Model b)
        {
            //dynamic b = JsonConvert.DeserializeObject<dynamic>(bi.ToString());
            var bill_to_add = new Bill();
            bill_to_add.id = rng.Next(1, 1001);
            bill_to_add.created_time = DateTime.UtcNow;
            bill_to_add.updated_time = DateTime.UtcNow;
            bill_to_add.patient_id = b.patient_id;
            bill_to_add.patient_name = b.patient_name;
            bill_to_add.patient_phone = b.patient_phone;
            bill_to_add.total_amount = b.total_amount;
            bill_to_add.created_user_id = 0;
            bill_to_add.updated_user_id = 0;
            bill_to_add.printed_or_drafted = b.printed_or_drafted;
            bill_to_add.patient_address = b.patient_address;
            bill_to_add.is_cancelled = b.is_cancelled;
            dbContext.bill.Add(bill_to_add);
            dbContext.SaveChanges();
            Billitem bi = new Billitem();
            foreach (var bitem in b.bill_items)
            {

                bi.id = rng.Next(1, 1001);
                bi.bill_id = bill_to_add.id;
                bi.created_time= DateTime.UtcNow;
                bi.updated_time= DateTime.UtcNow;
                bi.sales_service_item_id = bitem.sales_service_item_id;
                bi.name = bitem.name;
                bi.quantity = bitem.quantity;
                bi.uom = bitem.uom;
                bi.price = bitem.price;
                bi.subtotal = bitem.subtotal;
                bi.remark = bitem.remark;
                dbContext.billitem.Add(bi);
                dbContext.SaveChanges();
             }
            await dbContext.SaveChangesAsync();

            return Ok(bill_to_add);
        }

        [Authorize]
        [HttpPut]
        [Route("update_bill/{id}")]
        public async Task<IActionResult> Update_bill([FromRoute]int id, [FromBody] Bill_Request_Model b)
        {
            var bill_to_update = await dbContext.bill.FirstOrDefaultAsync(b => b.id == id);
            if( bill_to_update == null)
            {
                return NotFound();
            }
            
            
            bill_to_update.updated_time = DateTime.UtcNow;
            bill_to_update.patient_id = b.patient_id;
            bill_to_update.patient_name = b.patient_name;
            bill_to_update.patient_phone = b.patient_phone;
            bill_to_update.total_amount = b.total_amount;
           
            bill_to_update.printed_or_drafted = b.printed_or_drafted;
            bill_to_update.patient_address = b.patient_address;
            bill_to_update.is_cancelled = b.is_cancelled;

            dbContext.bill.Update(bill_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(bill_to_update);
        }

        [Authorize]
        [HttpPut]
        [Route("update_bill/print/{id}")]
        public async Task<IActionResult> Update_bill_print([FromRoute] int id)
        {
            var bill_to_update = await dbContext.bill.FirstOrDefaultAsync(b => b.id == id);
            if (bill_to_update == null)
            {
                return NotFound();
            }
            bill_to_update.printed_or_drafted = "printed";
            dbContext.bill.Update(bill_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(bill_to_update);
        }

        [Authorize]
        [HttpPut]
        [Route("update_bill/cancel/{id}")]
        public async Task<IActionResult> Update_bill_cancel([FromRoute] int id)
        {
            var bill_to_update = await dbContext.bill.FirstOrDefaultAsync(b => b.id == id);
            if (bill_to_update == null)
            {
                return NotFound();
            }
            bill_to_update.is_cancelled = true;
            dbContext.bill.Update(bill_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(bill_to_update);
        }

        [Authorize]
        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<Bill_Bulk_Update_Model> bulk_data)
        {
            foreach(var b in bulk_data)
            {
                var bill_to_update = await dbContext.bill.FirstOrDefaultAsync(b => b.id == b.id);
                if (bill_to_update == null)
                {
                    return NotFound();
                }


                bill_to_update.updated_time = DateTime.UtcNow;
                bill_to_update.patient_id = b.patient_id;
                bill_to_update.patient_name = b.patient_name;
                bill_to_update.patient_phone = b.patient_phone;
                bill_to_update.total_amount = b.total_amount;

                bill_to_update.printed_or_drafted = b.printed_or_drafted;
                bill_to_update.patient_address = b.patient_address;
                bill_to_update.is_cancelled = b.is_cancelled;

                dbContext.bill.Update(bill_to_update);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [Authorize]
        [HttpDelete]
        [Route("delete_bill/{id}")]
        public async Task<IActionResult> Delete_bill([FromRoute] int id)
        {
            var bill_to_delete = await dbContext.bill.FirstOrDefaultAsync(b => b.id == id);
            if(bill_to_delete == null)
            {
                return NotFound();
            }
            dbContext.bill.Remove(bill_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok(bill_to_delete);
        }

        [Authorize]
        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody]List<int> id_list)
        {

            foreach(var id in id_list)
            {
                var bill_to_delete = await dbContext.bill.FirstOrDefaultAsync(b => b.id == id);
                if (bill_to_delete == null)
                {
                    return NotFound();
                }
                dbContext.bill.Remove(bill_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_delete_complete");
        }

        //[HttpDelete]
        //[Route("bulk_delete")]
        //public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        //{
        //    foreach (var id in id_list)
        //    {

        //    }
        //}

       
    }
}
