using System.Security.Claims;


namespace FoodDelivery.Models.Helpers
{
    public class IdentityHelper
    {
        public static string GetLogin(ClaimsPrincipal claimsPrincipal)
        {
            string userLogin = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserLogin")?.Value;

            return userLogin;
        }
    }
}
