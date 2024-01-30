using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Service.ServerToServer.Dto.ClientCredential;
using Service.ServerToServer.Dto.ServerRequest;
using Service.ServerToServer.Service.ServerRequestService;

namespace Service.ServerToServer.Service.BaseRequestService
{
    public class BaseRequestService<ReturnModel> : IBaseRequestService<ReturnModel>
    {
        private readonly IServerRequestService _serverRequestService;

        public BaseRequestService(IServerRequestService serverRequestService)
        {
            _serverRequestService = serverRequestService;
        }

        public async Task<ServerResponseDto<ReturnModel>> Get(string url, string accessToken)
        {
            return await _serverRequestService.Get<ReturnModel>(url, accessToken);
        }

        public async Task<ServerResponseDto<ReturnModel>> Send<RequestDataModel>(ServerRequestDto<RequestDataModel> data, string accessToken)
        {
            return await _serverRequestService.Send<RequestDataModel,ReturnModel>(data, accessToken);
        }

        public async Task<ServerResponseDto<ReturnModel>> Send<RequestDataModel>(ServerRequestDto<RequestDataModel> data, string accessToken, HttpMethod method)
        {
            return await _serverRequestService.Send<RequestDataModel, ReturnModel>(data, accessToken, method);
        }

        public async Task<ServerResponseDto<ReturnModel>> Send<RequestDataModel>(ServerRequestDto<RequestDataModel> data, HttpContext context)
        {
            return await _serverRequestService.Send<RequestDataModel,ReturnModel>(data, context);
        }

        public async Task<ServerResponseDto<ReturnModel>> Send<RequestDataModel>(ServerRequestDto<RequestDataModel> data, HttpContext context, HttpMethod method)
        {
            return await _serverRequestService.Send<RequestDataModel, ReturnModel>(data, context, method);
        }

        public async Task<ServerResponseDto<ReturnModel>> Send<RequestDataModel>(ServerRequestDto<RequestDataModel> data, ClientCredentialDto clientCredential, string cacheName)
        {
            return await _serverRequestService.Send<RequestDataModel, ReturnModel>(data, clientCredential, cacheName);
        }

        public async Task<ServerResponseDto<ReturnModel>> Send<RequestDataModel>(ServerRequestDto<RequestDataModel> data, ClientCredentialDto clientCredential)
        {
            return await _serverRequestService.Send<RequestDataModel, ReturnModel>(data, clientCredential);
        }

        public async Task<ServerResponseDto<ReturnModel>> Send<RequestDataModel>(ServerRequestDto<RequestDataModel> data, ClientCredentialDto clientCredential, HttpMethod method, string cacheName)
        {
            return await _serverRequestService.Send<RequestDataModel, ReturnModel>(data, clientCredential, method, cacheName);
        }

        public async Task<ServerResponseDto<ReturnModel>> Send<RequestDataModel>(ServerRequestDto<RequestDataModel> data, ClientCredentialDto clientCredential, HttpMethod method)
        {
            return await _serverRequestService.Send<RequestDataModel, ReturnModel>(data, clientCredential, method);
        }
    }
}
