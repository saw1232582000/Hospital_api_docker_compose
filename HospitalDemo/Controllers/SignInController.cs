using HospitalDemo.Data;
using HospitalDemo.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignInController : Controller
    {
        private readonly HospitalDbContext dbContext;
        Random rng = new Random();
        public SignInController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("SingUp")]
        public async Task<IActionResult> SignUp([FromBody]UserLogin_Request_Model newuser)
        {
            var user = new UserLogin()
            {
                id = rng.Next(1, 2001),
                username=newuser.username,
                password=newuser.password,
                role=newuser.role
            };
            dbContext.User.Add(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}
