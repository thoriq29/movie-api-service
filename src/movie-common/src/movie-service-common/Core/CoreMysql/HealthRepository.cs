

using Service.Core.Interfaces.Health;
using Service.Core.Interfaces.Log;

namespace Service.Core.MySql
{
    public sealed class HealthRepository : BaseMySqlRepository<BaseModel>, IHealthMySqlRepository<BaseModel>
    {
        private readonly ICoreLogger _logger;

        public HealthRepository(BaseDbContext dbContext, ICoreLogger logger, IErrorFactory errorList)
            : base(dbContext, logger, errorList)
        {
            _logger = logger;
        }
    }
}
