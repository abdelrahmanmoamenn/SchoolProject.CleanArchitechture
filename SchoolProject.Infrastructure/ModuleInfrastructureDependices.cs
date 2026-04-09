using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.IRepoistories;
using SchoolProject.Infrastructure.IRepositories;
using SchoolProject.Infrastructure.Repoistories;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrustructure.Repositories;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependices
    {
        public static IServiceCollection AddInfrastructureDependices(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IInstructorsRepository, InstructorsRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}

