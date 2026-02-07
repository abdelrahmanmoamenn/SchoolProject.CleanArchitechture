using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastrcture.InfrastructureBases;
using SchoolProject.Infrastrcture.IRepoistories;
using SchoolProject.Infrastrcture.Repoistories;

namespace SchoolProject.Infrastrcture
{
    public static class ModuleInfrastructureDependices
    {
        public static IServiceCollection AddInfrastructureDependices(this IServiceCollection services) {
             services.AddTransient<IStudentRepository, StudentRepository>();
             services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
             return services;
                }
    }
}
