using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Interfaces.Health
{
    public interface IUrlConnectionHealthService
    {
        Task<bool> CheckUrlConnection(string url);
    }
}
