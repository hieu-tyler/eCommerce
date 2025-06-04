using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Data.Entities;
using ViewModels.System.Users;

namespace Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<string?> Authenticate(LoginRequest request)
        {
            var username = await _userManager.FindByNameAsync(request.UserName); 

            if (username == null) return null;
        
            var result = _signInManager.PasswordSignInAsync(username, request.Password, request.RememberMe, true);

            if (!result.Result.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(username);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, username.Email),
                new Claim(ClaimTypes.GivenName, username.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";", roles)),
            };

            // Encrypt the claims to a JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Tokens:Issuer"],
                audience: _config["Tokens:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new AppUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };
            var result = _userManager.CreateAsync(user, request.Password);

            if (result.Result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
