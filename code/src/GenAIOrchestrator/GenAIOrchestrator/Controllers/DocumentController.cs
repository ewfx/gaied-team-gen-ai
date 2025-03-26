using Microsoft.AspNetCore.Mvc;

namespace GenAIOrchestrator.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
