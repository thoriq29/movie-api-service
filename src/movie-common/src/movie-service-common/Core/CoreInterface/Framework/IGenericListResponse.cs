using System.Collections.Generic;

namespace Service.Core.Interfaces.Framework
{
    public interface IGenericListResponse<T> : IBasicResponse
    {
        public ICollection<T> Data { get; set; }
        public IDictionary<string, object> Meta { get; set; }
    }
}
