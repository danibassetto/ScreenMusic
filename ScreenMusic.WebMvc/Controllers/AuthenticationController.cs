using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScreenMusic.WebMvc.Services;

namespace ScreenMusic.WebMvc.Controllers;

[AllowAnonymous]
public class AuthenticationController(AuthenticationServiceClient authenticationServiceClient) : Controller
{
    private readonly AuthenticationServiceClient _authenticationServiceClient = authenticationServiceClient;

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password)
    {
        var result = await _authenticationServiceClient.Login(email, password);
        if (result.IsSuccess)
            return RedirectToAction(nameof(HomeController.Index), "Home");
        else
        {
            ViewData["ErrorMessage"] = result.Error;
            return View();
        }
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string email, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ViewData["ErrorMessage"] = "As senhas não coincidem.";
            return View();
        }

        var result = await _authenticationServiceClient.Register(email, password);
        if (result.IsSuccess)
            return RedirectToAction(nameof(Login));
        else
        {
            ViewData["ErrorMessage"] = result.Error;
            return View();
        }
    }    

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authenticationServiceClient.Logout();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}