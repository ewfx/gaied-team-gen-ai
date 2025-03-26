using Microsoft.AspNetCore.Mvc;

namespace GenAIOrchestrator.Controllers
{
    public class AIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
