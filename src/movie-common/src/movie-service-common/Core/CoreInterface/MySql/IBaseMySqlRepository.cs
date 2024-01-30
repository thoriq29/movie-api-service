using Service.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Core.Interfaces.MySql
{
    public interface IBaseMySqlRepository<T> : IBaseRepository<T>
    {
    }
}
