using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Service.ServerToServer.Dto.ClientCredential;
using Service.ServerToServer.Dto.ServiceAccount;

namespace Service.ServerToServer.Service.MythicAccountService
{
    #nullable enable
    public interface IMythicAccountService
    {
        public Task<AccountResponseDto?> GetAccountData(string accountUrl, HttpContext context);
        public Task<AccountResponseDto?> GetAccountData(string accountUrl, string accountId, ClientCredentialDto clientCredential);
        public Task<AccountResponseDto?> GetAccountData(string accountUrl, string accountId, string accessToken);
        public Task<List<AccountResponseDto>> GetAccountData(string accountUrl, List<string> accountId, string accessToken);
        public Task<List<AccountResponseDto>> GetAccountData(string accountUrl, List<string> accountId, ClientCredentialDto clientCredential);
    }
}
