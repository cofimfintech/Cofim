using System.Threading.Tasks;
using Cofim.Common.Model.Request;
using Cofim.Common.Model.Response;

namespace Cofim.Common.Services
{
    public interface IApiService
    {
        Task<ResponseAPI<TokenResponse>> GetTokenAsync(string urlBase, string servicePrefix, string controller, LoginTARequest request);

        Task<ResponseAPI<UsuarioResponse>> GetUsuarioByEmailAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, string email);

        Task<bool> CheckConnectivityAsync(string url);

    }
}