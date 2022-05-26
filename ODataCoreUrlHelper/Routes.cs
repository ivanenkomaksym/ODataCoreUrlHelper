using ODataCoreUrlHelper.Controllers;

namespace ODataCoreUrlHelper
{
    internal static class Routes
    {
        public const string VersionedRoutePrefix = "api/v{version:apiVersion}";

        public const string ResourceDataControllerRoute = "api/v{version:apiVersion}/resources/{token}";

        public const string GetResourceDataRoute = nameof(ResourceDataController) + "_" + nameof(ResourceDataController.GetData);
    }
}
