using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Cofim.Common.Model.Request;
using Cofim.Common.Model.Response;

namespace Cofim.Common.Services
{
    public class ApiService : IApiService
    {
        public async Task<ResponseAPI<TokenResponse>> GetTokenAsync(string urlBase,
                                                                    string controller, 
                                                                    string method,
                                                                    LoginTARequest request)
        {
            try
            {
                var requestString = JsonConvert.SerializeObject(request); //LLamada por PostMAN
                var content       = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client        = new HttpClient { BaseAddress = new Uri(urlBase) };
                var url           = $"{controller}/{method}";

                var response = await client.PostAsync(url, content); //Consumo de Servicio por metodo POST
                var result   = await response.Content.ReadAsStringAsync();// Respuesta de la llamada API

                if (!response.IsSuccessStatusCode) //Si regreso un BAd request
                { return new ResponseAPI<TokenResponse> { IsSuccess = false, Message = result }; }

                var token = JsonConvert.DeserializeObject<TokenResponse>(result);

                return new ResponseAPI<TokenResponse> { IsSuccess = true, Result = token };
            }
            catch (Exception ex)
                  { return new ResponseAPI<TokenResponse> { IsSuccess = false, Message = ex.Message }; }
        }

        public async Task<ResponseAPI<UsuarioResponse>> GetUsuarioByEmailAsync(string urlBase,
                                                                               string controller,
                                                                               string method,
                                                                               string tokenType,
                                                                               string accessToken,
                                                                               string email)
        {
            try
            {
                var request    = new EmailRequest { Email = email };
                var requestStr = JsonConvert.SerializeObject(request);
                var content    = new StringContent(requestStr, Encoding.UTF8, "application/json");
                var client     = new HttpClient { BaseAddress = new Uri(urlBase) };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);

                var url      = $"{controller}/{method}";
                var response = await client.PostAsync(url, content);
                var result   = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                   { return new ResponseAPI<UsuarioResponse> { IsSuccess = false, Message = result }; }

                var usuario = JsonConvert.DeserializeObject<UsuarioResponse>(result);

                return new ResponseAPI<UsuarioResponse> { IsSuccess = true, Result = usuario };

            }
            catch (Exception ex)
                  { return new ResponseAPI<UsuarioResponse> { IsSuccess = false, Message = ex.Message }; }
        }

        public async Task<bool> CheckConnectivityAsync(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
               { return false; }

            return await CrossConnectivity.Current.IsRemoteReachable(url);
        }

    }//CLASS
}//NAMESACE
