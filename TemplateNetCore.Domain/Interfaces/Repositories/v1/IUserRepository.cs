using TemplateNetCore.Domain.Entities.v1;

namespace TemplateNetCore.Domain.Interfaces.Repositories.v1
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
