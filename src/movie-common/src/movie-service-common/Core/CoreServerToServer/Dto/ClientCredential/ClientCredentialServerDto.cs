using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServerToServer.Dto.ClientCredential
{
    public class ClientCredentialServerDto : ClientCredentialDto
    {
        public ClientCredentialServerDto() : base()
        {
            Scope = "server";
        }
    }
}
