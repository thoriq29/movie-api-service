using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Service.Core.Interfaces.Framework
{
    public interface IServiceWrapper
    {
        /// <summary>
        /// Call Services that return ResultStatus and Wrap into IActionResult
        /// </summary>
        /// <typeparam name="T">Services parameters type</typeparam>
        /// <typeparam name="Model">Return model</typeparam>
        /// <param name="serviceFunction">Services function to be called</param>
        /// <param name="serviceParameter">Services parameters</param>
        /// <returns></returns>
        public Task<IActionResult> CallServices<T, Model>(Func<T, Task<IServiceApiResult<Model>>> serviceFunction, T serviceParameter);

        /// <summary>
        /// Call Services that return ResultStatus and Wrap into IActionResult
        /// </summary>
        /// <param name="serviceFunction">Services function to be called</param>
        /// <returns></returns>
        public Task<IActionResult> CallServices<T>(Func<Task<IServiceApiResult<T>>> serviceFunction);
    }
}
