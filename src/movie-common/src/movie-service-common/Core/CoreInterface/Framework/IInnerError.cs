namespace Service.Core.Interfaces.Framework
{
    public interface IInnerError
    {
        public string Code { get; set; }
        public IInnerError Innererror { get; set; }
    }
}
