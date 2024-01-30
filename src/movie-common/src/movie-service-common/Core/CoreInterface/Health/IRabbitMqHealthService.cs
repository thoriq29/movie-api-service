using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Interfaces.Health
{
    public interface IRabbitMqHealthService
    {
        Task<bool> CheckRabbitMqConnection();
    }
}
