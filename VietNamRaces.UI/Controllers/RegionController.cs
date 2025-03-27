using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using System.Text.Json;
using VietNamRaces.UI.Models;
using VietNamRaces.UI.Models.DTO;

namespace VietNamRaces.UI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7045/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

            }
            catch (Exception ex)
            {

            }
            
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7045/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")

            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if(response is not null)
            {
                return RedirectToAction("Index", "Region");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7045/api/regions/{id.ToString()}");
            
            if(response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();

            var hhtpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7045/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponse = await client.SendAsync(hhtpRequest);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadFromJsonAsync<RegionDto>();
            if(response is not null)
            {
                return RedirectToAction("Edit", "Region");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7045/api/regions/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Region");
            }catch(Exception ex)
            {
                throw;
            }

            return View("Edit");
        }
    }
}
