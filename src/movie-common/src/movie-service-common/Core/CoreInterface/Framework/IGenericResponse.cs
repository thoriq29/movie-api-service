using System.Collections.Generic;

namespace Service.Core.Interfaces.Framework
{
    public interface IGenericResponse<T> : IBasicResponse
    {
        public T Data { get; set; }
        public IDictionary<string, object> Meta { get; set; }
    }
}
