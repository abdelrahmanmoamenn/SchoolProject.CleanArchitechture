using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.IRepoistories;
using SchoolProject.Infrastructure.IRepositories;
using SchoolProject.Infrastructure.Repoistories;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrustructure.Abstracts.Views;
using SchoolProject.Infrustructure.Repositories;
using SchoolProject.Infrustructure.Repositories.Views;

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
            //Views
            services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();

            return services;
        }
    }
}

