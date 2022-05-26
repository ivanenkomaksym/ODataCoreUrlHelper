using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;

namespace ODataCoreUrlHelper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddOData(options =>
            {
                options.Select().Count().SkipToken().Expand().SetMaxTop(10);
                options.AddRouteComponents(Routes.ResourceDataControllerRoute, BuildEdmModel());
            })
            .AddODataNewtonsoftJson();

            services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = false);
            services.AddVersionedApiExplorer(options => options.SubstituteApiVersionInUrl = true);

            services.AddSwaggerGen(setupAction =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var info = new OpenApiInfo
                    {
                        Title = "ODataCoreUrlHelper",
                        Version = description.ApiVersion.ToString()
                    };
                    setupAction.SwaggerDoc(description.GroupName, info);
                }
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseODataRouteDebug();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                foreach (var description in versionDescription.ApiVersionDescriptions)
                {
                    setupAction.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"Test v{description.GroupName}");
                }
            });
        }

        private static IEdmModel BuildEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<ResourceData>("data");

            return builder.GetEdmModel();
        }

    }
}
