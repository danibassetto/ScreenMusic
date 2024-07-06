using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScreenMusic.WebMvc.Controllers;
using ScreenMusic.WebMvc.Services;

[AllowAnonymous]
public class AuthenticationController(AuthenticationServiceClient authenticationServiceClient) : Controller
{
    private readonly AuthenticationServiceClient _authenticationServiceClient = authenticationServiceClient;

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (ModelState.IsValid)
        {
            var response = await _authenticationServiceClient.LoginAsync(email, password);
            if (response.IsSuccess)
                return RedirectToAction(nameof(HomeController.Index), "Home");
            else
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authenticationServiceClient.LogoutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}