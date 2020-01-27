using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class FluentValidationHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder ConfigureFluentValidation(this IMvcBuilder mvcBuilder)
        {
            //previne o FluentValidation de retornar bad request automaticamente
            mvcBuilder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return mvcBuilder.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                options.LocalizationEnabled = true;
                
            });
        }
    }
}