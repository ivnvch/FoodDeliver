using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Helpers
{
    public class IdentityHelper
    {
        public static string GetLogin(ClaimsPrincipal claimsPrincipal)
        {
            string username = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "UserLogin")?.Value;

            return username;
        }
    }
}
