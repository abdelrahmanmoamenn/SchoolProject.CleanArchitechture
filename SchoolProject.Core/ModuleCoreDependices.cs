using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastrcture.IRepoistories;
using SchoolProject.Infrastrcture.Repoistories;
using System.Reflection;

namespace SchoolProject.Core
{
    public static class ModuleCoreDependices
    {
        public static IServiceCollection AddCoreDependices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
