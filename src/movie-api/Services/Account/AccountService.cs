using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Movie.Api.Utils;
using Service.ServerToServer.Dto.ClientCredential;
using Service.ServerToServer.Dto.ServiceAccount;
using Service.ServerToServer.Service.MythicAccountService;

namespace Movie.Api.Services.Account
{
	public class AccountService: IAccountService
	{
        private readonly IConfiguration _configuration;
        private readonly IMythicAccountService _mythicAccountService;

        public AccountService(IConfiguration configuration, IMythicAccountService mythicAccountService)
		{
            _configuration = configuration;
            _mythicAccountService = mythicAccountService;
		}

        public async Task<AccountResponseDto> GetMythicAccountData(string accountId)
        {
            var (mythicAccountUrl, clientCredentialServerDto) = GetRequestCredential();
            return await _mythicAccountService.GetAccountData(mythicAccountUrl, accountId, clientCredentialServerDto);
        }

        public async Task<List<AccountResponseDto>> GetMythicAccountsData(List<string> accountIds)
        {
            var (mythicAccountUrl, clientCredentialServerDto) = GetRequestCredential();
            return await _mythicAccountService.GetAccountData(mythicAccountUrl, accountIds, clientCredentialServerDto);
        }

        private (string, ClientCredentialServerDto) GetRequestCredential()
        {
            var (mythicAccountUrl, authority, clientId, clientSecret) = GetConfiguration();

            ClientCredentialServerDto clientCredentialServerDto = new()
            {
                AuthServerUrl = authority,
                ClientId = clientId,
                ClientSecret = clientSecret,
            };
            return (mythicAccountUrl, clientCredentialServerDto);
        }

        private (string, string, string, string) GetConfiguration()
        {
            var mythicAccountUrl = ConfigurationUtils.UserInfoAccountUri(_configuration);
            var authority = _configuration.GetConnectionString("Authority");
            var clientId = _configuration.GetSection("Authority").GetSection("ClientId").Get<string>();
            var clientSecret = _configuration.GetSection("Authority").GetSection("ClientSecret").Get<string>();
            return (mythicAccountUrl, authority, clientId, clientSecret);
        }
    }
}

