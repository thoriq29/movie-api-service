using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Framework.Hosting
{
    public static class HostingEnvironmentExtensions
    {
        /// <summary>
        ///     Checks if the current hosting environment name is <see cref="EnvironmentName.Debug" />.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is <see cref="EnvironmentName.Debug" />, otherwise false.</returns>
        public static bool IsDebug(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException(nameof(hostingEnvironment));
            return hostingEnvironment.EnvironmentName == EnvironmentName.Debug;
        }

        public static bool IsDevelopment(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException(nameof(hostingEnvironment));
            return hostingEnvironment.EnvironmentName == EnvironmentName.Development;
        }

        public static bool IsNotRelease(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException(nameof(hostingEnvironment));
            return hostingEnvironment.EnvironmentName != EnvironmentName.Release;
        }

        public static bool IsNotDebug(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException(nameof(hostingEnvironment));
            return hostingEnvironment.EnvironmentName != EnvironmentName.Debug;
        }

        /// <summary>
        ///     Checks if the current hosting environment name is <see cref="EnvironmentName.Release" />.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is <see cref="EnvironmentName.Release" />, otherwise false.</returns>
        public static bool IsRelease(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException(nameof(hostingEnvironment));
            return hostingEnvironment.EnvironmentName == EnvironmentName.Release;
        }

        /// <summary>
        ///     Checks if the current hosting environment name is <see cref="EnvironmentName.Sandbox" />.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is <see cref="EnvironmentName.Sandbox" />, otherwise false.</returns>
        public static bool IsSandbox(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException(nameof(hostingEnvironment));
            return hostingEnvironment.EnvironmentName == EnvironmentName.Sandbox;
        }

        /// <summary>
        ///     Checks if the current hosting environment name is <see cref="EnvironmentName.Staging" />.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is <see cref="EnvironmentName.Staging" />, otherwise false.</returns>
        public static bool IsStaging(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException(nameof(hostingEnvironment));
            return hostingEnvironment.EnvironmentName == EnvironmentName.Staging;
        }

        /// <summary>
        ///     Checks if the current hosting environment name is <see cref="EnvironmentName.Testing" />.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is <see cref="EnvironmentName.Testing" />, otherwise false.</returns>
        public static bool IsTesting(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException(nameof(hostingEnvironment));
            return hostingEnvironment.EnvironmentName == EnvironmentName.Testing;
        }

        /// <summary>
        ///     Checks if the current hosting environment name is <see cref="EnvironmentName.Review" />.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is <see cref="EnvironmentName.Review" />, otherwise false.</returns>
        public static bool IsReview(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException(nameof(hostingEnvironment));
            return hostingEnvironment.EnvironmentName == EnvironmentName.Review;
        }
    }
}
