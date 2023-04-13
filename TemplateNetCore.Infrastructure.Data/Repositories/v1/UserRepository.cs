using Microsoft.EntityFrameworkCore;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.v1;

namespace TemplateNetCore.Infrastructure.Data.Repositories.v1
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<User> GetByEmailAsync(string email) =>
            await dbSet.FirstOrDefaultAsync(user => user.Email == email);
    }
}
