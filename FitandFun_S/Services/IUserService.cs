using FitandFun.Models;

namespace FitandFun.Services{

    public interface IUserService 
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetById(string id);
        Task CreateAsync(User user);
        Task UpdateAsync(string id, User user);
        Task DeleteAsync(string id);
        Task AddWorkout(string userId, string workoutName);
        Task<IEnumerable<User>> GetUsersWithWorkouts();
        Task<User> GetIdByUsername(string username);

    }
}