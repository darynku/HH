using HH.Domain.Entitties;

namespace HH.Domain.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}