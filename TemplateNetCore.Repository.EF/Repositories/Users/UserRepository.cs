using TemplateNetCore.Domain.Entities.Users;
using TemplateNetCore.Repository.Interfaces.Users;

namespace TemplateNetCore.Repository.EF.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
