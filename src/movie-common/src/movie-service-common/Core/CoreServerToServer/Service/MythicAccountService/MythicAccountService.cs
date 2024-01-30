using Microsoft.AspNetCore.Http;
using Service.Core.Interfaces.Log;
using Service.ServerToServer.Constants;
using Service.ServerToServer.Dto.ClientCredential;
using Service.ServerToServer.Dto.ServiceAccount;
using Service.ServerToServer.Service.ServerRequestService;
using Service.ServerToServer.Utils;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Http;

namespace Service.ServerToServer.Service.MythicAccountService
{
    #nullable enable
    public sealed class MythicAccountService : IMythicAccountService
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _erroFactory;
        private readonly IServerRequestService _serverRequestService;

        public MythicAccountService(ICoreLogger logger, IErrorFactory error, IServerRequestService serverRequestService)
        {
            _logger = logger;
            _erroFactory = error;
            _serverRequestService = serverRequestService;
        }

        public async Task<AccountResponseDto?> GetAccountData(string accountUrl, HttpContext context)
        {
            var user = context.User;
            var accountId = user.FindFirstValue("sub");
            var accessToken = _serverRequestService.GetAccessToken(context);

            return await GetAccountData(accountUrl, accountId, accessToken);
        }

        public async Task<AccountResponseDto?> GetAccountData(string accountUrl, string accountId, ClientCredentialDto clientCredential)
        {
            if (string.IsNullOrWhiteSpace(clientCredential.ClientId))
            {
                _logger.LogError(
                    ErrorConstants.E118007,
                    $"Accessing account data but not providing clientId",
                    $"AccountUrl: {accountUrl}, AccountId: {accountId}");
                return null;
            }
            else
            {
                _logger.LogInformation($"Getting account data with clientId: {clientCredential.ClientId}");
            }

            var cacheName = CacheUtil.FinalCacheName($"{RedisConfiguration.AccessTokenCacheName}-{clientCredential.ClientId}");
            var accessToken = await _serverRequestService.GetAccessToken(clientCredential, cacheName);

            return await GetAccountData(accountUrl, accountId, accessToken);
        }

        public async Task<AccountResponseDto?> GetAccountData(string accountUrl, string accountId, string? accessToken)
        {
            accountUrl = $"{accountUrl}?AccountIds={accountId}";

            var request = await CreateRequestAccountData(accountUrl, accessToken);

            return request.FirstOrDefault();
        }

        public async Task<List<AccountResponseDto>> GetAccountData(string accountUrl, List<string> accountId, string? accessToken)
        {
            var paramBuilder = new StringBuilder();
            string delimiter = "?";
            
            foreach (var id in accountId)
            {
                paramBuilder.Append($"{delimiter}AccountIds={id}");
                delimiter = "&";
            }

            accountUrl = $"{accountUrl}{paramBuilder}";

            var request = await CreateRequestAccountData(accountUrl, accessToken);

            return request;
        }
        
        public async Task<List<AccountResponseDto>> GetAccountData(string accountUrl, List<string> accountId, ClientCredentialDto clientCredential)
        {
            if (string.IsNullOrWhiteSpace(clientCredential.ClientId))
            {
                _logger.LogError(
                    ErrorConstants.E118007,
                    $"Accessing account data but not providing clientId",
                    $"AccountUrl: {accountUrl}, AccountId: {accountId}");
                return Enumerable.Empty<AccountResponseDto>().ToList(); ;
            }
            else
            {
                _logger.LogInformation($"Getting account data with accessToken: {clientCredential.ClientId}");
            }

            var cacheName = CacheUtil.FinalCacheName($"{RedisConfiguration.AccessTokenCacheName}-{clientCredential.ClientId}");
            var accessToken = await _serverRequestService.GetAccessToken(clientCredential, cacheName);

            return await GetAccountData(accountUrl, accountId, accessToken);
        }

        private async Task<List<AccountResponseDto>> CreateRequestAccountData(string accountUrl, string? accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                _logger.LogError(
                    ErrorConstants.E118006,
                    $"Accessing account data but not providing accessToken",
                    $"AccountUrl: {accountUrl}");
                return Enumerable.Empty<AccountResponseDto>().ToList(); ;
            }
            else
            {
                _logger.LogInformation($"Getting account data with accessToken. url: {accountUrl}");
            }

            List<AccountResponseDto> accountResponse = Enumerable.Empty<AccountResponseDto>().ToList();

            try
            {
                var request = await _serverRequestService.SendRaw(accountUrl, "", accessToken, HttpMethod.Get);

                if (request.IsSuccessStatusCode)
                {
                    var responseBody = await request.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<List<AccountResponseDto>>(responseBody);
                    accountResponse = responseData != null ? responseData : accountResponse;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(
                    _erroFactory.CreateError(ErrorConstants.E118005.code, ErrorConstants.E118005.message),
                    $"{e.Message}",
                    $"AccountUrl: {accountUrl}",
                    e,
                    nameof(GetAccountData));
            }
            
            return accountResponse;
        }
    }
}
