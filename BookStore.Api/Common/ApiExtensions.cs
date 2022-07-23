using BookStore.Api.Common.Filters;
using BookStore.Api.Common.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace BookStore.Api.Common;

public static class ApiExtensions
{
    public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
    {
        var services = app.ApplicationServices;
        var provider = services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }

    public static IServiceCollection AddSwaggeredApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen(options =>
        {
            // for further customization
            options.OperationFilter<DefaultValuesFilter>();
        });
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }
}