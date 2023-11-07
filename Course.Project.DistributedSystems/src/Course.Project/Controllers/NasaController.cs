using Course.Project.NASA.Client;
using Course.Project.NASA.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Course.Project.Controllers
{
    public class NasaController : Controller
    {
        private readonly INasaClient _nasaClient;
        private readonly IConfiguration _configuration;

        public NasaController(INasaClient nasaClient, IConfiguration configuration)
        {
            _nasaClient = nasaClient;
            _configuration = configuration;
        }

        public IActionResult Image()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetImage(ImageRequestModel model)
        {
            string apiKey = _configuration["Nasa:ApiKey"];
            ImageOfTheDayResponse image = await _nasaClient.GetImageOfTheDayAsync(apiKey, model.Date);
            if(image is null)
                return View(default);

            var response = new ImageResponseModel(image);

            return View(response);
        }
    }

    public class ImageRequestModel
    {
        [DisplayName("Enter date for image")]
        public DateTimeOffset Date { get; set; }
    }

    public class ImageResponseModel
    {
        public ImageResponseModel(ImageOfTheDayResponse image)
        {
            Title = image.Title;
            Url = image.Url;
            Description = image.Explanation;
            Date = image.Date;
        }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
