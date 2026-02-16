using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.IRepoistories;
using SchoolProject.Infrastructure.Repoistories;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependices
    {
        public static IServiceCollection AddInfrastructureDependices(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}

