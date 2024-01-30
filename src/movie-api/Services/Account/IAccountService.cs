using System.Collections.Generic;
using System.Threading.Tasks;
using Service.ServerToServer.Dto.ServiceAccount;

namespace Movie.Api.Services.Account
{
	public interface IAccountService
	{
        public Task<AccountResponseDto> GetMythicAccountData(string accountId);
        public Task<List<AccountResponseDto>> GetMythicAccountsData(List<string> accountIds);
    }
}

