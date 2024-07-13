using Microsoft.AspNetCore.Mvc;
using SQL_EFCore_.NET_8_Web_API_CRUD.Model;

namespace SQL_EFCore_.NET_8_Web_API_CRUD.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDBContext _dBContext;

        public UsersController(ApplicationDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(User user)
        {
            try
            {
                await _dBContext.Users.AddAsync(user);
                await _dBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok("User Created successfully!!");
        }

        //Read
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsersAsync()
        {
            try
            {
                return Ok(await _dBContext.Users.ToListAsync());

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        //Update
        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync(string email, User user)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Please enter correct email");
            }
            var existingUser = await _dBContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (existingUser == null)
            {
                return NotFound();
            }
            try
            {
                existingUser.Salary = user.Salary;
                existingUser.ContactNumber = user.ContactNumber;

                _dBContext.Users.Update(existingUser);
                await _dBContext.SaveChangesAsync();

                return Ok("User updated successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        //Delete
        [HttpDelete]
        public async Task<ActionResult> DeleteUserByEmailAsync(string email)
        {
            try
            {
                var userToDelete = await _dBContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (userToDelete == null)
                {
                    return NotFound();
                }

                _dBContext.Users.Remove(userToDelete);
                await _dBContext.SaveChangesAsync();

                return Ok("User deleted successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
