using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc;

namespace Xan.AspNetCore.Tests.Mvc.AbstractXanControllerTests;

public class TestController
    : AbstractXanController
{
    public TestController()
    {
        ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }
}
