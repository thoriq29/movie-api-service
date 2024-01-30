using Microsoft.EntityFrameworkCore;
using Service.Core.Interfaces.Framework;
using System.Threading.Tasks;

namespace Movie.Common.Contexts
{
    public class DatabaseInitializer : IServerPreRun
    {
        private readonly DatabaseContext _databaseContext;

        public DatabaseInitializer(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task Execute()
        {
            await _databaseContext.Database.MigrateAsync(); //:: Automatically runs pending migrations
        }
    }
}