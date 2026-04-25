using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.AuthServices.Implementations;
using SchoolProject.Service.AuthServices.Interfaces;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependices
    {
        public static IServiceCollection AddServiceDependices(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailsService, EmailsService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<IFileService, FileService>();
            return services;
        }
    }
}
