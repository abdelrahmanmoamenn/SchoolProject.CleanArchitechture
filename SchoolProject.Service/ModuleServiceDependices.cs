using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implmentations;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependices
    {
        public static IServiceCollection AddServiceDependices(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            return services;
        }
    }
}
