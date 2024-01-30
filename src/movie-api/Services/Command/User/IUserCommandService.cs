using System.Threading.Tasks;
using Movie.Api.Dto.Genre;
using Movie.Api.Dto.Movie;
using Movie.Api.Dto.UserReview;
using Service.Core.Interfaces.Framework;

namespace Movie.Api.Services.Command.User
{
    public interface IUserCommandService
    {
        public Task<IServiceApiResult<string>> Regiser(string accountId);
    }
}