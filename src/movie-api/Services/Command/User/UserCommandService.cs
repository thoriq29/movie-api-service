
using System;
using System.Threading.Tasks;
using Movie.Api.Services.Account;
using Movie.Common.Models.User;
using Movie.Common.Services.User;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;

namespace Movie.Api.Services.Command.User
{
    public class UserCommandService : IUserCommandService
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _errorFactory;
        private readonly IServiceResultTemplate _serviceResultTemplate;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public UserCommandService(
            ICoreLogger logger,
            IErrorFactory errorFactory,
            IServiceResultTemplate serviceResultTemplate,
            IUserService userService,
            IAccountService accountService
        )
        {
            _logger = logger;
            _errorFactory = errorFactory;
            _serviceResultTemplate = serviceResultTemplate;
            _userService = userService;
            _accountService = accountService;
        }

        public async Task<IServiceApiResult<string>> Regiser(string accountId)
        {
            try
            {
                if (string.IsNullOrEmpty(accountId))
                {
                    return _serviceResultTemplate.BadRequest<string>("AccountId is required");
                }

                // check existing player
                var existingUser = await _userService.Find(it => it.AccountId == accountId);
                if (existingUser != null)
                {
                   return _serviceResultTemplate.BadRequest<string>("User already registered");
                }
                // get data user to mythic account
                var userData = await _accountService.GetMythicAccountData(accountId);
                if (userData == null)
                {
                    return _serviceResultTemplate.BadRequest<string>("Mythic account user not found");
                }

                // save data user
                var userModel = new UserModel()
                {
                    AccountId = userData.AccountId,
                    Email = userData.Email,
                    Username = userData.UserName
                };

                await _userService.Add(userModel);
                return _serviceResultTemplate.Success();
            }
            catch(Exception e)
            {
                return _serviceResultTemplate.InternalServerError<string>($"An error occurred while registering user {e.Message}");
            }
        }
    }
}