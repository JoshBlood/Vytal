using Microsoft.AspNetCore.Mvc;
using VytalSign.Services;

namespace VytalSign.Controllers
{
    public class ModelsController : Controller
    {
        private readonly ApiService _apiService;

        public ModelsController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var models = await _apiService.GetHeartbeatsAsync();
            return View(models);
        }
    }
}
