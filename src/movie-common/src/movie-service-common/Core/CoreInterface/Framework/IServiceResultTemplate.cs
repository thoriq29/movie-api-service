using Service.Core.Interfaces.Enums;
using Service.Core.Interfaces.Log;
using System.Collections.Generic;
using System.Net;

namespace Service.Core.Interfaces.Framework
{
    public interface IServiceResultTemplate
    {
        public IServiceApiResult Success();
        public IServiceApiResult<T> Success<T>();
        public IServiceApiResult<T> SuccessWithData<T>(T result);
        public IServiceApiResult<T> SuccessWithDataAndMeta<T>(T result, IDictionary<string, object> metaData);
        public IServiceApiResult<ICollection<T>> SuccessWithListData<T>(ICollection<T> resultList);
        public IServiceApiResult<string> NotFound(string message);
        public IServiceApiResult<string> InternalServerError(string message);
        public IServiceApiResult<string> ApiRequestError(string message);
        public IServiceApiResult<string> BadRequest(string message);
        public IServiceApiResult<string> IdNotValid(string id);
        public IServiceApiResult<string> IdNotFound(string id);
        public IServiceApiResult<string> PlayerNotFound(string id);
        public IServiceApiResult<string> UpdateDataFailed(string id);
        public IServiceApiResult<string> CustomResult(string message, IError error, ResultStatus resultStatus = ResultStatus.BadRequest, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest);
        public IServiceApiResult<string> FileNotValid();
        public IServiceApiResult<string> InvalidRequest();
        public IServiceApiResult<string> MalformedData(string message);

        public IServiceApiResult<T> NotFound<T>(string message);
        public IServiceApiResult<T> InternalServerError<T>(string message);
        public IServiceApiResult<T> ApiRequestError<T>(string message);
        public IServiceApiResult<T> BadRequest<T>(string message);
        public IServiceApiResult<T> IdNotValid<T>(string id);
        public IServiceApiResult<T> IdNotFound<T>(string id);
        public IServiceApiResult<T> PlayerNotFound<T>(string id);
        public IServiceApiResult<T> UpdateDataFailed<T>(string id);
        public IServiceApiResult<T> CustomResult<T>(string message, IError error, ResultStatus resultStatus = ResultStatus.BadRequest, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest);
        public IServiceApiResult<T> FileNotValid<T>();
        public IServiceApiResult<T> InvalidRequest<T>();
        public IServiceApiResult<T> MalformedData<T>(string message);
    }
}
