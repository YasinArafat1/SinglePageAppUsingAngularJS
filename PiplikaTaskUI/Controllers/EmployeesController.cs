using Microsoft.AspNetCore.Mvc;
using PiplikaTaskUI.Models;
using PiplikaTaskUI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace PiplikaTaskUI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<EmployeeDTO> response = new List<EmployeeDTO>();

            try
            {
                //get all dept from web api
                var client = httpClientFactory.CreateClient();
                var httpresponsemessage = await client.GetAsync("https://localhost:7133/api/Employees");
                httpresponsemessage.EnsureSuccessStatusCode();

                response.AddRange(await httpresponsemessage.Content.ReadFromJsonAsync<IEnumerable<EmployeeDTO>>());


            }
            catch (Exception)
            {

                throw;
            }

            return View(response);
        }



        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(AddEmployeeViewModel addEmployeeViewModel)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7133/api/Employees"),
                Content = new StringContent(JsonSerializer.Serialize(addEmployeeViewModel), Encoding.UTF8, "application/json")

            };
            var httpresponseMessage = await client.SendAsync(httpRequestMessage);
            httpresponseMessage.EnsureSuccessStatusCode();


            var response = await httpresponseMessage.Content.ReadFromJsonAsync<DepartmentDTO>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Employees");
            }


            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<EmployeeDTO>($"https://localhost:7133/api/Employees/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeDTO employeeDTO )
        {
            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7133/api/Employees/{employeeDTO.EmployeeId}"),
                Content = new StringContent(JsonSerializer.Serialize(employeeDTO), Encoding.UTF8, "application/json")

            };



            var httpResponseMessage = await client.SendAsync(request);

            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<DepartmentDTO>();
            if (response is not null)
            {
                return RedirectToAction("Index", "Employees");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<EmployeeDTO>($"https://localhost:7133/api/Employees/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeDTO employeeDTO)
        {

            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponse = await client.DeleteAsync($"https://localhost:7133/api/Employees/{employeeDTO.EmployeeId}");

                httpResponse.EnsureSuccessStatusCode();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
    }
}
