using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.MySql;

namespace Service.Core.Interfaces.Health
{
    public interface IHealthMySqlRepository<TEntity> : IBaseMySqlRepository<TEntity> where TEntity : class, new()
    {

    }
}
