using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;

namespace mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Claims()
        {
            return View();
        }

        public async Task<IActionResult> ServiceClaims()
        {
            // call api
            var token = await this.HttpContext.GetTokenAsync("access_token");
            using var apiClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:6001")
            };
            apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var data = new List<ClaimDto>();

            var response = await apiClient.GetAsync("/identity");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "401";
            }
            else
            {
                var content = await response.Content.ReadAsStreamAsync();
                data = await JsonSerializer.DeserializeAsync<List<ClaimDto>>(content);
            }

            return View(data);
        }
    }

    public class ClaimDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

    }
}
