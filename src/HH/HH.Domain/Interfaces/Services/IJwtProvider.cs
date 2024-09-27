using HH.Domain.Entitties;

namespace HH.Domain.Interfaces.Services
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}