using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Core.Interfaces.Enums;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;

namespace Service.Core.Framework.Services
{
    public class ServiceWrapper : ControllerBase, IServiceWrapper
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _error;

        public ServiceWrapper(ICoreLogger logger, IErrorFactory error)
        {
            _logger = logger;
            _error = error;
        }

        /// <summary>
        /// Call Services that return ResultStatus and Wrap into IActionResult
        /// </summary>
        /// <typeparam name="T">Services parameters type</typeparam>
        /// <param name="serviceFunction">Services function to be called</param>
        /// <param name="serviceParameter">Services parameters</param>
        /// <returns></returns>
        public async Task<IActionResult> CallServices<T>(Func<T, Task<IServiceApiResult>> serviceFunction, T serviceParameter)
        {
            try
            {
                var result = await serviceFunction(serviceParameter);
                return ServiceApiResultToActionResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(_error.CreateErrorUncatched(), "error uncatched", e.Message);
                return Problem(e.StackTrace, e.TargetSite.Name, 500, e.Message, "uncatched");
            }
        }

        /// <summary>
        /// Call Services that return ResultStatus and Wrap into IActionResult
        /// </summary>
        /// <typeparam name="T">Services parameters type</typeparam>
        /// <typeparam name="Model">Return model</typeparam>
        /// <param name="serviceFunction">Services function to be called</param>
        /// <param name="serviceParameter">Services parameters</param>
        /// <returns></returns>
        public async Task<IActionResult> CallServices<T, Model>(Func<T, Task<IServiceApiResult<Model>>> serviceFunction, T serviceParameter)
        {
            try
            {
                var result = await serviceFunction(serviceParameter);
                return ServiceApiResultToActionResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(_error.CreateErrorUncatched(), "error uncatched", e.Message);
                return Problem(e.StackTrace, e.TargetSite.Name, 500, e.Message, "uncatched");
            }
        }

        /// <summary>
        /// Call Services that return ResultStatus and Wrap into IActionResult
        /// </summary>
        /// <typeparam name="T">Services parameters type</typeparam>
        /// <param name="serviceFunction">Services function to be called</param>
        /// <returns></returns>
        public async Task<IActionResult> CallServices<T>(Func<Task<IServiceApiResult<T>>> serviceFunction)
        {
            try
            {
                var result = await serviceFunction();
                return ServiceApiResultToActionResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(_error.CreateErrorUncatched(), "error uncatched", e.Message);
                return Problem(e.StackTrace, e.TargetSite.Name, 500, e.Message, "uncatched");
            }
        }

        private IActionResult ServiceApiResultToActionResult<T>(IServiceApiResult<T> result)
        {
            switch (result.ResultStatus)
            {
                case ResultStatus.NotFound:
                    return NotFound(result.BasicResponse);
                case ResultStatus.InternalError:
                    return Problem(result.BasicResponse.Message, result.BasicResponse.Error.target, (int)result.BasicResponse.StatusCode, result.ResultStatus.ToString());
                case ResultStatus.AlreadyExist:
                    return BadRequest(result.BasicResponse);
                case ResultStatus.ValidationFailed:
                    return BadRequest(result.BasicResponse);
                case ResultStatus.InvalidParameter:
                    return BadRequest(result.BasicResponse);
                case ResultStatus.BadRequest:
                    return BadRequest(result.BasicResponse);
                case ResultStatus.ApiRequestFail:
                    return Problem(result.BasicResponse.Message, result.BasicResponse.Error.target, (int)result.BasicResponse.StatusCode, result.ResultStatus.ToString());
                default:
                    return Ok(result.BasicResponse);
            }
        }

        private IActionResult ServiceApiResultToActionResult(IServiceApiResult result)
        {
            return ServiceApiResultToActionResult<string>(result);
        }
    }
}
