using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Net.Mime;

namespace ODataCoreUrlHelper.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.Count | AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.Expand)]
    [Route(Routes.ResourceDataControllerRoute)]
    public class ResourceDataController : ODataController
    {
        [HttpGet("data", Name = Routes.GetResourceDataRoute)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResourceData))]
        public IActionResult GetData(string token)
        {
            var resource = new ResourceData
            {
                Id = token
            };

            return Ok(resource);
        }
    }
}
