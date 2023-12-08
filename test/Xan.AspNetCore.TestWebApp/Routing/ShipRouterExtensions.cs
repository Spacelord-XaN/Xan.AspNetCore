using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.TestWebApp.Controllers;
using Xan.AspNetCore.TestWebApp.Models.Crud;

namespace Xan.AspNetCore.TestWebApp.Routing;

public static class ShipRouterExtensions
{
    public static string ToDetails(this ICrudRouter<ShipEntity> router, int id)
    {
        ArgumentNullException.ThrowIfNull(router);

        return router.GetUriByAction(nameof(ShipController.Details), new { id });
    }
}
