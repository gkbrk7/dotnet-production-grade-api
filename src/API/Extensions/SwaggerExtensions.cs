using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = ".NET 5 API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gokberk Yildirim",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/gkbrk7"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Sample Licence",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
        }
    }
}