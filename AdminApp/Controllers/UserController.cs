using AdminApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels.System.Users;

namespace AdminApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;

        public UserController(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var sessionToken = HttpContext.Session.GetString("Token");
            var request = new GetUserPagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Keyword = keyword,
            };
            
            
            var data = await _userApiClient.GetUserPaging(request);
            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RegisterUser(request);

            if (result)
                return RedirectToAction("Index");

            return View();
        }
    }
}
