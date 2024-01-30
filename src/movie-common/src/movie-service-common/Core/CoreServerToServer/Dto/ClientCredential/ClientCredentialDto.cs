namespace Service.ServerToServer.Dto.ClientCredential
{
    public class ClientCredentialDto : ClientCredentialSettingDto
    {
        public string AuthServerUrl { get; set; }
        public string Scope { get; set; }
    }
}
