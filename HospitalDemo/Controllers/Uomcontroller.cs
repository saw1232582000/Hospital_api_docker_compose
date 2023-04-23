using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.UOM;
using Microsoft.AspNetCore.Mvc;
using NodaTime;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Uomcontroller : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public Uomcontroller(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("all_Uom")]
        public IActionResult Get_uom()
        {
            return Ok( dbContext.uom.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get_uom_by_id([FromRoute]int id)
        {
            var uom = dbContext.uom.Find(id);
            if(uom!=null)
            {
                return Ok(uom);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("add_UOM")]
        public IActionResult Post_uom([FromBody]UOM_Request_model uom_data)
        {
            //var uom = new UOM();
            var uom = new UOM();
            uom.id = rng.Next(1, 1001);
            uom.created_time=DateTime.Now;
            uom.updated_time=DateTime.Now;
            uom.created_user_id = 1;
            uom.updated_user_id = 1;
            uom.name = uom_data.name;
            uom.description = uom_data.description;
            dbContext.uom.Add(uom);
            dbContext.SaveChanges();
            return Ok(uom);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put_uom([FromRoute]int id, [FromBody]UOM_Request_model updated_uom_data)
        {
            var uom = await dbContext.uom.FindAsync(id);
            if(uom == null)
            {
                return NotFound();
            }
            uom.name = updated_uom_data.name;
            uom.description = updated_uom_data.description;
            uom.updated_time = DateTime.UtcNow;
            dbContext.Update(uom);
            await dbContext.SaveChangesAsync();
            return Ok(uom);
            
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<UOM_Bulk_Update_Model> bulk_data)
        {
            foreach (var b in bulk_data)
            {
                var uom = await dbContext.uom.FindAsync(b.id);
                if (uom == null)
                {
                    return NotFound();
                }
                uom.name = b.name;
                uom.description = b.description;
                uom.updated_time = DateTime.UtcNow;
                dbContext.Update(uom);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete_uom([FromRoute] int id)
        {
            var uom=await dbContext.uom.FindAsync(id);
            if(uom !=null)
            {
                dbContext.uom.Remove(uom);
                await dbContext.SaveChangesAsync();
                return Ok("uom deleted");
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("bulk")]
        public async Task<IActionResult> Delete_uom_bulk([FromBody] List<int> id_list)
        {
            foreach( var id in id_list)
            {
                var uom = await dbContext.uom.FindAsync(id);
                if (uom == null)
                {
                    
                    return NotFound();
                }
                dbContext.uom.Remove(uom);
            }
            await dbContext.SaveChangesAsync();
            return Ok("uom deleted");
        }
    }
}
