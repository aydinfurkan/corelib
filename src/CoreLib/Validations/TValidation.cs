using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLib.Validations
{
    public static class TValidation
    {
        public static IServiceCollection AddValidation<T>(this IServiceCollection services)
        {
            services.AddMvc(options =>
                {
                    options.Filters.Add(new ModelStateFilter());
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<T>();
                });
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
            });
            
            return services;
        }
    }
}