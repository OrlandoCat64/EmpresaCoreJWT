using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmpleadoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            List<ML.Empleado> empleados = new List<ML.Empleado>();

            var token = HttpContext.Request.Cookies["session"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5176/api/Empleado/");

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = client.GetAsync("GetAllEmpleado").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var resultAPI = System.Text.Json.JsonSerializer.Deserialize<ML.Result>(json, options);

                if (resultAPI != null && resultAPI.Objects != null)
                {
                    foreach (var obj in resultAPI.Objects)
                    {
                        var empleadoJson = obj.ToString();
                        var empleado = System.Text.Json.JsonSerializer.Deserialize<ML.Empleado>(empleadoJson, options);
                        empleados.Add(empleado);
                    }
                }
            }

            return View(empleados);
        }

        public IActionResult Details(int id)
        {
            ML.Empleado empleado = new ML.Empleado();

            var token = HttpContext.Request.Cookies["session"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Login");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5176/api/Empleado/");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = client.GetAsync($"GetByIdEmpleado/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var resultAPI = JsonSerializer.Deserialize<ML.Result>(json, options);

                if (resultAPI != null && resultAPI.Object != null)
                {
                    empleado = JsonSerializer.Deserialize<ML.Empleado>(resultAPI.Object.ToString(), options);
                }
            }

            return View(empleado);
        }

        [HttpPost]
        public IActionResult Create(ML.Empleado empleado)
        {
            var token = HttpContext.Request.Cookies["session"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Login");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5176/api/Empleado/");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = client.PostAsJsonAsync("AddEmpleado", empleado).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        public IActionResult Delete(int id)
        {
            var token = HttpContext.Request.Cookies["session"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Login");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5176/api/Empleado/");

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = client.DeleteAsync($"DeleteEmpleado/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var token = HttpContext.Request.Cookies["session"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Login");

            ML.Empleado empleado = new ML.Empleado();

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5176/api/Empleado/");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = client.GetAsync($"GetByIdEmpleado/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var resultAPI = System.Text.Json.JsonSerializer.Deserialize<ML.Result>(json, options);

                if (resultAPI != null && resultAPI.Object != null)
                {
                    empleado = System.Text.Json.JsonSerializer.Deserialize<ML.Empleado>(
                        resultAPI.Object.ToString(), options);
                }
            }

            return View(empleado);
        }

        [HttpPost]
        public IActionResult Edit(ML.Empleado empleado)
        {
            var token = HttpContext.Request.Cookies["session"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Login");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5176/api/Empleado/");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = client.PutAsJsonAsync("UpdateEmpleado", empleado).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        [HttpPost]
        public IActionResult Guardar(ML.Empleado empleado)
        {
            var token = HttpContext.Request.Cookies["session"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Login");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5176/api/Empleado/");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response;

            if (empleado.IdEmpleado == 0)
            {
                response = client.PostAsJsonAsync("AddEmpleado", empleado).Result;
            }
            else
            {
                response = client.PutAsJsonAsync("UpdateEmpleado", empleado).Result;
            }

            return RedirectToAction("Index");
        }
    }
}
