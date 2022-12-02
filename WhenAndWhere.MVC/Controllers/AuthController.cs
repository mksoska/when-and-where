using Microsoft.AspNetCore.Mvc;

namespace WhenAndWhere.MVC.Controllers;

public class AuthController : Controller
{
    public IActionResult Meetup()
    {
        return View();
    }
}