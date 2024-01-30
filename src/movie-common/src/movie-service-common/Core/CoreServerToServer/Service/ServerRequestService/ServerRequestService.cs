using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Service.Core.Interfaces.Log;
using Service.ServerToServer.Constants;
using Service.ServerToServer.Dto;
using Service.ServerToServer.Dto.ClientCredential;
using Service.ServerToServer.Dto.PasswordGrant;
using Service.ServerToServer.Dto.ServerRequest;
using Service.ServerToServer.Utils;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Linq;

namespace Service.ServerToServer.Service.ServerRequestService
{
    #nullable enable
    public class ServerRequestService : IServerRequestService
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _erroFactory;
        private readonly IDistributedCache _cache;
        private readonly IHttpClientFactory _httpClientFactory;

        public ServerRequestService(ICoreLogger logger, IErrorFactory error, IDistributedCache cache, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _erroFactory = error;
            _cache = cache;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServerResponseDto<ReturnModel>> Get<ReturnModel>(string url, string accessToken)
        {
            return await ExecuteGetRequest<ReturnModel>(url, accessToken, HttpMethod.Get);
        }

        public async Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, string accessToken)
        {
            return await CreateRequest<T, Model>(data, accessToken, HttpMethod.Get);
        }

        public async Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, string accessToken, HttpMethod method)
        {
            return await CreateRequest<T, Model>(data, accessToken, method);
        }

        public async Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, HttpContext context)
        {
            var accessToken = GetAccessToken(context);
            return await CreateRequest<T, Model>(data, accessToken, HttpMethod.Get);
        }

        public async Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, HttpContext context, HttpMethod method)
        {
            var accessToken = GetAccessToken(context);
            return await CreateRequest<T, Model>(data, accessToken, method);
        }

        public async Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, ClientCredentialDto clientCredential, string cacheName)
        {
            var accessToken = await GetAccessToken(clientCredential, cacheName);
            return await CreateRequest<T, Model>(data, accessToken, HttpMethod.Get);
        }

        public async Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, ClientCredentialDto clientCredential)
        {
            var cacheName = CacheUtil.FinalCacheName($"{RedisConfiguration.AccessTokenCacheName}-{clientCredential.ClientId}");
            var accessToken = await GetAccessToken(clientCredential, cacheName);
            return await CreateRequest<T, Model>(data, accessToken, HttpMethod.Get);
        }

        public async Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, ClientCredentialDto clientCredential, HttpMethod method, string cacheName)
        {
            var accessToken = await GetAccessToken(clientCredential, cacheName);
            return await CreateRequest<T, Model>(data, accessToken, method);
        }

        public async Task<ServerResponseDto<Model>> Send<T, Model>(ServerRequestDto<T> data, ClientCredentialDto clientCredential, HttpMethod method)
        {
            var cacheName = CacheUtil.FinalCacheName($"{RedisConfiguration.AccessTokenCacheName}-{clientCredential.ClientId}");
            var accessToken = await GetAccessToken(clientCredential, cacheName);
            return await CreateRequest<T, Model>(data, accessToken, method);
        }

        private async Task<ServerResponseDto<Model>> ExecuteGetRequest<Model>(string url, string? accessToken, HttpMethod method)
        {
            var response = new ServerResponseDto<Model>();
            response.Url = url;
            response.Status = HttpStatusCode.InternalServerError;

            try
            {
                var request = await GetRawRequest(url, accessToken, method);

                response.Status = HttpStatusCode.OK;
                response.Message = HttpStatusCode.OK.ToString();

                var responseBody = await request.Content.ReadAsStringAsync();

                response.DataRequest = JsonConvert.DeserializeObject<Model>(responseBody);
            }
            catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = e.Message;
                _logger.LogError(
                    _erroFactory.CreateError("118000", "Failed to send request from server to server service"),
                    $"{e.Message}",
                    $"Params: url {url}",
                    e,
                    nameof(Send));
            }

            return response;
        }

        public async Task<HttpResponseMessage> GetRawRequest(string url, string? accessToken, HttpMethod method)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(url),
            };

            if(accessToken != null)
            {
                requestMessage.Content ??= new StringContent("{}", Encoding.UTF8, "application/json");
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var client = new HttpClient();
            var response = await client.SendAsync(requestMessage);
            return response;
        }

        private async Task<ServerResponseDto<Model>> CreateRequest<T, Model>(ServerRequestDto<T> data, string? accessToken, HttpMethod method)
        {
            var response = new ServerResponseDto<Model>();
            response.Url = data.Url;
            response.Status = HttpStatusCode.InternalServerError;

            try
            {
                var request = await SendRaw(data.Url, data.Data, accessToken, method);

                response.Status = HttpStatusCode.OK;
                response.Message = HttpStatusCode.OK.ToString();

                var responseBody = await request.Content.ReadAsStringAsync();

                response.DataRequest = JsonConvert.DeserializeObject<Model>(responseBody);
            }
            catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = e.Message;
                _logger.LogError(
                    _erroFactory.CreateError("118000", "Failed to send request from server to server service"),
                    $"{e.Message}",
                    $"Params: url {data.Url}, data {data.Data}",
                    e,
                    nameof(Send));
            }

            return response;
        }

        public async Task<HttpResponseMessage> SendRaw<T>(string url, T data, string? accessToken, HttpMethod method)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(url),
                Content = new StringContent(JsonConvert.SerializeObject(data))
            };

            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var client = new HttpClient();
            var response = await client.SendAsync(requestMessage);
            return response;
        }

        public string? GetAccessToken(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
            return authHeaderValue.Parameter;
        }

        public async Task<string?> GetAccessToken(ClientCredentialDto clientCredential)
        {
            string? accessToken = null;
            var cacheName = CacheUtil.FinalCacheName($"{RedisConfiguration.AccessTokenCacheName}-{clientCredential.ClientId}");

            try
            {
                accessToken = await _cache.GetStringAsync(cacheName);

                if (CheckAccessTokenExpiration(accessToken))
                {
                    accessToken = await CreateAccessToken(clientCredential);

                    await SetAccessTokenCache(accessToken, cacheName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    _erroFactory.CreateError(ErrorConstants.E118002.code, ErrorConstants.E118002.message),
                    $"Failed to get token because an exception has occurred",
                    $"Params: cache name {cacheName}, data {clientCredential}",
                    ex,
                    nameof(GetAccessToken));
            }

            return accessToken;
        }

        public async Task<string?> GetAccessToken(ClientCredentialDto clientCredential, string cacheName)
        {
            string? accessToken = null;

            try
            {
                accessToken = await _cache.GetStringAsync(cacheName);

                if (CheckAccessTokenExpiration(accessToken))
                {
                    accessToken = await CreateAccessToken(clientCredential);

                    await SetAccessTokenCache(accessToken, cacheName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    _erroFactory.CreateError(ErrorConstants.E118002.code, ErrorConstants.E118002.message),
                    $"Failed to get token because an exception has occurred",
                    $"Params: cache name {cacheName}, data {clientCredential}",
                    ex,
                    nameof(GetAccessToken));
            }

            return accessToken;
        }

        public async Task SetAccessTokenCache(string? accessToken, string cacheName)
        {
            if (accessToken != null)
            {
                var cacheOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(GetTokenExpirationDate(accessToken));
                await _cache.SetStringAsync(cacheName, accessToken, cacheOptions);
            }
        }

        public async Task<string> CreateAccessToken(ClientCredentialDto clientCredential)
        {
            string? accessToken = null;

            var client = _httpClientFactory.CreateClient();

            var discoveryDocumentResponse = await GetDiscoveryDocumentAsync(client, clientCredential.AuthServerUrl);

            var tokenResponse = await RequestAccessToken(client, discoveryDocumentResponse.TokenEndpoint, clientCredential);

            if (tokenResponse.IsError)
            {
                _logger.LogError(
                    _erroFactory.CreateError(ErrorConstants.E118004.code, ErrorConstants.E118004.message),
                    string.Format("Error message: {0}, Error description: {1}", tokenResponse.Error, tokenResponse.ErrorDescription),
                    $"CliendId: {clientCredential.ClientId}, AuthServerUrl: {clientCredential.AuthServerUrl}",
                    null,
                    nameof(CreateAccessToken));

                throw new Exception("Request access token failed");
            }

            accessToken = tokenResponse.AccessToken;

            return accessToken;
        }

        async Task<string> CreatePasswordGrantAccessToken(PasswordGrantDto clientCredential)
        {
            string? accessToken = null;

            var client = _httpClientFactory.CreateClient();

            var discoveryDocumentResponse = await GetDiscoveryDocumentAsync(client, clientCredential.AuthServerUrl);

            var tokenResponse = await RequestPasswordGrantAccessToken(client, discoveryDocumentResponse.TokenEndpoint, clientCredential);

            if (tokenResponse.IsError)
            {
                _logger.LogError(
                    _erroFactory.CreateError(ErrorConstants.E118004.code, ErrorConstants.E118004.message),
                    string.Format("Error message: {0}, Error description: {1}", tokenResponse.Error, tokenResponse.ErrorDescription),
                    $"CliendId: {clientCredential.ClientId}, AuthServerUrl: {clientCredential.AuthServerUrl}",
                    null,
                    nameof(CreatePasswordGrantAccessToken));

                throw new Exception("Request access token failed");
            }

            accessToken = tokenResponse.AccessToken;

            return accessToken;
        }

        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(HttpClient client, string authServerUrl)
        {
            var discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync(authServerUrl);

            if (discoveryDocumentResponse.IsError)
            {
                _logger.LogError(
                    _erroFactory.CreateError(ErrorConstants.E118001.code, ErrorConstants.E118001.message),
                    $"Error message: {discoveryDocumentResponse.Error}, HTTP Error reason: {discoveryDocumentResponse.HttpErrorReason}",
                    $"AuthServerUrl: {authServerUrl}",
                    null,
                    nameof(CreateAccessToken));

                throw new Exception("Discovery document failed");
            }

            return discoveryDocumentResponse;
        }

        private static async Task<TokenResponse> RequestAccessToken(HttpClient client, string tokenEndpoint, ClientCredentialDto clientData)
        {
            var clientCredentialData = new ClientCredentialsTokenRequest
            {
                Address = tokenEndpoint,
                GrantType = IdentityModel.OidcConstants.GrantTypes.ClientCredentials,
                ClientId = clientData.ClientId,
                ClientSecret = clientData.ClientSecret,
                Scope = clientData.Scope
            };

            return await client.RequestClientCredentialsTokenAsync(clientCredentialData);
        }

        private static async Task<TokenResponse> RequestPasswordGrantAccessToken(HttpClient client, string tokenEndpoint, PasswordGrantDto clientData)
        {
            var passwordGrantData = new PasswordTokenRequest
            {
                Address = tokenEndpoint,
                GrantType = IdentityModel.OidcConstants.GrantTypes.Password,
                ClientId = clientData.ClientId,
                ClientSecret = clientData.ClientSecret,
                Scope = clientData.Scope,
                UserName = clientData.Username,
                Password = clientData.Password
            };

            return await client.RequestPasswordTokenAsync(passwordGrantData);
        }

        private static DateTime GetTokenExpirationDate(string accessToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(accessToken);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;

            DateTime result = DateTimeOffset.FromUnixTimeSeconds(long.Parse(tokenExp)).LocalDateTime;
            return result;
        }

        private static bool CheckAccessTokenExpiration(string accessToken)
        {
            bool response;

            if (string.IsNullOrEmpty(accessToken))
            {
                response = true;
            }
            else
            {
                var expDate = GetTokenExpirationDate(accessToken);
                response = DateTime.Now.CompareTo(expDate.AddSeconds(RedisConfiguration.RemainingAccessTokenSeconds * -1)) > 0;
            }

            return response;
        }
    }
}
