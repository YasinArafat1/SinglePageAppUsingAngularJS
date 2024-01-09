using Microsoft.AspNetCore.Mvc;
using PiplikaTaskUI.Models;
using PiplikaTaskUI.Models.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PiplikaTaskUI.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DepartmentsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<DepartmentDTO> response = new List<DepartmentDTO>();

            try
            {
                //get all dept from web api
                var client = httpClientFactory.CreateClient();
                var httpresponsemessage = await client.GetAsync("https://localhost:7133/api/Departments");
                httpresponsemessage.EnsureSuccessStatusCode();

                response.AddRange(await httpresponsemessage.Content.ReadFromJsonAsync<IEnumerable<DepartmentDTO>>());


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

        public async Task<IActionResult> Create(AddDepartmentViewModel addDepartmentViewModel)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7133/api/Departments"),
                Content = new StringContent(JsonSerializer.Serialize(addDepartmentViewModel),Encoding.UTF8,"application/json")

            };
          var httpresponseMessage = await client.SendAsync(httpRequestMessage);
            httpresponseMessage.EnsureSuccessStatusCode();


         var response =    await httpresponseMessage.Content.ReadFromJsonAsync<DepartmentDTO>();

            if (response is not null)
            {
                return RedirectToAction("Index","Departments");
            }


            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
         var  response =  await client.GetFromJsonAsync<DepartmentDTO>($"https://localhost:7133/api/Departments/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentDTO departmentDTO)
        {
            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7133/api/Departments/{departmentDTO.DepartmentId}"),
                Content = new StringContent(JsonSerializer.Serialize(departmentDTO), Encoding.UTF8, "application/json")

            };



       var httpResponseMessage =      await client.SendAsync(request);

            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<DepartmentDTO>();
            if (response is not null)
            {
                return RedirectToAction("Index","Departments");
            }
             
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<DepartmentDTO>($"https://localhost:7133/api/Departments/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentDTO departmentDTO)
        {

            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponse = await client.DeleteAsync($"https://localhost:7133/api/Departments/{departmentDTO.DepartmentId}");

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
