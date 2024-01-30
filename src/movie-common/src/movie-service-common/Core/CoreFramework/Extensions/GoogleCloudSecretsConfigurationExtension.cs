using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Framework.Extensions
{
    public static class GoogleCloudSecretsConfigurationExtension
    {
        public static IConfigurationBuilder AddGoogleCloudSecrets(
          this IConfigurationBuilder @this)
        {
            string defaultSecretName = "backend-movie-development";
            string secretName = Environment.GetEnvironmentVariable("SECRET_NAME") ?? defaultSecretName;
            string gcpProjectId = Environment.GetEnvironmentVariable("PROJECT_ID");
            string gcpCredential = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            if (!string.IsNullOrEmpty(gcpCredential) && !string.IsNullOrEmpty(gcpProjectId))
            {
                SecretManagerServiceClient managerServiceClient = SecretManagerServiceClient.Create();
                SecretName parent = new SecretName(gcpProjectId, secretName);
                SecretVersion secretVersion = managerServiceClient.ListSecretVersions(parent).OrderByDescending(m => m.CreateTime).FirstOrDefault(m => m.State == SecretVersion.Types.State.Enabled);
                if (secretVersion != null)
                {
                    string stringUtf8 = managerServiceClient.AccessSecretVersion(secretVersion.SecretVersionName).Payload.Data.ToStringUtf8();
                    if (!string.IsNullOrEmpty(stringUtf8))
                    {
                        Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(stringUtf8));
                        JsonConfigurationExtensions.AddJsonStream(@this, stream);
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + " INF] Use google cloud secrets manager.");
                        return @this;
                    }
                }
            }
            Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + " INF] Use local secrets manager.");
            return @this;
        }
    }
}
