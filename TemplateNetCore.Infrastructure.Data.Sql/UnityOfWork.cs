using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql.v1;
using TemplateNetCore.Infrastructure.Data.Repositories.v1;

namespace TemplateNetCore.Infrastructure.Data;

public class UnityOfWork : IUnityOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    private UserRepository userRepository;

    public UnityOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public IUserRepository UserRepository
    {
        get
        {
            userRepository ??= new UserRepository(_applicationDbContext);
            return userRepository;
        }
    }

    public void Commit() =>
        _applicationDbContext.SaveChanges();

    public async Task CommitAsync() =>
        await _applicationDbContext.SaveChangesAsync();
}
