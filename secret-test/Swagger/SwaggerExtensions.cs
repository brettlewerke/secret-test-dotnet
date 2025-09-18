using Microsoft.OpenApi.Models;

namespace secret_test.Swagger

{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerWithAuthentication(this IServiceCollection services, IConfiguration config, ILogger logger)
        {

            services.AddSwaggerGen(options =>
            {



            });

            return services;
        }

        // Insert Swagger & its UI into the pipeline, prefill some auth info and title.
        public static WebApplication UseSwaggerWithOptions(this WebApplication app, IConfiguration config)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "secret_test Swagger UI";

            });
            return app;
        }

    }
}