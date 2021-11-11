using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CoreLib.Swagger
{
    public static class TSwagger
    {
        public static IServiceCollection AddTSwaggerGen(this IServiceCollection services, string title, string description, string name="v1", string version="v1", string author="Furkan AYDIN", string email="furkan139aydin@gmail.com", string license="MIT")
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name, new OpenApiInfo
                {
                    Version = version,
                    Title = title,
                    Description = description,
                    Contact = new OpenApiContact
                    {
                        Name = author,
                        Email = email,
                    },
                    License = new OpenApiLicense
                    {
                        Name = license,
                    }
                });
            });

            return services;
        }
        
        public static IApplicationBuilder UseTSwaggerUI(this IApplicationBuilder app, string name)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", name);
                c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
                c.ConfigObject.AdditionalItems.Add("theme", "agate");
            });
            return app;
        }
    }
}