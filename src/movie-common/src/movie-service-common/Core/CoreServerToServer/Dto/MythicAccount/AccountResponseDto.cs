using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service.ServerToServer.Dto.ServiceAccount
{
    public class AccountResponseDto
    {
        public string AccountId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }

        [JsonProperty("profilePictureUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("wallet_address")]
        public string WalletAddress { get; set; }
        public string BirthDate { get; set; }
        public string Country { get; set; }
        public string Entitlement { get; set; }
        public string TwitterId { get; set; }
    }
}
