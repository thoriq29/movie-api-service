using System.Threading.Tasks;
using Service.Core.Interfaces.Framework;

namespace Movie.Api.Services.Command.User
{
    public interface IUserCommandService
    {
        public Task<IServiceApiResult<string>> Regiser(string accountId);
    }
}