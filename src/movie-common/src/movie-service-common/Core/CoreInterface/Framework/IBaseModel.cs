using System;

namespace Service.Core.Interfaces.Framework
{
    public interface IBaseModel
    {
        public long ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
