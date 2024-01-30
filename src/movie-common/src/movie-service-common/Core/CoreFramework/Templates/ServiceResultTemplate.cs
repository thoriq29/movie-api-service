using Service.Core.Framework.Responses;
using Service.Core.Interfaces.Enums;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;
using System.Collections.Generic;
using System.Net;

namespace Service.Core.Framework.Movies
{
    public class ServiceResultTemplate : IServiceResultTemplate
    {
        private class ServiceApiResult : ServiceApiResult<string>, IServiceApiResult { }
        private class ServiceApiResult<T> : IServiceApiResult<T>
        {
            public ResultStatus ResultStatus { get; set; }
            public IGenericResponse<T> BasicResponse { get; set; }
        }

        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _error;

        public ServiceResultTemplate(ICoreLogger logger, IErrorFactory error)
        {
            _logger = logger;
            _error = error;
        }

        public IServiceApiResult Success()
        {
            return new ServiceApiResult()
            {
                ResultStatus = ResultStatus.Success,
                BasicResponse = new GenericResponse<string>() { StatusCode = HttpStatusCode.OK, Message = "ok" },
            };
        }

        public IServiceApiResult<T> Success<T>()
        {
            return new ServiceApiResult<T>()
            {
                ResultStatus = ResultStatus.Success,
                BasicResponse = new GenericResponse<T>() { StatusCode = HttpStatusCode.OK, Message = "ok" },
            };
        }

        public IServiceApiResult<T> SuccessWithData<T>(T result)
        {
            return new ServiceApiResult<T>()
            {
                ResultStatus = ResultStatus.Success,
                BasicResponse = new GenericResponse<T>()
                {
                    Data = result,
                    StatusCode = HttpStatusCode.OK,
                    Message = "ok"
                }
            };
        }

        public IServiceApiResult<T> SuccessWithDataAndMeta<T>(T result, IDictionary<string, object> metaData)
        {
            return new ServiceApiResult<T>()
            {
                ResultStatus = ResultStatus.Success,
                BasicResponse = new GenericResponse<T>()
                {
                    Data = result,
                    Meta = metaData,
                    StatusCode = HttpStatusCode.OK,
                    Message = "ok"
                }
            };
        }

        public IServiceApiResult<ICollection<T>> SuccessWithListData<T>(ICollection<T> resultList)
        {
            return new ServiceApiResult<ICollection<T>>()
            {
                ResultStatus = ResultStatus.Success,
                BasicResponse = new GenericResponse<ICollection<T>>()
                {
                    Data = resultList,
                    Meta = new Dictionary<string, object>
                    {
                        {"total", resultList.Count}
                    },
                    StatusCode = HttpStatusCode.OK,
                    Message = "ok"
                }
            };
        }

