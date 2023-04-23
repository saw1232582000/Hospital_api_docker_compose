using HospitalDemo.Data;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public CategoryController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("all_category")]
        public IActionResult Get_category()
        {
            return Ok(dbContext.category.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get_category_by_id([FromRoute] int id)
        {
            var category = dbContext.category.Find(id);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("add_category")]
        public IActionResult Post_category([FromBody] Category_Request_model category_data)
        {
            var category = new Category();
            
            category.id = rng.Next(1, 1001);
            category.created_time = DateTime.Now;
            category.updated_time = DateTime.Now;
            category.name=category_data.name;
            category.description = category_data.description;
            category.created_user_id = 1;
            category.updated_user_id = 1;
            dbContext.category.Add(category);
            dbContext.SaveChanges();
            return Ok(category);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put_category([FromRoute] int id, [FromBody] Category_Request_model updated_category_data)
        {
            var category = await dbContext.category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            category.name = updated_category_data.name;
            category.description = updated_category_data.description;
            category.updated_time = DateTime.UtcNow;
            dbContext.Update(category);
            await dbContext.SaveChangesAsync();
            return Ok(category);
            
        }

        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<Category_Bulk_Update_Model> bulk_data)
        {
            foreach (var b in bulk_data)
            {
                var category = await dbContext.category.FindAsync(b.id);
                if (category == null)
                {
                    return NotFound();
                }
                category.name = b.name;
                category.description = b.description;
                category.updated_time = DateTime.UtcNow;
                dbContext.Update(category);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete_category([FromRoute] int id)
        {
            var category = await dbContext.category.FindAsync(id);
            if (category != null)
            {
                dbContext.category.Remove(category);
                await dbContext.SaveChangesAsync();
                return Ok("category deleted");
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("bulk")]
        public async Task<IActionResult> Delete_category_bulk([FromBody] List<int> id_list)
        {
            foreach (var id in id_list)
            {
                var category = await dbContext.category.FindAsync(id);
                if (category == null)
                {

                    return NotFound();
                }
                dbContext.category.Remove(category);
            }
            await dbContext.SaveChangesAsync();
            return Ok("category deleted");
        }


    }
}
