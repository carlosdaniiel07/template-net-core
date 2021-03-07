
using TemplateNetCore.Domain.Interfaces.Users;

namespace TemplateNetCore.Service.Users
{
    public class HashService : IHashService
    {
        public bool Compare(string hash, string value) => BCrypt.Net.BCrypt.Verify(value, hash);

        public string Hash(string value) => BCrypt.Net.BCrypt.HashPassword(value);
    }
}
