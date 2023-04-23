using HospitalDemo.Data;
using HospitalDemo.Models.Patient;
using HospitalDemo.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data;
using System;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using Npgsql;
using NpgsqlTypes;
using static HospitalDemo.Models.Patient.UpdatePatientRequest;
using HospitalDemo.Models.Bill;
using System.IdentityModel.Tokens.Jwt;

namespace HospitalDemo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : Controller
    {
       // string database_connection = "Server=localhost;Database=hospital_api;Username=postgres;Password=sawwinthant";
        Random rng = new Random();
        private readonly HospitalDbContext dbContext;
        public PatientController(HospitalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        //  [Authorize(Roles = "admin")]
        [Authorize]
        public async Task<IActionResult> Getpatients()
        {
            //// Get the token string from the request header or wherever it's stored
            //string tokenString = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            //// Parse the token string into a JwtSecurityToken object
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.ReadJwtToken(tokenString);

            //// Check if the token has expired
            //if (token.ValidTo < DateTime.UtcNow)
            //{
            //    // The token has expired
            //    return Unauthorized();
            //}
            return Ok(await dbContext.patient.ToListAsync());

        }

        //[HttpGet]
        //public IActionResult GetPatients()
        //{
        //    string connectionString = database_connection;
        //    string sql = "SELECT id, name, gender::text, date_of_birth, age, address, contact_details, created_time, updated_time, created_user_id, updated_user_id FROM public.patient;";
        //    List<Patient> myList = new List<Patient>(); // create an empty list
        //    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
        //        {
        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var patient=new Patient();
        //                    patient.id = reader.GetInt32(0);
        //                    patient.name = reader.GetString(1);
        //                    //patient.gender= (gender_enum)Enum.Parse(typeof(gender_enum), reader.GetString(2), ignoreCase: true);
        //                    patient.gender = reader.GetString(2) == "male" ? gender_enum.male : gender_enum.female;
        //                    patient.date_of_birth = reader.GetDateTime(3);
        //                    patient.age = reader.GetInt32(4);
        //                    patient.address = reader.GetString(5);
        //                    patient.contact_details = reader.GetString(6);
        //                    patient.created_time = reader.GetDateTime(7);
        //                    patient.updated_time = reader.GetDateTime(8);
        //                    patient.created_user_id = reader.GetInt32(9);
        //                    patient.updated_user_id = reader.GetInt32(10);
        //                    myList.Add(patient);
        //                }
        //            }
        //        }
        //    }

        //    return Ok(myList);

        //}


        [Authorize]
        [HttpGet]
        [Route("get_id/{id}")]
        public async Task<IActionResult> Getpatient([FromRoute] int id)
        {
            var patient = await dbContext.patient.FirstOrDefaultAsync(p => p.id == id);
            if (patient != null)
            {
                return Ok(patient);
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> Getpatientswithnames(string name)
        {
            var results = await dbContext.patient.Where(m => m.name.StartsWith(name)).ToListAsync();
            return Ok(results);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Addpatient(AddPatientRequest addPatient)
        {
            var patient = new Patient()
            {
                //id = Guid.NewGuid(),
                id = rng.Next(1, 1001),
                name = addPatient.name,
                address = addPatient.address,
                contact_details = addPatient.contact_details,
                age = addPatient.age,
                created_time = DateTime.Now,
                gender =addPatient.gender,
                created_user_id = 0,
                date_of_birth = addPatient.date_of_birth.AddDays(1),
                updated_time = DateTime.Now,
                updated_user_id = 0
            };

            patient.date_of_birth = patient.date_of_birth.ToUniversalTime();
            patient.created_time = patient.created_time.ToUniversalTime();
            patient.updated_time = patient.created_time.ToUniversalTime();
            await dbContext.AddAsync(patient);
            await dbContext.SaveChangesAsync();
            return Ok(patient);
        }
        //[HttpPost]
        //[Route("add_patient")]
        //public IActionResult add_patient([FromBody] AddPatientRequest addPatient)
        //{
        //    int id = rng.Next(1, 1001);
        //    DateTime create_time = DateTime.UtcNow;
        //    create_time = create_time.ToUniversalTime();
        //    DateTime updated_time = create_time;
        //    try
        //    {
        //        using var connection = new NpgsqlConnection(database_connection);
        //        connection.Open();
        //        using var command = new NpgsqlCommand(
        //            "INSERT INTO patient (id,name, gender, date_of_birth, age, address, contact_details, created_time, updated_time, created_user_id, updated_user_id)" +
        //            " VALUES (@id,@name, @gender, @date_of_birth, @age, @address, @contact_details, @created_time, @updated_time, @created_user_id, @updated_user_id)",
        //            connection
        //            );
        //        command.Parameters.AddWithValue("@id", id);
        //        command.Parameters.AddWithValue("@name", addPatient.name);
        //        command.Parameters.AddWithValue("@gender", NpgsqlDbType.Unknown, "gender_enum").Value = addPatient.gender.ToString();
        //        command.Parameters.AddWithValue("@date_of_birth", addPatient.date_of_birth.ToUniversalTime());
        //        command.Parameters.AddWithValue("@age", addPatient.age);
        //        command.Parameters.AddWithValue("@address", addPatient.address);
        //        command.Parameters.AddWithValue("@contact_details", addPatient.contact_details);
        //        command.Parameters.AddWithValue("@created_time", create_time);
        //        command.Parameters.AddWithValue("@updated_time", updated_time);
        //        command.Parameters.AddWithValue("@created_user_id", 1);
        //        command.Parameters.AddWithValue("@updated_user_id", 1);
        //        command.ExecuteNonQuery();

        //        return Ok(addPatient);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Deletepatient([FromRoute] int id)
        {
            var patient =  dbContext.patient.FirstOrDefault(p => p.id == id);
            if (patient != null)
            {
                dbContext.Remove(patient);
                await dbContext.SaveChangesAsync();
                return Ok(patient);
            }
            return NotFound();
        }

        [Authorize]
        [HttpDelete]
        //[Route("{id_list}")]
        public async Task<IActionResult> Deletepatient_bulk([FromBody] List<int> id_list)
        {
            foreach(var id in id_list)
            {
                //  var entityToDelete = await dbContext.patient.FindAsync(id);
                var entityToDelete = dbContext.patient.FirstOrDefault(p => p.id == id);
                if (entityToDelete == null)
                {
                    return NotFound();
                }

                dbContext.patient.Remove(entityToDelete);
            }
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute]int id, AddPatientRequest updatepatient)
        {
            //   var patient = await dbContext.patient.FindAsync(id);
            var patient = dbContext.patient.FirstOrDefault(p => p.id == id);
            if (patient == null)
            {
                return NotFound();
            }
            patient.name = updatepatient.name;
            patient.address = updatepatient.address;
            patient.contact_details = updatepatient.contact_details;
            patient.age = updatepatient.age;
            patient.gender = updatepatient.gender;
            patient.created_user_id = 0;
            patient.date_of_birth = updatepatient.date_of_birth.ToUniversalTime();
            patient.date_of_birth = patient.date_of_birth.AddDays(1);
            patient.updated_time = DateTime.Now.ToUniversalTime();
            patient.updated_user_id = 0;
            dbContext.Update(patient);
            await dbContext.SaveChangesAsync();
            return Ok(patient);
            
        }

        [Authorize]
        [HttpPut]
        [Route("bulk update")]
        public async Task<IActionResult> Update_bulk([FromBody] List<Patient_Bulk_Update_Model> bulk_data)
        {
            foreach (var b in bulk_data)
            {
                var patient = dbContext.patient.FirstOrDefault(p => p.id == b.id);
                if (patient == null)
                {
                    return NotFound();
                }
                patient.name = b.name;
                patient.address =b.address;
                patient.contact_details = b.contact_details;
                patient.age = b.age;
                patient.gender = b.gender;
                patient.created_user_id = 0;
                patient.date_of_birth = b.date_of_birth.ToUniversalTime();
                patient.date_of_birth = patient.date_of_birth.AddDays(1);
                patient.updated_time = DateTime.Now.ToUniversalTime();
                patient.updated_user_id = 0;
                dbContext.Update(patient);
                await dbContext.SaveChangesAsync();
            }
            return Ok("bulk_update complete");
        }




        //private UserLogin GetCurrentUser()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;

        //    if (identity != null)
        //    {


        //        return new UserLogin
        //        {
        //            username = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value,
        //            role = identity.FindFirst(ClaimTypes.Role)?.Value
        //        };
        //    }
        //    return null;
        //}




    }
}
