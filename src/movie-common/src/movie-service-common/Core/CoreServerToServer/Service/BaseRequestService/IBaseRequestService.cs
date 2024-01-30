using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Service.ServerToServer.Dto.ClientCredential;
using Service.ServerToServer.Dto.ServerRequest;

namespace Service.ServerToServer.Service.BaseRequestService
{
    public interface IBaseRequestService<ReturnModel>
    {
        public Task<ServerResponseDto<ReturnModel>> Get(string url, string accessToken);
        public Task<ServerResponseDto<ReturnModel>> Send<RequestModel>(ServerRequestDto<RequestModel> data, string accessToken);
        public Task<ServerResponseDto<ReturnModel>> Send<RequestModel>(ServerRequestDto<RequestModel> data, string accessToken, HttpMethod method);
        public Task<ServerResponseDto<ReturnModel>> Send<RequestModel>(ServerRequestDto<RequestModel> data, HttpContext context);
        public Task<ServerResponseDto<ReturnModel>> Send<RequestModel>(ServerRequestDto<RequestModel> data, HttpContext context, HttpMethod method);
        public Task<ServerResponseDto<ReturnModel>> Send<RequestModel>(ServerRequestDto<RequestModel> data, ClientCredentialDto clientCredential);
        public Task<ServerResponseDto<ReturnModel>> Send<RequestModel>(ServerRequestDto<RequestModel> data, ClientCredentialDto clientCredential, string cacheName);
        public Task<ServerResponseDto<ReturnModel>> Send<RequestModel>(ServerRequestDto<RequestModel> data, ClientCredentialDto clientCredential, HttpMethod method);
        public Task<ServerResponseDto<ReturnModel>> Send<RequestModel>(ServerRequestDto<RequestModel> data, ClientCredentialDto clientCredential, HttpMethod method, string cacheName);
    }
}
