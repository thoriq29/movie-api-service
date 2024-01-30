using Service.ServerToServer.Dto.ClientCredential;

namespace Service.ServerToServer.Dto.PasswordGrant
{
    public class PasswordGrantDto : ClientCredentialDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}