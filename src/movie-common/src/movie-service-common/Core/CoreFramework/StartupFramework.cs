using Service.Core.Interfaces.Log;

namespace Service.Core.Framework
{
    public static class StartupFramework
    {
        public static void NecessaryInjection<Error, ErrorFactory>() 
            where Error : IError
            where ErrorFactory : IErrorFactory
        {

        }
    }
}
