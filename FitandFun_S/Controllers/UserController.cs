using Microsoft.AspNetCore.Mvc;
using FitandFun.Models;
using FitandFun.Services;

namespace FitandFun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("VratiSveKorisnike")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

       [HttpGet("VratiKorisnikeSaTreningom")]
        public async Task<IActionResult> GetUsersWithWorkouts()
        {
            try
            {
                var workouts = await _userService.GetUsersWithWorkouts();
                return Ok(workouts);
            }
            catch (Exception ex)
            {
                // Ovdje možete obraditi grešku prema potrebi
                return StatusCode(500, $"Došlo je do greške: {ex.Message}");
            }
        }

        [HttpGet("GetIdByUsername/{username}")]
        public async Task<IActionResult> GetIdByUsername(string username)
        {
            try
            {
                var user = await _userService.GetIdByUsername(username);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom dobijanja ID-ja korisnika po korisničkom imenu: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("VratiKorisnikaPoIdu/{id}")]
        public async Task<IActionResult> GetUserByID(string id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("NapraviKorisnika")]
        public async Task<IActionResult> CreateUser(User user)
        {
           await _userService.CreateAsync(user);
            return Ok("user created successfully");
        }

        [HttpPut("AzurirajKorisnika/{id}")]
        public async Task<IActionResult> UpdateUserById(string id, [FromBody] User newUser)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound();
            await _userService.UpdateAsync(id, newUser);
            return Ok("updated successfully");
        }

        [HttpDelete("ObrisiKorisnika/{id}")]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound();
            await _userService.DeleteAsync(id);
            return Ok("deleted successfully");
        }
        [HttpPost("DodajTreningKorisniku/{userId}/{workoutName}")]
        public async Task<IActionResult> AddWorkoutToUser(string userId, string workoutName)
        {
            try
            {
                await _userService.AddWorkout(userId, workoutName);
                return Ok("Workout added to user successfully");
            }
            catch (Exception)
            {
                
                return StatusCode(500, "An error occurred while adding workout to user.");
            }
        }
       
    }
}
