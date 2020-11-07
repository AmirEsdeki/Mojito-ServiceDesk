using System.Text.Json.Serialization;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Identity.Out
{
    public class UserTokenDTO
    {
        public UserTokenDTO(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }

        public string Token { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
