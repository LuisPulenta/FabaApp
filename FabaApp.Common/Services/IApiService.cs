using FabaApp.Common.Models;
using System.Threading.Tasks;

namespace FabaApp.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller);



        bool CheckConnection();

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

        Task<ResponseT<object>> GetList2Async<T>(
                string urlBase,
                string servicePrefix,
                string controller);

        Task<ResponseT<object>> PostAsync<T>(
           string urlBase,
           string servicePrefix,
           string controller,
           T model,
           string tokenType,
           string accessToken);

    }
}