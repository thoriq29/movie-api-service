using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Framework.Program
{
    public class BackendCoreAppConfigBuilder
    {
        public BackendCoreAppConfig Config { get { return _config; } }
        private BackendCoreAppConfig _config = new BackendCoreAppConfig();

        /// <summary>
        /// Add JSON App Setting
        /// </summary>
        /// <param name="appSettingFilePath"></param>
        public void AddAppSetting(string appSettingFilePath)
        {
            _config.AppSettingFilePaths.Add(appSettingFilePath);
        }

        /// <summary>
        /// Add secret file to your config
        /// User secret for Debug environment
        /// Google Encrypted File for Cloud environment
        /// </summary>
        /// <param name="secretFilePath"></param>
        public void AddSecret(string secretFilePath)
        {
            _config.SecretFilePaths.Add(secretFilePath);
        }

    }
}
