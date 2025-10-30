
using Implementation.AccessedLinkService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

public class ControllerActionDiscoveryService
{
    public void DiscoverControllerActions()
    {
        // Use reflection to scan and discover controllers and actions
        // within your project assemblies.
    }
}

public class ControllerActionDiscoveryHostedService : IHostedService
{
    private readonly ControllerActionDiscoveryService _discoveryService;
    public readonly AccessedLinkService accessedLinkService;

    public ControllerActionDiscoveryHostedService(ControllerActionDiscoveryService discoveryService, AccessedLinkService accessedLinkService)
    {
        _discoveryService = discoveryService;
        this.accessedLinkService = accessedLinkService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _discoveryService.DiscoverControllerActions();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public async void DiscoverControllerActions()
    {
        var controllerTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type)));
        var accessedLinkedList = new List<Mstech.Entity.Etity.AccessedLink>();
        foreach (var controllerType in controllerTypes)
        {
            var controllerAttribute = controllerType.GetCustomAttributes(typeof(RouteAttribute), true)
                .FirstOrDefault() as RouteAttribute;

            string controllerName = controllerType.Name.Replace("Controller", "");
            string AreaName = controllerAttribute != null ? controllerAttribute.Template.Split('/')[0] : "";

            var actionMethods = controllerType.GetMethods()
                .Where(method => method.IsPublic && typeof(IActionResult).IsAssignableFrom(method.ReturnType));

            foreach (var actionMethod in actionMethods)
            {
                var actionAttribute = actionMethod.GetCustomAttributes(typeof(RouteAttribute), true)
                    .FirstOrDefault() as RouteAttribute;

                string actionName = actionMethod.Name;

                if (actionAttribute != null)
                {

                    accessedLinkedList.Add(new Mstech.Entity.Etity.AccessedLink()
                    {
                        Action = actionName,
                        Controller = controllerName,
                        Area = AreaName,
                        IsInMenue = actionName == "Index" ? true : false

                    });
                    string actionRoute = actionAttribute.Template;
                    Console.WriteLine($"Action '{actionName}' has route: '{actionRoute}'");
                }
            }
        }
        foreach (var accessedLinked in accessedLinkedList)
        {
            accessedLinkService.Insert(accessedLinked);
        }
    }
}


