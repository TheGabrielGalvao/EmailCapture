using Domain.Entities.Auth;
using Domain.Model.Auth;

namespace Domain.Interface.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<AuthResponse> ExecuteAuth(AuthRequest request);

        Task<string> GenerateToken(User user);
    }
}
