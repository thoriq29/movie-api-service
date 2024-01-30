using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Service.ServerToServer.Dto.ClientCredential;
using Service.ServerToServer.Dto.ServerRequest;

namespace Service.ServerToServer.Service.ServerRequestService
{
    #nullable enable
    public interface IServerRequestService
    {
        public Task<ServerResponseDto<Model>> Get<Model>(string url, string accessToken);
        public Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, string accessToken);
        public Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, string accessToken, HttpMethod method);
        public Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, HttpContext context);
        public Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, HttpContext context, HttpMethod method);
        public Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, ClientCredentialDto clientCredential);
        public Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, ClientCredentialDto clientCredential, string cacheName);
        public Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, ClientCredentialDto clientCredential, HttpMethod method);
        public Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, ClientCredentialDto clientCredential, HttpMethod method, string cacheName);
        public string? GetAccessToken(HttpContext context);
        public Task<string?> GetAccessToken(ClientCredentialDto clientCredential, string cacheName);
        public Task<string?> GetAccessToken(ClientCredentialDto clientCredential);
        public Task<HttpResponseMessage> SendRaw<T>(string url, T data, string accessToken, HttpMethod method);
        public Task SetAccessTokenCache(string accessToken, string cacheName);
        public Task<string> CreateAccessToken(ClientCredentialDto clientCredential);
    }
}
