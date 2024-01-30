using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Framework.Program
{
    public class BackendCoreAppConfig
    {
        private List<string> appSettingFilePaths = new List<string>();
        private List<string> secretFilePaths = new List<string>();

        public List<string> AppSettingFilePaths
        {
            get { return appSettingFilePaths; }
            set { appSettingFilePaths = value; }
        }

        public List<string> SecretFilePaths
        {
            get { return secretFilePaths; }
            set { secretFilePaths = value; }
        }
    }
}
