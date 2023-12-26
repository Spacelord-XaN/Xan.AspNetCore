using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.TestWebApp.Controllers;
using Xan.AspNetCore.TestWebApp.Models.Crud;

namespace Xan.AspNetCore.TestWebApp.Routing;

public class ShipRouter
    : CrudRouter<ShipEntity>
{
    public ShipRouter(LinkGenerator linkGenerator)
        : base(linkGenerator)
    { }

    public string ToDetails(int id)
    {
        return GetUriByAction(nameof(ShipController.Details), new { id });
    }
}
