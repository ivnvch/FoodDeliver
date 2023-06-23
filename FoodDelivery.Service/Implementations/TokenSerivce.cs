using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.Helpers;
using FoodDelivery.Models.ViewModel;
using FoodDelivery.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDelivery.Service.Implementations
{
    public class TokenSerivce: ITokenService
    {

        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        private readonly IUserService _userService;

        public TokenSerivce(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:TokenKey"]));
            _userService = userService;
        }

        public AuthResponseModel GetToken(User user)
        {
            ClaimsIdentity identity = GetIdentity(user);

            JwtSecurityToken token = GenerateToken(identity);
            string encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            JwtSecurityToken refreshToken = GenerateToken(identity);
            string encodedRefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            AuthResponseModel authResponse = new AuthResponseModel
            {
                AccessToken = encodedToken,
                RefreshToken = encodedRefreshToken,
                //IsOnboarded = user.IsOnboarded,
                //UserToken = user.Token,
                ValidFrom = token.ValidFrom,
                ValidTo = token.ValidTo,
                RefreshValidTo = refreshToken.ValidTo
            };

            return authResponse;
        }

        public async Task<AuthResponseModel> Refresh(RefreshTokenModel model)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:TokenKey"]);
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            };
            ClaimsPrincipal principal = default;
            try
            {
                principal = handler.ValidateToken(model.RefreshToken, validationParameters, out _);

                string userToken = IdentityHelper.GetLogin(principal);
                User user = await _userService.GetUser(userToken);

                return GetToken(user);
            }
            catch (Exception ex)
            {
                throw;
                //throw new InvalidAccessTokenException(ErrorMessages.Auth.INVALID_TOKEN);
            }
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "AuthToken"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserLogin",user.Login),
                //new Claim("UserToken",user.Token.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role.ToString()),
            };

            ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private JwtSecurityToken GenerateToken(ClaimsIdentity identity)
        {
            DateTime now = DateTime.UtcNow;

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:TokenKey"]));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                 _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                notBefore: now,
                claims: identity.Claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:TokenLifetimeMinutes"])),
                signingCredentials: signIn);

            return token;
        }

    }

    public class RefreshTokenModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
