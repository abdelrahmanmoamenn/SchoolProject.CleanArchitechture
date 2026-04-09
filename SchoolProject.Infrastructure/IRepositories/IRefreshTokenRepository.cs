using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.IRepositories
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
