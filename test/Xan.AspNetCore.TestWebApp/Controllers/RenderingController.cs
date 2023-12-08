using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.TestWebApp.Models.Rendering;

namespace Xan.AspNetCore.TestWebApp.Controllers;

public class RenderingController
    : Controller
{
    public IActionResult DefaultHtmlFactoryTests() 
        => View(new DefaultHtmlFactoryViewModel());

    [HttpPost]
    public IActionResult DefaultHtmlFactoryTests(DefaultHtmlFactoryViewModel model)
        => View(model);
}
