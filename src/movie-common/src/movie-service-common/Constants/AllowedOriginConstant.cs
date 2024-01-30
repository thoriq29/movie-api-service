namespace Service.Movie.Common.Constants
{
    public static class AllowedOriginsConstant
    {
        public static readonly string[] DOMAIN =
           {
            };

        public static readonly string[] DOMAIN_LOCAL_DEV =
            {
                "http://localhost:8080",
                "https://localhost:8080",
                "http://localhost:9090",
                "https://localhost:9090",
                "http://localhost:3000",
                "https://localhost:3000",
                "http://localhost:51001",
                "https://localhost:51001",
                // browser stack
                "https://bs-local.com:8080"
            };
    }
}