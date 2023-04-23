using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.SalesServiceItem;
using HospitalDemo.Models.UOM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesServiceItemController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public SalesServiceItemController(HospitalDbContext dbContext)
        {
                this.dbContext = dbContext; 
        }

        [HttpGet]
        [Route("get_all_ssItem")]
        public async Task<IActionResult> Get_all()
        {
            return Ok(await dbContext.salesserviceitem.ToListAsync());
        }

        [HttpGet]
        [Route("get_all_ssItem_with_uom_category")]
        public async Task<IActionResult> Get_all_item()
        {
            // return Ok(await dbContext.salesserviceitem.ToListAsync());
            List<SaleServiceItem_request_model2> saleitem = new List<SaleServiceItem_request_model2>();
            var sdata = await dbContext.salesserviceitem.ToListAsync();
            foreach (var data in sdata)
            {
                var uom = dbContext.uom.Find(data.uom_id);
                var category = dbContext.category.Find(data.category_id);
                if (uom == null || category == null)
                {
                    return NotFound();
                }

                SaleServiceItem_request_model2 s = new SaleServiceItem_request_model2();
                s.id = data.id;
                s.name = data.name;
                s.price = data.price;
                s.uom_id = data.id;
                s.created_time = data.created_time;
                s.uom = new UOM_Request_model();
                s.uom.name = uom.name;
                s.uom.description = uom.description;
                s.category = new Models.Category.Category_Request_model();
                s.category_id = data.category_id;
                s.category.name = category.name;
                s.category.description = category.description;
                
                saleitem.Add(s);
            }
            return Ok(saleitem);
        }

        [HttpGet]
        [Route("get_one_ssItem_with_uom_category/{id}")]
        public async Task<IActionResult> Get_one_item([FromRoute]int id)
        {
            // return Ok(await dbContext.salesserviceitem.ToListAsync());
            
               
                var data = await dbContext.salesserviceitem.FirstOrDefaultAsync(b => b.id == id);
            if(data == null)
            {
                return NotFound();
            }
                var uom = dbContext.uom.Find(data.uom_id);
                var category = dbContext.category.Find(data.category_id);
                if (uom == null || category == null)
                {
                    return NotFound();
                }

                SaleServiceItem_request_model2 s = new SaleServiceItem_request_model2();
                s.id = data.id;
                s.name = data.name;
                s.price = data.price;
                s.uom_id = data.id;
                s.created_time = data.created_time;
                s.uom = new UOM_Request_model();
                s.uom.name = uom.name;
                s.uom.description = uom.description;
                s.category = new Models.Category.Category_Request_model();
                s.category_id = data.category_id;
                s.category.name = category.name;
                s.category.description = category.description;

                
           
            return Ok(s);
        }

        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> Get_bill([FromRoute] int id)
        {
            var data = await dbContext.salesserviceitem.FirstOrDefaultAsync(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("add_ssItem")]
        public async Task<IActionResult> Add_ssItem([FromBody]Salesserviceitem_request_mdoel sitem)
        {
            var sitme_to_add = new Salesserviceitem();
            sitme_to_add.id = rng.Next(1, 1001);
            sitme_to_add.created_time = DateTime.UtcNow;
            sitme_to_add.updated_time=DateTime.UtcNow;
            sitme_to_add.name = sitem.name;
            sitme_to_add.price = sitem.price;
            sitme_to_add.uom_id = sitem.uom_id;
            sitme_to_add.category_id = sitem.category_id;
            sitme_to_add.created_user_id = 0;
            sitme_to_add.updated_user_id = 0;
            sitme_to_add.is_active = sitem.is_active;

            dbContext.Add(sitme_to_add);
            await dbContext.SaveChangesAsync();
            return Ok(sitme_to_add);
        }

        [HttpPut]
        [Route("update_ssItem/{id}")]
        public async Task<IActionResult> Update_ssItem([FromRoute]int id, [FromBody] Salesserviceitem_request_mdoel sitem)
        {
            var sitem_to_update = await dbContext.salesserviceitem.FirstOrDefaultAsync(s => s.id == id);
            if(sitem_to_update==null)
            {
                return NotFound();
            }
            
            sitem_to_update.updated_time = DateTime.UtcNow;
            sitem_to_update.name = sitem.name;
            sitem_to_update.price = sitem.price;
            sitem_to_update.uom_id = sitem.uom_id;
            sitem_to_update.category_id = sitem.category_id;
            sitem_to_update.is_active = sitem.is_active;

            dbContext.salesserviceitem.Update(sitem_to_update);
            await dbContext.SaveChangesAsync();
            return Ok(sitem_to_update);
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<SaleServiceItem_Bulk_Update_Model> bulk_data)
        {
            foreach (var b in bulk_data)
            {
                var sitem_to_update = await dbContext.salesserviceitem.FirstOrDefaultAsync(s => s.id == b.id);
                if (sitem_to_update == null)
                {
                    return NotFound();
                }

                sitem_to_update.updated_time = DateTime.UtcNow;
                sitem_to_update.name = b.name;
                sitem_to_update.price = b.price;
                sitem_to_update.uom_id = b.uom_id;
                sitem_to_update.category_id = b.category_id;
                sitem_to_update.is_active = b.is_active;

                dbContext.salesserviceitem.Update(sitem_to_update);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete_ssItem([FromRoute] int id)
        {
            var sitem_to_delete = await dbContext.salesserviceitem.FirstOrDefaultAsync(s => s.id == id);
            if(sitem_to_delete==null)
            {
                return NotFound();
            }
            dbContext.salesserviceitem.Remove(sitem_to_delete);
            await dbContext.SaveChangesAsync();
            return Ok("item deleted");
        }

        [HttpDelete]
        [Route("bulk_delete")]
        public async Task<IActionResult> Delete_bill([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var sitem_to_delete = await dbContext.salesserviceitem.FirstOrDefaultAsync(s => s.id == id);
                if (sitem_to_delete == null)
                {
                    return NotFound();
                }
                dbContext.salesserviceitem.Remove(sitem_to_delete);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk delete complete");
        }
    }
}