        public IServiceApiResult<T> NotFound<T>(string message)
        {
            _logger.LogInformation(message);
            return new ServiceApiResult<T>() { ResultStatus = ResultStatus.NotFound, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", _error.CreateContentNotFound().message, message), StatusCode = HttpStatusCode.NotFound, Error = _error.CreateContentNotFound() } };
        }
        public IServiceApiResult<T> InternalServerError<T>(string message)
        {
            _logger.LogInformation("Internal Server Error " + message);
            return new ServiceApiResult<T>() { ResultStatus = ResultStatus.InternalError, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", _error.CreateInternalServerError().message, message), StatusCode = HttpStatusCode.InternalServerError, Error = _error.CreateInternalServerError() } };
        }

        public IServiceApiResult<T> ApiRequestError<T>(string message)
        {
            _logger.LogInformation("Failed to send Api Request " + message);
            return new ServiceApiResult<T>() { ResultStatus = ResultStatus.ApiRequestFail, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", _error.CreateInternalServerError().message, message), StatusCode = HttpStatusCode.InternalServerError, Error = _error.CreateInternalServerError() } };
        }
        public IServiceApiResult<T> BadRequest<T>(string message)
        {
            _logger.LogInformation("Bad Request " + message);
            return new ServiceApiResult<T>() { ResultStatus = ResultStatus.BadRequest, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", _error.CreateInvalidOperation().message, message), StatusCode = HttpStatusCode.BadRequest, Error = _error.CreateInvalidOperation() } };
        }
        public IServiceApiResult<T> PlayerNotFound<T>(string id)
        {
            _logger.LogInformation(string.Format("{0} {1}", _error.CreatePlayerIDNotFound().message, id));
            return new ServiceApiResult<T> { ResultStatus = ResultStatus.NotFound, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", _error.CreatePlayerIDNotFound().message, id), StatusCode = HttpStatusCode.NotFound, Error = _error.CreatePlayerIDNotFound() } };
        }
        public IServiceApiResult<T> IdNotFound<T>(string id)
        {
            _logger.LogInformation(string.Format("{0} {1}", _error.CreateIDNotFound().message, id));
            return new ServiceApiResult<T> { ResultStatus = ResultStatus.NotFound, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", _error.CreateIDNotFound().message, id), StatusCode = HttpStatusCode.NotFound, Error = _error.CreateIDNotFound() } };
        }
        public IServiceApiResult<T> IdNotValid<T>(string id)
        {
            _logger.LogInformation(string.Format("{0} {1}", _error.CreateIdNotValid().message, id));
            return new ServiceApiResult<T>() { ResultStatus = ResultStatus.BadRequest, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", _error.CreateIdNotValid().message, id), StatusCode = HttpStatusCode.BadRequest, Error = _error.CreateIdNotValid() } };

        }
        public IServiceApiResult<T> UpdateDataFailed<T>(string id)
        {
            _logger.LogInformation(string.Format("{0} {1}", _error.CreateUpdateDataFailed().message, id));
            return new ServiceApiResult<T>() { ResultStatus = ResultStatus.InternalError, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", _error.CreateUpdateDataFailed().message, id), StatusCode = HttpStatusCode.InternalServerError, Error = _error.CreateUpdateDataFailed() } };

        }
        public IServiceApiResult<T> FileNotValid<T>()
        {
            _logger.LogInformation(_error.CreateFileNotValid().message);
            return new ServiceApiResult<T>() { ResultStatus = ResultStatus.BadRequest, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0}", _error.CreateFileNotValid().message), StatusCode = HttpStatusCode.BadRequest, Error = _error.CreateFileNotValid() } };
        }
        public IServiceApiResult<T> CustomResult<T>(string message, IError error, ResultStatus resultStatus = ResultStatus.BadRequest, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            _logger.LogInformation(string.Format("{0} {1}", error.message, message));
            return new ServiceApiResult<T>() { ResultStatus = resultStatus, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", error.message, message), StatusCode = httpStatusCode, Error = error } };
        }

        public IServiceApiResult<T> InvalidRequest<T>()
        {
            _logger.LogInformation(_error.CreateInvalidRequestDetected().message);
            return new ServiceApiResult<T> { ResultStatus = ResultStatus.BadRequest, BasicResponse = new GenericResponse<T>() { Message = _error.CreateInvalidRequestDetected().message, StatusCode = HttpStatusCode.BadRequest, Error = _error.CreateInvalidRequestDetected() } };
        }

        public IServiceApiResult<T> MalformedData<T>(string message)
        {
            _logger.LogInformation(string.Format("{0} {1}", _error.CreateMalformedData().message, message));
            return new ServiceApiResult<T>() { ResultStatus = ResultStatus.InternalError, BasicResponse = new GenericResponse<T>() { Message = string.Format("{0} {1}", _error.CreateMalformedData().message, message), StatusCode = HttpStatusCode.InternalServerError, Error = _error.CreateMalformedData() } };
        }

        public IServiceApiResult<string> NotFound(string message)
        {
            return NotFound<string>(message);
        }

        public IServiceApiResult<string> InternalServerError(string message)
        {
            return InternalServerError<string>(message);
        }

        public IServiceApiResult<string> ApiRequestError(string message)
        {
            return ApiRequestError<string>(message);
        }

        public IServiceApiResult<string> BadRequest(string message)
        {
            return BadRequest<string>(message);
        }

        public IServiceApiResult<string> IdNotValid(string id)
        {
            return IdNotValid<string>(id);
        }

        public IServiceApiResult<string> PlayerNotFound(string id)
        {
            return PlayerNotFound<string>(id);
        }
        public IServiceApiResult<string> IdNotFound(string id)
        {
            return IdNotFound<string>(id);
        }

        public IServiceApiResult<string> UpdateDataFailed(string id)
        {
            return UpdateDataFailed<string>(id);
        }

        public IServiceApiResult<string> CustomResult(string message, IError error, ResultStatus resultStatus = ResultStatus.BadRequest, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return CustomResult<string>(message, error, resultStatus, httpStatusCode);
        }

        public IServiceApiResult<string> FileNotValid()
        {
            return FileNotValid<string>();
        }

        public IServiceApiResult<string> InvalidRequest()
        {
            return InvalidRequest<string>();
        }

        public IServiceApiResult<string> MalformedData(string message)
        {
            return MalformedData<string>(message);
        }
    }
}
