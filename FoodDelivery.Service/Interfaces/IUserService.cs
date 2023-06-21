using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.User;
using System.Collections;

namespace FoodDelivery.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> CreateUser(UserViewModel viewModel);
        //Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers();
        Task<IBaseResponse<bool>> DeleteUser(int id);
        Task<User> GetUser(string userToken);
    }
}
