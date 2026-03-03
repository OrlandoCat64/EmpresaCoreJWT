using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            ML.Login login = new ML.Login();
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(ML.Login login)
        {
            //consumir la api
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5176/api/Login/IniciarSesion");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ML.Login>("", login); //Serializar
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    string token = readTask.Result;

                    HttpContext.Response.Cookies.Append("session", token.Trim('"'), new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(5),
                        HttpOnly = true,
                        Secure = false
                    });

                    return RedirectToAction("Index", "Empleado");
                }
            }

            return View();

        }
    }
}
