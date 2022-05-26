using Microsoft.AspNetCore.Mvc;

namespace ODataCoreUrlHelper.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(Routes.VersionedRoutePrefix)]
    public class ResourceManagementController : ControllerBase
    {
        [HttpPost("resources")]
        public IActionResult Create(ApiVersion version)
        {
            var location = Url.Link(Routes.GetResourceDataRoute, new
            {
                token = "id1",
                version = version.ToString()
            });

            return Created(location, "id1");
        }
    }
}
