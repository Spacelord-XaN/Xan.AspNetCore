namespace Xan.AspNetCore.Mvc;

public static class MvcHelper
{
    public static string ControllerName<TController>()
    {
        Type entityType = typeof(TController);
        return entityType.Name.Replace("Controller", "");
    }
}
