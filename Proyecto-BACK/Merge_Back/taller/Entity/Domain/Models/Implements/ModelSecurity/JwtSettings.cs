using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Models.Implements.ModelSecurity
{
   public class JwtSettings
    {
        public string key { get; set; } = null!;
        public string issuer { get; set; } = "WebCDCP.API";
        public string audience { get; set; } = "WebCDCP.Client";
        public int accessTokenExpirationMinutes { get; set; } = 15;
        public int refreshTokenExpirationDays { get; set; } = 7;
    }
}
